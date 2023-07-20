using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : Singleton<PopupController>
{
    [SerializeField] private PopupSetting popupSetting;
    [SerializeField] private PopupLose popupLose;
    [SerializeField] private PopupShop popupShop;

    public float timeDuration = 0.5f;

    public void ShowPopupShop()
    {
        popupShop.ShowPopupShop();
    }

    public void HiddenPopupShop()
    {
        popupShop.HiddenPopupShop();
    }

    public void ShowPopupSetting()
    {
        popupSetting.ShowPopupSetting();
    }

    public void HiddenPopupSetting()
    {
        popupSetting.HiddenPopupSetting();
    }

    public void ShowPopupLose()
    {
        popupLose.ShowPopupLose();
    }
    public void HiddenPopupLose()
    {
        popupLose.HiddenPopupLose();
    }

    public PopupSetting GetPopupSetting()
    {
        return popupSetting;
    }

    public PopupShop GetPopupShop()
    {
        return popupShop;
    }
}
