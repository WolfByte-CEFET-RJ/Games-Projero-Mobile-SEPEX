using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopUp : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public int index;
    public bool isTyping=false;
    private void Start()//--> ao ser chamada ela inicia limpando a string do texto e chamando a funcao StartDialougue();
    {
        textComponent.text = string.Empty;
        StartDialougue();
    }
    private void Update()
    {
        if (textComponent.text == lines[index] && !isTyping)//--> Verifica se o que tinha que ser escrito na linha ja acabou, pulando para a proxima
        {
         
            NextLine();
            
        }
    }
    void StartDialougue()//--> Comeca o dialogo(caixa de texto tutorial) com o efeito de digitacao na hora
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()//--> escreve a linha com o delay==textSpeed de um caracter para o outro
    {
        isTyping = true;
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            AudioManager.main.PlaySFX(AudioManager.main.typeSound);
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }
    public bool AcabouTexto()//--> Verifica se todas as linhas ja foram escritas
    {
        if (textComponent.text == lines[index] && !isTyping && lines.Length - 1 == index)
        {

            return true;

        }
        else return false;
    }
    void NextLine()//-->  escreve a proxima linha
    {
       if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        
    }
}
