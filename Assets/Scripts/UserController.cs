using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : Singleton<UserController>
{
    public int CurrentPoint;
    public int BestPoint;
    public int UserCoin;
    public int IsSoundOn;

    public int PriceTank = 10;
    public int PriceProjectile = 10;

    public List<Tank> listTanks;
    public List<ProjectileScriptableObject> lstProjectiles;

    private void Start()
    {
        LoadDB();
    }

    public void SetBuyTankId(int id)
    {
        foreach (Tank tank in UserController.Instance.listTanks)
        {
            if (tank.id == id)
            {
                tank.isBought = true;
                return;
            }
        }
    }

    public void SetSelectTankId(int id)
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
    }


    public void SetBuyProjectileId(int id)
    {
        foreach (ProjectileScriptableObject projectileScriptableObject in UserController.Instance.lstProjectiles)
        {
            if (projectileScriptableObject.id == id)
            {
                projectileScriptableObject.isBought = true;
                return;
            }
        }
    }


    public void SetSelectProjectileId(int id)
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
    }

    public void LoadDB()
    {
        CurrentPoint = PlayerPrefs.GetInt("CurrentPoint", 0);
        BestPoint = PlayerPrefs.GetInt("BestPoint", 0);
        UserCoin = PlayerPrefs.GetInt("UserCoin", 50);
        IsSoundOn = PlayerPrefs.GetInt("IsSoundOn", 1);
    }

    public void SaveIsSoundOn()
    {
        PlayerPrefs.SetInt("IsSoundOn", IsSoundOn);
        PlayerPrefs.Save();
    }

    public void SaveCurrentPoint()
    {
        PlayerPrefs.SetInt("CurrentPoint", CurrentPoint);
        PlayerPrefs.Save();
    }

    public void SaveBestPoint()
    {
        PlayerPrefs.SetInt("BestPoint", BestPoint);
        PlayerPrefs.Save();
    }

    public void SaveUserCoin()
    {
        PlayerPrefs.SetInt("UserCoin", UserCoin);
        PlayerPrefs.Save();
    }

}
