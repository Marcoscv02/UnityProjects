using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [SerializeField]  float delay;
    [SerializeField]  float force;
    [SerializeField]  float forceIncrease;
    //[SerializeField]  GameManager gameManager;
    [SerializeField]  AudioClip sfxPadel;
    [SerializeField]  AudioClip sfxBrick;
    [SerializeField]  AudioClip sfxWall;
    [SerializeField]  AudioClip sfxFall;
    Vector3 tamPadelOriginal= new Vector3(1.3f,0.2f,1);
    GameObject padel;
    AudioSource audioSource;  
    private bool reduced = false;  
    Dictionary<string, int> briks = new Dictionary<string, int>(){
        {"BrickGreen",10},
        {"BrickBlue",20},
        {"BrickRed",30},
        {"BrickPink",40},
        {"BrickOrenge",50},
        {"BirckYellow",60}, 
        {"pass", -5}
        };

  int hitsCount =   0;
  int brickCount=0;
  int sceneId;
  Rigidbody2D rb;



  // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource= GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("ThrowBall", delay);

        padel = GameObject.Find("padel");
        sceneId = SceneManager.GetActiveScene().buildIndex;
    }

    public void TamPadel(bool reduce){
        reduced = reduce;
        Vector3 tamPadelActual = padel.transform.localScale; //1.3,0.2

        if(reduce){
            padel.transform.localScale= new Vector3(tamPadelActual.x*0.5f, tamPadelActual.y, tamPadelActual.z);
        }else{       
            padel.transform.localScale=tamPadelOriginal;
        }
    }




   private void OnCollisionEnter2D(Collision2D other) {
        string tag = other.gameObject.tag;
        //Si la pelota toca un ladrillo
        if(briks.ContainsKey(tag)){
            audioSource.clip = sfxBrick;
            audioSource.Play();
            GameManager.UpdateScore(briks[tag]);
            Destroy(other.gameObject);
            brickCount++;
            if(brickCount==GameManager.totalBricks[sceneId]){
                SceneManager.LoadScene(sceneId+1);
            }
        }
        //Si la pelota toca la pala
        if (tag =="padel")
        {
            hitsCount++;
            
            if (hitsCount % 4==0){
                rb.AddForce(rb.linearVelocity*forceIncrease, ForceMode2D.Impulse);
            }
            
            Vector3 positionPadel= other.gameObject.transform.position;
            Vector2 contact= other.GetContact(0).point;

            if (rb.linearVelocity.x<0&&contact.x>positionPadel.x
            ||rb.linearVelocity.x>0&&contact.x>positionPadel.x){

                rb.linearVelocity = new Vector2(-rb.linearVelocityX, rb.linearVelocity.y);
            
            }
            audioSource.clip = sfxPadel;
            audioSource.Play();
        }
        if (tag == "wall" || tag == "wallUp"|| tag== "unbroken")
        {
            audioSource.clip = sfxWall;
            audioSource.Play();
        }
        if(tag == "wallUp"){
            TamPadel(true);
        }
    }


    
    // Update is called once per frame
    void Update()
    {
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



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag== "downLimit"){
            audioSource.clip = sfxFall;
            audioSource.Play();
            GameManager.restLife();
            Invoke("ThrowBall", delay);

            if(reduced) TamPadel(false);
        }
    }
   
}
