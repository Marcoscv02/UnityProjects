using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

       Vector3 moveInput = Vector2.zero;
       moveInput.x = Input.GetAxis("Horizontal") * speed;
       moveInput.z = Input.GetAxis("Vertical") * speed;
       moveInput *= Time.deltaTime;

       transform.Translate(moveInput);

       if (Input.GetKey(KeyCode.Escape))
       {
        Cursor.lockState = CursorLockMode.None;
       }
    }
}
