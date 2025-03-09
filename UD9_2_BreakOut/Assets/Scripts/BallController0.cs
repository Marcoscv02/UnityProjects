using System.Collections.Generic;
using UnityEngine;

public class BallController0 : MonoBehaviour
{
    [SerializeField]  float force;
    [SerializeField]  AudioClip sfxPadel;
    [SerializeField]  AudioClip sfxBrick;
    [SerializeField]  AudioClip sfxWall;
    AudioSource audioSource;  
    Dictionary<string, int> briks = new Dictionary<string, int>(){
        {"BrickGreen",10},
        {"BrickBlue",20},
        {"BrickRed",30},
        {"BrickPink",40},
        {"BrickOrenge",50},
        {"BirckYellow",60} 
        };

  Rigidbody2D rb;



  // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("ThrowBall",0);
    }


   private void OnCollisionEnter2D(Collision2D other) {
        string tag = other.gameObject.tag;
        //Si la pelota toca un ladrillo
        if(briks.ContainsKey(tag)){
            audioSource.clip = sfxBrick;
            audioSource.Play();
            Destroy(other.gameObject);
        }
        if (tag == "wall" || tag == "wallUp")
        {
            audioSource.clip = sfxWall;
            audioSource.Play();
        }
    }


    //Metodo que se llama para lanzar la pelota en un angulo deterinado
    private void ThrowBall(){
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
        float dir_x, dir_y = -1;
        dir_x = Random.Range(0,2) == 0 ? -1 : 1;
        Vector2 dir = new Vector2(dir_x, dir_y);
        dir.Normalize();

        rb.AddForce(dir*force , ForceMode2D.Impulse);

    }
    

    
    // Update is called once per frame
    void Update()
    {
    }
   
}
