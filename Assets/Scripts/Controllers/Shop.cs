using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int[] conters = new int[3];//0-velocidade; 1-critico; 2-alcance
    [SerializeField] private Sprite[] statusBar;
    [SerializeField] private Text[] errorTexts;
    [SerializeField][TextArea] private string noMoneyTxt;
    [SerializeField] [TextArea] private string noUpgradeTxt;

    [Space] [SerializeField] private Animator movementAnim;
    // Start is called before the first frame update
    private PlayerCoin coins;
    private void Start()
    {
        coins = FindObjectOfType<PlayerCoin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateSpeedAtribute(Image i)//0
    {
        if(conters[0] < statusBar.Length-1)
        {
            if(coins.GetCoins() >= 5)
            {
                AudioManager.main.PlaySFX(AudioManager.main.upgradesSfx[conters[0]]);
                conters[0]++;
                i.sprite = statusBar[conters[0]];
                coins.SetCoins(5, 0);
            }
            else{
                StartCoroutine(ShowErrorTxt(noMoneyTxt, errorTexts[0], 0));
                //Debug.LogError("Sem dinheiro para essa arma\nJoel-Em breve faco a logica visual desse aviso");
            }
        }
        else
            StartCoroutine(ShowErrorTxt(noUpgradeTxt, errorTexts[0], 0));
    }
    public void UpdateCriticAtribute(Image i)//1
    {
        if (conters[1] < statusBar.Length-1)
        {
            if(coins.GetCoins() >= 10)
            {
                AudioManager.main.PlaySFX(AudioManager.main.upgradesSfx[conters[1]]);
                conters[1]++;
                i.sprite = statusBar[conters[1]];
                coins.SetCoins(10, 1);
            }
            else
            {
                StartCoroutine(ShowErrorTxt(noMoneyTxt, errorTexts[1], 1));
                //Debug.LogError("Sem dinheiro para essa arma\nJoel-Em breve faco a logica visual desse aviso");
            }

        }
        else
            StartCoroutine(ShowErrorTxt(noUpgradeTxt, errorTexts[1], 1));
    }
    public void UpdateRangeAtribite(Image i)//2
    {
        if (conters[2] < statusBar.Length-1)
        {
            if (coins.GetCoins() >= 10)
            {
                AudioManager.main.PlaySFX(AudioManager.main.upgradesSfx[conters[2]]);
                conters[2]++;
                i.sprite = statusBar[conters[2]];
                coins.SetCoins(10, 2);
            }
            else
            {
                StartCoroutine(ShowErrorTxt(noMoneyTxt, errorTexts[2], 2));
                //Debug.LogError("Sem dinheiro para essa arma\nJoel-Em breve faco a logica visual desse aviso");
            }
        }   
        else
            StartCoroutine(ShowErrorTxt(noUpgradeTxt, errorTexts[2], 2));
    }
    private byte[] errorCont = new byte[3];

    public void MoveShop(string animat)
    {
        AudioManager.main.PlaySFX(AudioManager.main.menuBtnIn);
        movementAnim.Play(animat);
    }
    IEnumerator ShowErrorTxt(string txt, Text errorTarget, int index)
    {
        errorTarget.text = txt;
        errorCont[index]++;
        AudioManager.main.PlaySFX(AudioManager.main.errorSfx);
        yield return new WaitForSeconds(1.5f);
        errorCont[index]--;
        if(errorCont[index] == 0)
            errorTarget.text = "";
    }
}
