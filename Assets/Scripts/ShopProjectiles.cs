using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopProjectiles : MonoBehaviour
{
    [SerializeField] private GameObject shopProjectilePrefab;

    private void Start()
    {
        InitShopProjectiles();
    }

    private void InitShopProjectiles()
    {
        foreach (ProjectileScriptableObject projectile in UserController.Instance.lstProjectiles)
        {
            GameObject gameObject = Instantiate(shopProjectilePrefab, transform);
            ShopProjectile shopProjectile = gameObject.GetComponent<ShopProjectile>();
            if (shopProjectile != null)
            {
                shopProjectile.InitData(projectile.id, projectile.sprite, projectile.isSelected, projectile.isBought);
            }
        }
    }

    internal void SetStateOnClickSelect()
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
            ShopProjectile shopProjectile = child.GetComponent<ShopProjectile>();
            if (shopProjectile != null)
            {
                if (shopProjectile.ProjectileIsBoughtAndNoSelet())
                {
                    shopProjectile.UIStateBuy();
                }
            }
        }
    }
}
