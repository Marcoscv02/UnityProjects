using UnityEngine;

public class PadelController : MonoBehaviour
{
    const float MAX_X = 3.2f;
    const float MIN_X = -3.2f;
    [SerializeField] private float speed = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //  transform.position = new Vector3(0, -4.5f, 0);
    }

    // Update is called once per frame
void Update()
{
    if (Input.GetKey("left") && transform.position.x > MIN_X)
    {
        transform.Translate(-Time.deltaTime * speed, 0, 0); // Movimiento a la izquierda (negativo en X)
    }

    if (Input.GetKey("right") && transform.position.x < MAX_X)
    {
        transform.Translate(Time.deltaTime * speed, 0, 0); // Movimiento a la derecha (positivo en X)
    }
}



}
