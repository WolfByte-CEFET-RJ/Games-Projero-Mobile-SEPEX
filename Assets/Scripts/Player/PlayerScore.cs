using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static PlayerScore Instance;
    private int inimigosMortos;
    private int tempoPassado;
    private float timer;
    private bool pausado;
    private int verifica;
    
    private void Awake()
    {
        // Certifique-se de que há apenas uma instância do EnemyManager (Singleton)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantém o objeto ao mudar de cena
        }
        else
        {
            Destroy(gameObject);
        }
        pausado = false;
        verifica = PlayerPrefs.GetInt("Tutorial");
    }
    private void Update()
    {
        verifica = PlayerPrefs.GetInt("Tutorial");
        timer += Time.deltaTime;
        if (timer >= 1f && !pausado && verifica != 1)
        {
            tempoPassado++;
            timer = 0f; // Reseta o timer para contar o próximo segundo

            Debug.Log("Tempo passado: " + tempoPassado + " segundos");
        }
    }
    public void EnemyDestroyed()
    {
        inimigosMortos++;
        Debug.Log("Inimigos destruídos: " + inimigosMortos);
    }

    // Update is called once per frame
    public int GetEnemyDestroyedCount()
    {
        return inimigosMortos;
    }
    public int GetTempoPassado()
    {
        return tempoPassado;
    }
    public void PausarTimer()
    {
        pausado = true;
    }
    public void DespausarTimer()
    {
        pausado = false;
    }
}
