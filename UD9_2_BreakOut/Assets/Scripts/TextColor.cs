using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextColor : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float duration;//Tiempo que trancurre entre el cambio de color


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
                StartCoroutine("ChangeColor");
    }

    //Corrutina que cambia el color del texto
    IEnumerator ChangeColor(){
        float t = 0;

        while(t<duration){
            t+=Time.deltaTime/duration;
            text.color = Color.Lerp(Color.red, Color.blue, t);
            text.color = Color.Lerp(Color.blue, Color.red, t);
            yield return null;
        }
        StartCoroutine("ChangeColor");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
