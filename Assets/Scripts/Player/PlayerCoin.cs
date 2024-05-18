using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoin : MonoBehaviour
{
    private int coins;
    [SerializeField] private Text coinText;
    [SerializeField] private float speedIncreased;
    [SerializeField] private int criticIncreased;
    private int criticIncrConter;//Outro contador(mesmo motivo do de baixo)
    [SerializeField] private float rangeIncreased;
    private float rangeIncrConter;//Contador para o quanto já foi incrementado, já que nem todas as armas possiveis podem existir quando o player decidir aumentar o alcance (*)
    private PlayerMove playerMove;
    //(*) Depois de algumas refatoracoes pro inventario funcionar, acredito que esses contadores ja sao desnecessarios. Caso haja necessidade
    //De otimizar os codigos, posso refatorar esse script daqui.
    private void Start()
    {
        playerMove = gameObject.GetComponent<PlayerMove>();
    }
    public int GetCoins() { return coins; }
    public void SetCoins(int price, int type) 
    { 
        coins -= price;
        coinText.text = "X " + coins.ToString();
        switch (type)
        {
            case 0: IncreaseSpeed(); break;
            case 1: IncreaseCritic(); break;
            case 2: IncreaseRange(); break;
            default: Debug.Log("Comprando nova arma"); break;
        }

    }

    void IncreaseSpeed()
    {
        playerMove.Speed += speedIncreased;
    }
    void IncreaseCritic()
    {
        criticIncrConter += criticIncreased;
        Weapon[] weapons = GameObject.FindObjectsOfType<Weapon>();
        foreach (Weapon w in weapons)
            w.UpgradeCritic(criticIncrConter);

    }
    void IncreaseRange()
    {
        rangeIncrConter += rangeIncreased;
        Weapon[] weapons = GameObject.FindObjectsOfType<Weapon>();
        foreach (Weapon w in weapons)
            w.UpgradeRadius(rangeIncrConter);
        
    }
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin") && gameObject.tag =="Player")
        {
            coins++;
            coinText.text = "X " + coins.ToString();
            Destroy(collision.gameObject);
        }
    }
    public void CoinReset()//usado no caso de pular o tutorial no meio da explicacao dos inimigos
    {
        coins = 0;
        coinText.text = "X " + coins.ToString();
    }
}
