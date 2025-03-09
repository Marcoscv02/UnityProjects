using UnityEditor;
using UnityEngine;

// Se especifica que este editor personalizado es para objetos de tipo AbstractDungeonGenerator (y sus derivados)
[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RandomDungeonGeneratorEditor : Editor
{
    // Referencia al objeto AbstractDungeonGenerator que se está editando en el inspector
    AbstractDungeonGenerator generator;

    // Método llamado cuando el editor se inicializa
    private void Awake()
    {
        // Se asigna el objeto inspeccionado a la variable 'generator'
        generator = (AbstractDungeonGenerator)target;
    }

    // Método que define la interfaz personalizada del inspector
    public override void OnInspectorGUI()
    {
        // Muestra la interfaz predeterminada del inspector
        base.OnInspectorGUI();
        // Crea un botón en el inspector con la etiqueta "Create Dungeon"
        if (GUILayout.Button("Create Dungeon"))
        {
            // Al hacer clic en el botón, se llama al método GenerateDungeon() del generador
            generator.GenerateDungeon();
        }
    }
}
