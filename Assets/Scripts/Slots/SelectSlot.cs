using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSlot : MonoBehaviour, IDropHandler
{
    private SelectSlotController controller;

    [Header("DragSettings")]
    [SerializeField] private WeaponDrag weaponDragReference;
    [Header("TransformSettings")]
    [SerializeField] private Transform playerSlotTransf;
    [SerializeField] private Transform outsideTransf;
    //[Header("ShopSettings")]
    private PlayerCoin pCoin;

    public WeaponDrag WeaponDragReference { get => weaponDragReference; set => weaponDragReference = value; }

    void Start()
    {
        pCoin = FindObjectOfType<PlayerCoin>();
        controller = FindObjectOfType<SelectSlotController>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        WeaponDrag weap = dropped.GetComponent<WeaponDrag>();

        if (pCoin.GetCoins() < weap.CostOfWeapon)//Toda logica que ocorre pra baixo desse codigo e considerando que voce possa comprar a arma.
            return;//Mas se o player nao tem moedas para isso. bloqueio o codigo nesse return

        controller.OnChangeWeaponDrag(weap);
        if (weap.IsBought == false )//Se ela ainda nao foi comprada...
        {
            BuyWeapon(weap.CostOfWeapon);
            weap.IsBought = true;
            if (weap.CadeadoAnim != null)
                StartCoroutine(OpenCadeado(weap));
        }
        weap.ParentTransf = transform;
        if (!WeaponDragReference)//Se o slot esta vazio
        {
            WeaponDragReference = weap;

            SetNewParent(WeaponDragReference.WeaponReference, playerSlotTransf);
        }
        else
        {
            SetNewParent(WeaponDragReference.WeaponReference, outsideTransf);

            SetNewParent(WeaponDragReference.transform, WeaponDragReference.InitialParent);

            WeaponDragReference = weap;
            Debug.Log(WeaponDragReference.name);
            SetNewParent(WeaponDragReference.WeaponReference, playerSlotTransf);
        }
        
    }
    IEnumerator OpenCadeado(WeaponDrag weap)
    {
        weap.CadeadoAnim.SetInteger("transition", 1);
        yield return new WaitForSeconds(1);
        weap.CadeadoAnim.gameObject.SetActive(false);
    }
    void BuyWeapon(int price)
    {
        pCoin.SetCoins(price, -1);
        
    }
    void SetNewParent(Transform thisTransf, Transform newParent)
    {
        thisTransf.SetParent(newParent);
        thisTransf.localPosition = Vector3.zero;
    }
}
