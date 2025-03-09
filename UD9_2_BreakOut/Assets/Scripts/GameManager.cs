using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score {get; private set;} = 0;
    public static int lives {get; private set;} = 3;
    public static List<int> totalBricks = new List<int> {0,36,28};

    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] TextMeshProUGUI txtLives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnGUI(){
        txtScore.text= String.Format("{0,3:D3}", score); //Formato en 3 digitos
        txtLives.text= lives.ToString();
    }

    public static void UpdateScore(int points){
        score += points;
    }

    public static void restLife(){
        lives--;

        if (lives==0){
            //Game Over
        }
    }
}
