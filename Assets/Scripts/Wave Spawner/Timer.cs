using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class Timer : MonoBehaviour
{
    public UnityEvent timeOver;
    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Settings")]
    public float currentTime;
    [Header("Limit Settings")]
    public float timerLimit;
    [Header("RangeSettings")]
    [SerializeField] private Animator[] rangeAnims;
    void Start()
    {
        timerText.color = Color.yellow;
    }
    private void OnEnable()
    {
        Debug.LogAssertion("Iniciando Wave");
        foreach (Animator a in rangeAnims)
        {
            a.SetInteger("transition", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 3)
        {
            timerText.color = Color.red;
        }
        if (currentTime <= timerLimit)
        {
            currentTime = timerLimit;
            SetTimerText();
            timeOver.Invoke();
            foreach (Animator a in rangeAnims)
            {
                a.SetInteger("transition", 1);
            }
            enabled = false;
        }
        SetTimerText();
        executeSound();
    }
    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0");
    }

    private string controlString = "";
    private void executeSound()
    {
        if(controlString != currentTime.ToString("0"))
        {
            controlString = currentTime.ToString("0");

            if (currentTime > 2.25f)//Efeitos para o 5, 4 e 3
            {
                AudioManager.main.PlaySFX(AudioManager.main.timerSfx);

            }
            else//Efeitos para o 2, 1 e 0
            {
                AudioManager.main.PlaySFX(AudioManager.main.timerFinalSfx);
            }
        }
    }
}
