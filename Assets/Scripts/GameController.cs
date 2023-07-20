using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public StateGame CurrentStateGame;
    public LevelGame CurrentLevelGame;

    [SerializeField] private Player player;
    [SerializeField] private Enemies enemies;
    [SerializeField] private Projectiles projectiles;
    [SerializeField] CameraControl cameraControl;

    private GameController.StateGame stateGame;

    private void Start()
    {
        CurrentStateGame = StateGame.Menu;
        CurrentLevelGame = new LevelGame();
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        cameraControl.SetCamera();
    }

    public enum StateGame
    {
        Play,
        Lose,
        Pause,
        Menu
    }

    public void InitTank()
    {
        player.InitTank();
    }

    public void PlayGame()
    {
        CurrentStateGame = StateGame.Play;
    }

    public void RestartGame()
    {     
        RestartBoardGame();
        enemies.Init();
    }

    public void RestartBoardGame()
    {
        player.RestartPlayer();
        enemies.RestartEnemies();
        projectiles.RestartProjectiles();
        CurrentLevelGame.ResetLevelGame();
        UIController.Instance.RestartUIPlayGame();
    }

    public void LoseGame()
    {
        UserController.Instance.SaveUserCoin();
        if (UserController.Instance.CurrentPoint > UserController.Instance.BestPoint)
        {
            UserController.Instance.BestPoint = UserController.Instance.CurrentPoint;
            UserController.Instance.SaveBestPoint();
            UIController.Instance.SetValueTxtBestPointAtUIMenuGame();
        }
        CurrentStateGame = GameController.StateGame.Lose;
        PopupController.Instance.ShowPopupLose();
    }

    public void StateGameBackHome()
    {
        CurrentStateGame = GameController.StateGame.Menu;
    }

    public void StateGameRestart()
    {
        stateGame = StateGame.Play;
        Invoke("SetTimeCurrentStateGame", PopupController.Instance.timeDuration * 2);
    }

    public void DeleteTank()
    {
        player.DeleteTank();
    }

    public void StateGameContinue()
    {
        stateGame = StateGame.Play;
        Invoke("SetTimeCurrentStateGame", PopupController.Instance.timeDuration * 2);
    }

    private void SetTimeCurrentStateGame()
    {
        GameController.Instance.CurrentStateGame = stateGame;
    }
}

public class LevelGame
{
    public int MaxEnemies { get; set; }
    public int NumberHpEnemy { get; set; }

    public LevelGame()
    {
        MaxEnemies = 3;
        NumberHpEnemy = 1;
    }

    public void ResetLevelGame()
    {
        MaxEnemies = 3;
        NumberHpEnemy = 1;
    }

    public void UpLevelGameNumEnemies(int addEnemies)
    {
        MaxEnemies += addEnemies;
    }

    public void UpLevelGameHpEnemy(int addNumberHpEnemy)
    {
        NumberHpEnemy += addNumberHpEnemy;
    }
}
