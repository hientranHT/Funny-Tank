using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class UIController : Singleton<UIController>
{
    [SerializeField] private Text txtCoin;
    [SerializeField] private Text txtPoint;
    [SerializeField] private Text txtBestPointAtUIMenuGame;

    [SerializeField] private GameObject UIMenuGame;
    [SerializeField] private GameObject UIPlayGame;

    [SerializeField] private Image IconSoundAtMenu;
    [SerializeField] private Image IconSoundAtGame;

    private Color colorSoundOn = new Color(1f, 1f, 1f, 1f);
    private Color colorSoundOff = new Color(1f, 1f, 1f, 0.3f);

    private void Start()
    {
        SetValueTxtCoin();
        SetValueTxtPoint();
        SetValueTxtBestPointAtUIMenuGame();
    }

    public void SetValueTxtBestPointAtUIMenuGame()
    {
        txtBestPointAtUIMenuGame.text = UserController.Instance.BestPoint.ToString();
    }

    public void SetValueTxtPoint()
    {
        txtPoint.text = UserController.Instance.CurrentPoint.ToString();
    }

    public void SetValueTxtCoin()
    {
        txtCoin.text = UserController.Instance.UserCoin.ToString();
    }

    public void OnClickPlayUIPlayGame()
    {
        GameController.Instance.RestartGame();
        GameController.Instance.PlayGame();
        UIMenuGame.SetActive(false);
        UIPlayGame.SetActive(true);
        AudioController.Instance.PlayAudio(0);
    }

    public void OnClickSoundUIPlayGame()
    {
        AudioController.Instance.PlayAudio(0);
        AudioController.Instance.SetSoundMusic();
    }

    public void SetOnUISound()
    {
        IconSoundAtMenu.color = colorSoundOn;
        IconSoundAtGame.color = colorSoundOn;
    }

    public void SetOffUISound()
    {
        IconSoundAtMenu.color = colorSoundOff;
        IconSoundAtGame.color = colorSoundOff;
    }

    public void RestartUIPlayGame()
    {
        UserController.Instance.CurrentPoint = 0;
        SetValueTxtPoint();
    }

    public void OnClickShopAtUIPlayGame()
    {
        AudioController.Instance.PlayAudio(0);
    }

    public void BackMenuGame()
    {
        UIMenuGame.SetActive(true);
        UIPlayGame.SetActive(false);
    }
}
