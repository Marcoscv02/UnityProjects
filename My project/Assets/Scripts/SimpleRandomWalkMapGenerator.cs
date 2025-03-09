using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random; // Alias para evitar conflictos con System.Random

public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{   
    [SerializeField]
    protected SimpleRandomWalkSO randomWalkParameters;
   
    /* 
      Método que inicia la generación procedural del mapa.
      Llama al método que ejecuta el algoritmo de caminata aleatoria.
    */
    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParameters, startPosition); 

        tileMapVisualizer.Clear(); 
        tileMapVisualizer.paintFloorTiles(floorPositions); // Pinta las celdas del suelo
        Debug.Log(startPosition);
        WallGenerator.CreateWalls(floorPositions, tileMapVisualizer); // Genera las paredes del mapa
        
        // Verifica que startPosition esté dentro del mapa generado
        if (!floorPositions.Contains(startPosition))
        {
            Debug.LogWarning("startPosition no está dentro del mapa generado.");
            return;
        }

        // Coloca al personaje en startPosition
        tileMapVisualizer.createMakako(startPosition);
    }




    /*
    Ejecuta múltiples caminatas aleatorias para generar un patrón de suelo.
    retorna un conjunto de posiciones en 2D que representan el suelo del mapa generado.
    */
    protected HashSet<Vector2Int> RunRandomWalk( SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position; // Posición inicial de la caminata
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>(); // Conjunto que almacena las posiciones generadas

        for (int i = 0; i < parameters.iterations; i++)
        {
            // Ejecuta una caminata aleatoria desde la posición actual y obtiene un camino de celdas
            var path = ProveduralGenerationAlgorthims.simpleRandomWalk(currentPosition, parameters.walkLenght);
            floorPositions.UnionWith(path); // Agrega las posiciones del camino al conjunto principal

            // Si está activado, el próximo camino empieza en una posición aleatoria del suelo ya generado
            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        return floorPositions;
    }
}
