using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTank : MonoBehaviour
{
    [SerializeField] private Text txtBuy;
    [SerializeField] private Text txtSelect;
    [SerializeField] private Text txtUsing;
    [SerializeField] private Image imgTank;
    public int id;

    public void SetId(int idTank)
    {
        id = idTank;
    }

    public void SetImgTank(Sprite sprite)
    {
        imgTank.sprite = sprite; 
    }

    public void SetStateUI(bool isSelected, bool isBought)
    {
        if (isSelected == true && isBought == true)
        {
            UIStateSelect();
            return;
        }

        if (isSelected == false && isBought == true)
        {
            UIStateBuy();
            return;
        }

        if (isSelected == false && isBought == false)
        {
            UIStateNormal();
            return;
        }
    }

    public void InitData(int id, Sprite sprite, bool isSelected, bool isBought)
    {
        SetId(id);
        SetImgTank(sprite);
        SetStateUI(isSelected, isBought);
    }

    public void OnClickButton()
    {
        AudioController.Instance.PlayAudio(0);
        if (txtUsing.gameObject.activeSelf == true)
        {
            return;
        }
        if (txtSelect.gameObject.activeSelf == true)
        {
            OnClickSelect();
            return;
        }
        if (txtBuy.gameObject.activeSelf == true)
        {
            OnClickBuy();
            return;
        }
    }

    private void OnClickSelect()
    {
        foreach (Tank tank in UserController.Instance.listTanks)
        {
            if (tank.id == id)
            {
                tank.isSelected = true;
            }
            else
            {
                tank.isSelected = false;
            }
        }
        UIStateSelect();
        SetUiStateShopTanks();
        GameController.Instance.DeleteTank();
        GameController.Instance.InitTank();
        UserController.Instance.SetSelectTankId(id);
    }

    private void SetUiStateShopTanks()
    {
        ShopTanks shopTanks = transform.GetComponentInParent<ShopTanks>();
        if(shopTanks != null)
        {
            shopTanks.SetStateOnClickSelect();

        }
    }

    private void OnClickBuy()
    {
        if (UserController.Instance.UserCoin >= UserController.Instance.PriceTank)
        {
            UserController.Instance.UserCoin = UserController.Instance.UserCoin - UserController.Instance.PriceTank;
            UserController.Instance.SaveUserCoin();
            UIStateBuy();
            UserController.Instance.SetBuyTankId(id);
            PopupController.Instance.GetPopupShop().SetValueTxtCoin();
            UIController.Instance.SetValueTxtCoin();
        }
    }

    public bool TankIsBoughtAndNoSelet()
    {
        foreach (Tank tank in UserController.Instance.listTanks)
        {
            if (!tank.isSelected && tank.isBought && tank.id == id)
            {
                return true;
            }
        }
        return false;
    }

    public void UIStateBuy()
    {
        txtSelect.gameObject.SetActive(true);
        txtBuy.gameObject.SetActive(false);
        txtUsing.gameObject.SetActive(false);
    }

    public void UIStateSelect()
    {
        txtSelect.gameObject.SetActive(false);
        txtBuy.gameObject.SetActive(false);
        txtUsing.gameObject.SetActive(true);
    }

    public void UIStateNormal()
    {
        txtSelect.gameObject.SetActive(false);
        txtBuy.gameObject.SetActive(true);
        txtUsing.gameObject.SetActive(false);
    }
}
