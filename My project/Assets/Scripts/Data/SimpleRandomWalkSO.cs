using UnityEngine;

/*
 Permite crear un asset de este tipo desde el menú de Unity.
 "fileName" define el nombre inicial del asset y "menuName" la ruta en el menú.
*/

[CreateAssetMenu(fileName = "SimpleRandomWalParameters_", menuName = "PCG/SimpleRandomWalData")]
public class SimpleRandomWalkSO : ScriptableObject
{
    // Número de veces que se ejecutará la caminata aleatoria.
    public int iterations = 10;
    
    // Longitud (cantidad de pasos) de cada caminata aleatoria.
    public int walkLenght = 10;
    
    // Si es verdadero, cada iteración iniciará en una posición aleatoria dentro del área generada.
    public bool startRandomlyEachIteration = true;
}
