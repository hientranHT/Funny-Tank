using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTanks : MonoBehaviour
{
    [SerializeField] private GameObject ShopTank;

    private void Start()
    {
        InitShopTanks();
    }

    private void InitShopTanks()
    {
        foreach (Tank tank in UserController.Instance.listTanks)
        {
            GameObject gameObject = Instantiate(ShopTank, transform);
            ShopTank shoptank = gameObject.GetComponent<ShopTank>();
            if(shoptank!=null)
            {
                shoptank.InitData(tank.id, tank.sprite, tank.isSelected, tank.isBought);
            }
        }
    }

    public void SetStateOnClickSelect()
    {
        int i = 0;
        GameObject[] allChildren = new GameObject[transform.childCount];

        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        foreach (GameObject child in allChildren)
        {
            ShopTank shoptank = child.GetComponent<ShopTank>();
            if(shoptank != null)
            {
                if (shoptank.TankIsBoughtAndNoSelet())
                {
                    shoptank.UIStateBuy();
                }
            }
        }
    }
}
