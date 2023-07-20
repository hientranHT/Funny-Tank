using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBase : MonoBehaviour
{
    [SerializeField] private RectTransform popup;
    [SerializeField] private Image imagePopup;
    [SerializeField] private RectTransform hubPopup;

    protected float timeDuration = 0.5f;
 
    protected void ShowPopup()
    {
        hubPopup.transform.localScale = Vector3.zero;
        popup.gameObject.SetActive(true);       
        imagePopup.DOFade(0.8f, timeDuration).SetUpdate(true).onComplete = ScaleHubPopup;
    }

    protected void ScaleHubPopup()
    {
        hubPopup.DOScale(1, timeDuration).SetUpdate(true);
    }

    protected void HiddenPopup()
    {
        hubPopup.DOScale(0, timeDuration).SetUpdate(true).onComplete = DoFadePopup; 
    }

    protected void DoFadePopup()
    {
        imagePopup.DOFade(0, timeDuration).SetUpdate(true).onComplete = SetActivePopup;
    }

    private void SetActivePopup()
    {
        popup.gameObject.SetActive(false);
    }
}
