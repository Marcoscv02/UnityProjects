using UnityEngine;

public class FireController : MonoBehaviour
{
    [SerializeField] float delay = 3;
    [SerializeField] GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bulletObject = Instantiate(bullet, transform.position, transform.rotation);
            Destroy (bulletObject, delay);
        }
    }
}
