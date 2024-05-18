using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniciadorJogo : MonoBehaviour
{
    public TutorialManager tutorialManager;

    void Start()//--> bota o tutorialManager em todos os Quadrados para que possa ser contado os quadradosAndados
    {
        QuadradoTutorial[] quadradoTutorials = FindObjectsOfType<QuadradoTutorial>();
        foreach (QuadradoTutorial quadrado in quadradoTutorials)
        {
            quadrado.tutorialManager = tutorialManager;
        }

    }
}
