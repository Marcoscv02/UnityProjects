using System.Collections.Generic;
using UnityEngine;

// Algoritmo de procedimiento que genera caminos aleatorios en un mapa 2D
public static class ProveduralGenerationAlgorthims
{
    
     //Método que realiza una caminata aleatoria simple
    public static HashSet<Vector2Int> simpleRandomWalk(Vector2Int startPosition, int walkLenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();// Conjunto para almacenar las posiciones generadas

        
        path.Add(startPosition); // Agrega la posición inicial al camino
        var previousPosition = startPosition;
        
        // Realiza la caminata aleatoria por la cantidad de pasos indicada
        for (int i = 0; i < walkLenght; i++)
        {
            // Calcula una nueva posición sumando una dirección aleatoria a la posición actual y la agrega al camino
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        if (path.Contains(startPosition)) 
        {
            Debug.Log("Start position is in the path");
        }
        
        return path;// Retorna el conjunto con todas las posiciones generadas
    }



    public static List<Vector2Int> RandomWalkCorredor (Vector2Int startPosition, int corredorLenght)
    {   
        List<Vector2Int> corredor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corredor.Add(currentPosition);

        for(int i = 0; i < corredorLenght; i++)
        {
            currentPosition += direction;
            corredor.Add(currentPosition);
        }

        return corredor;
    }



    public static List<BoundsInt> BynarySpacePartitioning(BoundsInt spaceTosplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceTosplit);
        
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.x >= minWidth && room.size.y >= minHeight)
            {
                if (Random.value < 0.5f)
                {
                    
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, room, roomsQueue);
                    }
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, room, roomsQueue);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }  

                }
                else
                {
                     if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, room, roomsQueue);
                    }
                    else if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, room, roomsQueue);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }

                }
            }   
               
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, BoundsInt room, Queue<BoundsInt> roomsQueue)
    {
        var xsplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xsplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xsplit, room.min.y, room.min.z), 
                                        new Vector3Int(room.size.x - xsplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, BoundsInt room, Queue<BoundsInt> roomsQueue)
    {
        var ySplit = Random.Range(1, room.size.y); //(minHeight, room.size.y - minHeight);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
                                        new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}




//--------------------------------------------------------------------------------------------------------//




// Clase estática que define direcciones cardinales y proporciona utilidades relacionadas
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };

    // Función que retorna una dirección aleatoria de la lista de direcciones cardinales
    public static Vector2Int GetRandomCardinalDirection()
    {
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
