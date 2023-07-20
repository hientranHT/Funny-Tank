using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupShop : PopupBase
{
    [SerializeField] private Text txtCoin;

    [SerializeField] private GameObject shopTanks;
    [SerializeField] private GameObject shopProjectiles;

    public void ShowPopupShop()
    {
        SetValueTxtCoin();
        base.ShowPopup();
    }

    public void HiddenPopupShop()
    {
        base.HiddenPopup();
    }

    public void OnClickClose()
    {
        HiddenPopupShop();
    }

    public void SetValueTxtCoin()
    {
        txtCoin.text = UserController.Instance.UserCoin.ToString();
    }

    public void OnClickBtnTankShop()
    {
        if(shopTanks.activeSelf)
        {
            return;
        }
        AudioController.Instance.PlayAudio(0);
        shopTanks.SetActive(true);
        shopProjectiles.SetActive(false);
    }
    public void OnClickBtnProjectileShop()
    {
        if (shopProjectiles.activeSelf)
        {
            return;
        }
        AudioController.Instance.PlayAudio(0);
        shopTanks.SetActive(false);
        shopProjectiles.SetActive(true);
    }
}
