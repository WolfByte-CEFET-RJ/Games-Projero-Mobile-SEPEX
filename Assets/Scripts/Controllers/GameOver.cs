using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOver : MonoBehaviour
{
    public static Action onGameOver;
    [SerializeField] private GameObject gamOvPanel;
    [SerializeField] private GameObject waveSpawnerObj;
    void OnEnable()
    {
        onGameOver += ShowGameOver;
    }
    private void OnDisable()
    {
        onGameOver -= ShowGameOver;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowGameOver()
    {
        if (PlayerScore.Instance != null)
        {
            PlayerScore.Instance.PausarTimer();
        }
        gamOvPanel.SetActive(true);
        waveSpawnerObj.SetActive(false);//Evitar que apos Game Over o player possa passar de fase (como ocorrido na ultima reuniao). 
        //Mostrar highScore??
        //Tocar som de gameOver
        //Todos os etc de quando o Player morrer
    }
}
