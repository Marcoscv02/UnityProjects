using UnityEngine;
using UnityEngine.InputSystem;

public class mouseController : MonoBehaviour
{
    const float TURN_MIN = -45;
    const float TURN_MAX = 45;
    GameObject player;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player= transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       player.transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X")); 
    }
}
