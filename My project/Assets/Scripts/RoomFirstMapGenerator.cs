using System.Collections.Generic;
using UnityEngine;

public class RoomFirstMapGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int minRoomWith = 4, minRoomHeight = 4;
    [SerializeField]
    private int mapWith = 20, mapHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;



    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }



    private void CreateRooms()
    {
        var roomList = ProveduralGenerationAlgorthims.BynarySpacePartitioning(new BoundsInt((Vector3Int) startPosition, new Vector3Int(mapWith, mapHeight, 0)), minRoomWith, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomList);
        }
        else
        {
            floor = CreateSimpleRooms(roomList);
        }
        
        List<Vector2Int> roomcenters = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomcenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corredores = ConnectRooms(roomcenters);
        floor.UnionWith(corredores);

        tileMapVisualizer.paintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tileMapVisualizer);
        tileMapVisualizer.createMakako(startPosition);
    }




    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomList.Count; i++)
        {
            var roomBounds = roomList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);

            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) &&
                    position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                }
                
            }
        }
        return floor;
    }




    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomcenters)
    {
        HashSet<Vector2Int> corredores = new HashSet<Vector2Int>();
        var currentRoomcenter = roomcenters[Random.Range(0, roomcenters.Count)];
        roomcenters.Remove(currentRoomcenter);

        while (roomcenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomcenter, roomcenters);
            roomcenters.Remove(closest);

            HashSet<Vector2Int> newCorredor = CreateCorredor(currentRoomcenter, closest);
            currentRoomcenter = closest;

            corredores.UnionWith(newCorredor);
        }
        return corredores;
    }




    private HashSet<Vector2Int> CreateCorredor(Vector2Int currentRoomcenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corredor = new HashSet<Vector2Int>();
        var position = currentRoomcenter;
        corredor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position+= Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position+= Vector2Int.down;
            }
            corredor.Add(position);
        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position+= Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position+= Vector2Int.left;
            }
            corredor.Add(position);
        }


        return corredor;
    }




    private Vector2Int FindClosestPointTo(Vector2Int currentRoomcenter, List<Vector2Int> roomcenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float lenght = float.MaxValue;

        foreach (var positions in roomcenters)
        {
            float currentDistance = Vector2Int.Distance(positions, currentRoomcenter);

            if (currentDistance < lenght)
            {
                closest = positions;
                lenght = currentDistance;
            }
        }
        return closest;
    }




    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomList)
        {
            for (int colum = offset; colum < room.size.x - offset; colum++)
            {
               for (int row = offset; row < room.size.y - offset; row++)
               {
                    Vector2Int position = (Vector2Int) room.min + new Vector2Int(colum, row);
                    floor.Add(position);
               } 
            }
        }
        return floor;
    }
}
