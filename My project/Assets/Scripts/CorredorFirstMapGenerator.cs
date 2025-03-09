

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorredorFirstMapGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int corredorLenght = 14, corredorCount=5;

    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;

 

    protected override void RunProceduralGeneration()
    {
        CorredorFirstGeneration();
    }



    private void CorredorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        List<List<Vector2Int>> corredores = CreateCorredors(floorPositions, potentialRoomPositions);

        HashSet <Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);
        
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        createRoomsAtDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        for (int i = 0; i < corredores.Count; i++)
        {
            //corredores[i] = IncreaseCorredorSizeByOne(corredores[i]);
            corredores[i] = IncreaseCorredorBrush3by3(corredores[i]);
            floorPositions.UnionWith(corredores[i]);
            
        }


        tileMapVisualizer.paintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer);

    }

   

    private void createRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(randomWalkParameters, position);
                roomFloors.UnionWith(room);
            }
        }
    }



    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach( var position in floorPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                if( floorPositions.Contains(position + direction))
                    neighboursCount++; 
                
            }
            if(neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }



    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions= new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List <Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(randomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor); 
        }
        return roomPositions;
    }



    private List<List<Vector2Int>> CreateCorredors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        List<List<Vector2Int>> corredores = new List<List<Vector2Int>>();

        for(int i = 0; i < corredorCount; i++)
        {
            var corredor = ProveduralGenerationAlgorthims.RandomWalkCorredor(currentPosition, corredorLenght);
            corredores.Add(corredor);
            currentPosition = corredor[corredor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corredor);
        }
        return corredores;
    }


    private List<Vector2Int> IncreaseCorredorSizeByOne(List<Vector2Int> corredor)
    {
        List<Vector2Int> newCorredor = new List<Vector2Int>();
        Vector2Int previousDirection = Vector2Int.zero;

        for (int i = 1; i < corredor.Count; i++)
        {
            Vector2Int directionFromCell = corredor[i] - corredor[i-1];

            if (previousDirection != Vector2Int.zero && directionFromCell != previousDirection)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        newCorredor.Add(corredor[i-1] + new Vector2Int(x, y));
                    }
                }
                previousDirection = directionFromCell;

            }else
            {
                Vector2Int newCorredorTileOffset = GetDirection90From (directionFromCell);
                newCorredor.Add(corredor[i-1]);
                newCorredor.Add(corredor[i-1] + newCorredorTileOffset);
            }
        }
         return newCorredor;
    }



    private Vector2Int GetDirection90From(Vector2Int direction)
    {
        if (direction == Vector2Int.up)
            return Vector2Int.right;
        if (direction == Vector2Int.right)
            return Vector2Int.down;
        if (direction == Vector2Int.down)
            return Vector2Int.left;
        if (direction == Vector2Int.left)
            return Vector2Int.up;
        
        return Vector2Int.zero;
    }


     private List<Vector2Int> IncreaseCorredorBrush3by3(List<Vector2Int> vector2Ints)
    {
        List<Vector2Int> newCorredor = new List<Vector2Int>();

        for (int i = 1; i < vector2Ints.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    newCorredor.Add(vector2Ints[i] + new Vector2Int(x, y));
                }
            }
        }
         return newCorredor;
    }
}
