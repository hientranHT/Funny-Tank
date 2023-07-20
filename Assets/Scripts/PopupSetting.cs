using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSetting : PopupBase
{
    public void ShowPopupSetting()
    {
        base.ShowPopup();
        GameController.Instance.CurrentStateGame = GameController.StateGame.Pause;
        AudioController.Instance.PlayAudio(0);
    }

    public void HiddenPopupSetting()
    {
        base.HiddenPopup();
    }

    public void OnClickContinue()
    {
        AudioController.Instance.PlayAudio(0);
        HiddenPopupSetting();
        GameController.Instance.StateGameContinue();
    }

    public void OnClickRestart()
    {
        AudioController.Instance.PlayAudio(0);
        HiddenPopupSetting();
        GameController.Instance.RestartGame();
        GameController.Instance.StateGameRestart();
    }

    public void OnClickSound()
    {
        AudioController.Instance.PlayAudio(0);
        AudioController.Instance.SetSoundMusic();
    }

    public void OnClickHome()
    {
        AudioController.Instance.PlayAudio(0);
        HiddenPopupSetting();
        GameController.Instance.RestartBoardGame();
        UIController.Instance.BackMenuGame();
        GameController.Instance.StateGameBackHome();
    }
}
