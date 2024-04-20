using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarFlip : MonoBehaviour
{
    public Slider slider;
    public GameObject flip;
    public GameObject HealthBar;

    public void Start()
    {
       // HealthBar.SetActive(false);
        
    }
    /*
    public void Update()
    {
        if (flip.activeInHierarchy)
        {
            HealthBar.SetActive(true);
        }
    }
    */
    public void ActiveBar()
    {
        HealthBar.SetActive(true);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
