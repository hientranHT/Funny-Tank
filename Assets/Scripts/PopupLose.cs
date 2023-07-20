using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupLose : PopupBase
{
    [SerializeField] private Text textBest;
    public void ShowPopupLose()
    {
        SetBestPoint();
        base.ShowPopup();
    }

    public void HiddenPopupLose()
    {
        base.HiddenPopup();
    }

    public void SetBestPoint()
    {
        textBest.text = "Best Score: " + UserController.Instance.BestPoint.ToString();
    }

    public void OnClickHome()
    {
        HiddenPopupLose();
        GameController.Instance.RestartBoardGame();
        UIController.Instance.BackMenuGame();
        GameController.Instance.StateGameBackHome();
    }

    public void OnClickRestartGame()
    {
        HiddenPopupLose();
        GameController.Instance.RestartGame();
        GameController.Instance.StateGameRestart();
    }
}
