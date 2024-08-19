using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalEnemiesText;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    public GameObject[] paraDesativar;

    private void OnEnable()
    {
        foreach (GameObject obj in paraDesativar)
        {
            if (obj != null) // Certifique-se de que o objeto não seja null
            {
                obj.SetActive(false);
            }
        }
        if (PlayerScore.Instance != null)
        {
            // Obtém o total de inimigos mortos e o tempo jogado
            int totalEnemies = PlayerScore.Instance.GetEnemyDestroyedCount();
            int totalTime = PlayerScore.Instance.GetTempoPassado();

            // Calcula o valor final (inimigos mortos * tempo jogado)
            int finalScore = totalEnemies * totalTime;
            if (finalScore < 10000){
                finalScoreText.color = Color.red;
            }
            if (finalScore < 20000 && finalScore > 10000)
            {
                finalScoreText.color = Color.yellow;
            }
            if (finalScore > 30000)
            {
                finalScoreText.color = Color.green;
            }
            // Atualiza os textos de TextMeshPro com os valores
            if (totalEnemiesText != null)
                totalEnemiesText.text = "x " + totalEnemies.ToString();

            if (totalTimeText != null)
                totalTimeText.text = "x " + totalTime.ToString();

            if (finalScoreText != null)
                finalScoreText.text = finalScore.ToString();
        }
    }
}
