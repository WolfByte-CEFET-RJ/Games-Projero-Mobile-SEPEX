using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    public int quadradosAndados;
    public WaveSpawnerTutorial waveSpawnerTutorial;

    private void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);
            }
        }
        
        if (popUpIndex == 0){
            if(quadradosAndados== 4)
            {
                popUpIndex++;

            }
        }
        if (popUpIndex == 1)
        {
         
            StartCoroutine(PodeSpawnComDelay(5f));
            StartCoroutine(PopUpIndexAdd(10f));
        }
        if (popUpIndex == 2)
        {

        }
    }
    public void IncrementaQuadrado()
    {
        quadradosAndados++;
        Debug.Log("Quadrados Andados: " + quadradosAndados);
    }
    private IEnumerator PodeSpawnComDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        waveSpawnerTutorial.PodeSpawnarTutorial();
    }
    private IEnumerator PopUpIndexAdd(float delay)
    {
        yield return new WaitForSeconds(delay);
        popUpIndex++;
    }
}
