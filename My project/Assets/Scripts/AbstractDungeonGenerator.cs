using UnityEngine;

/*
 Clase abstracta que define la base para la generación de mazmorras
 Hereda de MonoBehaviour para poder ser utilizada como componente en Unity
*/
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TileMapVisualizer tileMapVisualizer = null;

    protected Vector2Int startPosition = Vector2Int.zero;

    // Método público que inicia la generación de la mazmorra
    public void GenerateDungeon()
    {
        tileMapVisualizer.Clear();
        RunProceduralGeneration();
    }

    // Método abstracto que debe implementar cada clase derivada para generar el mapa
    protected abstract void RunProceduralGeneration();

}
