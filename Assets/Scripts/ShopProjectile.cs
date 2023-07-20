using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopProjectile : MonoBehaviour
{
    [SerializeField] private Text txtBuy;
    [SerializeField] private Text txtSelect;
    [SerializeField] private Text txtUsing;
    [SerializeField] private Image imgShopProjectile;
    public int id;

    public void SetId(int idTank)
    {
        id = idTank;
    }

    public void SetImgProjectile(Sprite sprite)
    {
        imgShopProjectile.sprite = sprite; 
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
        SetImgProjectile(sprite);
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
        foreach (ProjectileScriptableObject projectileScriptableObject in UserController.Instance.lstProjectiles)
        {
            if (projectileScriptableObject.id == id)
            {
                projectileScriptableObject.isSelected = true;
            }
            else
            {
                projectileScriptableObject.isSelected = false;
            }
        }
        UIStateSelect();
        SetUiStateShopProjectiles();
        UserController.Instance.SetSelectProjectileId(id);
    }

    private void SetUiStateShopProjectiles()
    {
        ShopProjectiles shopProjectiles = transform.GetComponentInParent<ShopProjectiles>();
        if (shopProjectiles != null)
        {
            shopProjectiles.SetStateOnClickSelect();

        }
    }

    private void OnClickBuy()
    {
        if (UserController.Instance.UserCoin >= UserController.Instance.PriceProjectile)
        {
            UserController.Instance.UserCoin = UserController.Instance.UserCoin - UserController.Instance.PriceTank;
            UserController.Instance.SaveUserCoin();
            UIStateBuy();
            UserController.Instance.SetBuyProjectileId(id);
            PopupController.Instance.GetPopupShop().SetValueTxtCoin();
            UIController.Instance.SetValueTxtCoin();
        }
    }

    public bool ProjectileIsBoughtAndNoSelet()
    {
        foreach (ProjectileScriptableObject projectileScriptableObject in UserController.Instance.lstProjectiles)
        {
            if (!projectileScriptableObject.isSelected && projectileScriptableObject.isBought && projectileScriptableObject.id == id)
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
