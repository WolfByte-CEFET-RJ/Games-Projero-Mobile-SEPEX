using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciadorJogo : MonoBehaviour
{
    public TutorialManager tutorialManager;

    void Start()
    {
        QuadradoTutorial[] quadradoTutorials = FindObjectsOfType<QuadradoTutorial>();
        foreach (QuadradoTutorial quadrado in quadradoTutorials)
        {
            quadrado.tutorialManager = tutorialManager;
        }

    }
}
