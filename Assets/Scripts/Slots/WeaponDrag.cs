using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image im;

    private bool blockMovement;//Booleano que sera responsavel por bloquear o movimento se não tiver dinheiro suficiente
    private bool isBought;//Responsavel pra saber se a arma foi comprada
    [Header("TransformSettings")]
    [SerializeField] private Transform weaponReference;//A arma da cena que o objeto arrastavel referencia
    [SerializeField] private Transform initialParent;//guardar a posicao inicial do objeto, em caso de sobreescrita
    private Transform parentTransf;

    [Space]
    [SerializeField] private Transform parentVisible;//Variavel necessária, pois sem ela certas armas acabam ficando atras de outros objs na cena
    //Aqui, temos um obj que esta "por baixo" dos outros objs, permitindo que ele possa estar a frente dos outros
    [Header("ShopSettings")]
    [SerializeField] private int costOfWeapon;
    [SerializeField] private Animator cadeadoAnim;
    [SerializeField] private GameObject textError;

    private SelectSlot slotAllocated;
    private PlayerCoin pCoin;
    #region Atributtes(Getters and Setters)
    public Transform ParentTransf { private get => parentTransf; set => parentTransf = value; }
    public Transform WeaponReference { get => weaponReference;  set => weaponReference = value; }
    public Transform InitialParent { get => initialParent; private set => initialParent = value; }
    public bool IsBought { get => isBought; set => isBought = value; }
    public int CostOfWeapon { get => costOfWeapon; private set => costOfWeapon = value; }
    public Animator CadeadoAnim { get => cadeadoAnim; set => cadeadoAnim = value; }

    #endregion
    void Start()
    {
        pCoin = FindObjectOfType<PlayerCoin>();
        im = GetComponent<Image>();
        if (!InitialParent)//Se initialParent nao estiver referenciada, referenciamos com o pai atual do objeto
            InitialParent = transform.parent;
        else//Senao, quer dizer que ele ja tem um pai, que aqui, faz referencia ao slot do Player. Logo, é como se a arma ja estivesse comprada
            isBought = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsBought && CostOfWeapon > pCoin.GetCoins())//Se ela ainda não foi comprada e o player nao tiver dinheiro suficiente
        {
            textError.SetActive(true);
            AudioManager.main.PlaySFX(AudioManager.main.errorSfx);
            blockMovement = true;
            if(cadeadoAnim != null)
                cadeadoAnim.SetInteger("transition", 3);
        }
        else if (!IsBought)//Apenas para fazer uma animação, para dar um feedback melhor pro player
        {
            if(cadeadoAnim != null)
            {
                cadeadoAnim.SetInteger("transition", 0);//Animacao de abrir cadeado
            }
        }
            

        im.raycastTarget = false;
        ParentTransf = transform.parent;
        transform.SetParent(parentVisible);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (blockMovement)
        {
            return;
        }
        if(Input.touchCount > 0)//Movimentação das armas pelo celular
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            touchPos.z = 0f;
            transform.position = touchPos;
        }
        else//Joel-Movimentacao pelo PC apenas para testar, pois estou com problemas para conectar o cel no Pc
        {
            transform.position = Input.mousePosition;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(blockMovement)//Se o movimento foi bloqueado no inicio, desativo o texto de erro e reseto o booleano de controle
        {
            textError.SetActive(false);
            blockMovement = false;
            if(cadeadoAnim != null)
                cadeadoAnim.SetInteger("transition", -1);
        }
        else if (cadeadoAnim != null && cadeadoAnim.GetInteger("transition") != 1)//Se nao estiver tocando a anim de arma comprada...
        {
            cadeadoAnim.SetInteger("transition", 2);//Executo a anim do cadeado trancando de volta
        }

        im.raycastTarget = true;
        transform.SetParent(ParentTransf);
        transform.localPosition = Vector3.zero;



        //TreatmentSlotAllocated();//Metodo meu que nao deu certo, e se voce esta lendo isso, e pq esqueci de apagar
    }

   
}
