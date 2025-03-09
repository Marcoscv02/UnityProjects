using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
    // Referencias a los Tilemap usados para el suelo y las paredes.
    [SerializeField]
    private Tilemap wallTilemap, floorTileMap ;

    // Referencias a los tiles que se utilizarán para pintar el suelo y la parte superior de la pared.
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalDownLeft, wallDiagonalDownRight, wallDiagonalUpLeft, wallDiagonalUpRight;

    [SerializeField]
    private GameObject makakoObject;




    // Método público que se encarga de pintar los tiles del suelo en las posiciones proporcionadas.
    public void paintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTileMap, floorTile);
    }




    // Método privado que itera sobre una colección de posiciones y pinta un tile en cada una de ellas.
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
       foreach (var position in positions)
       {
           PaintSingleTail(tilemap, tile, position);
       }
    }





    /*
      Método interno para pintar una pared básica en una posición determinada.
    */
    internal void PaintSingleBasicWall(Vector2Int position, string bynaryType)
    {
        int typeAsInt = Convert.ToInt32(bynaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
            PaintSingleTail(wallTilemap, tile, position);
    }



    // Método interno que se encarga de pintar una esquina de pared simple en la posición especificada.
    internal void PaintSingleCornerWall(Vector2Int position, string bynaryType)
    {
        int typeAsInt = Convert.ToInt32(bynaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;

        }else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;

        }else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalDownLeft;
            
        }else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalDownRight;

        }else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalUpLeft;

        }else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalUpRight;

        }else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;

        }else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }

        if (tile!=null)
        {
            PaintSingleTail(wallTilemap, tile, position);
        }
    }





    // Método privado que se encarga de pintar un único tile en la posición especificada.
    private void PaintSingleTail(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }





    // Método público que limpia (elimina) todos los tiles de los tilemaps de suelo y pared.
    public void Clear() //Se llama antes de crear un nuevo mapa
    {
        floorTileMap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }


    internal void createMakako(Vector2Int startPosition)
    {
        // Verifica que el prefab esté asignado
        if (makakoObject == null)
        {
            Debug.LogError("makakoPrefab no está asignado en TileMapVisualizer.");
            return;
        }

        // Destruye la instancia existente si hay una
        if (makakoObject != null)
        {
            Destroy(makakoObject); // Destruye el objeto anterior
        }

        // Calcula la posición central de la celda de inicio
        Vector3 spawnPosition = new Vector3(startPosition.x + 0.5f, startPosition.y + 0.5f, 0f);

        // Instancia el prefab en la posición calculada
        makakoObject = Instantiate(makakoObject, spawnPosition, Quaternion.identity);
        Debug.Log("Makako creado en: " + spawnPosition);
    }
}
