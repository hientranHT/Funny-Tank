using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

    public bool isSpawnEnemies = false;

    private int amountEnemies;
    public int AmountEnemies { get => amountEnemies; set => amountEnemies = value; }

    public void Init()
    {
        if (isSpawnEnemies)
        {
            return;
        }
        isSpawnEnemies = true;
        StartCoroutine(InitEnemies());
    }

    private IEnumerator InitEnemies()
    {
        
        for (int i = 0; i < GameController.Instance.CurrentLevelGame.MaxEnemies; i++)
        {
            //yield return new WaitUntil(() => GameController.Instance.CurrentStateGame == GameController.StateGame.Play);
            //yield return new WaitForSeconds(2);
            for (float timer = 2; timer >= 0; timer -= Time.deltaTime)
            {
                if (GameController.Instance.CurrentStateGame != GameController.StateGame.Play)
                {
                    yield return new WaitUntil(() => GameController.Instance.CurrentStateGame == GameController.StateGame.Play);
                }
                yield return null;
            }
            InitEnemy();
        }
        isSpawnEnemies = false;
    }

    private void InitEnemy()
    {
        if (Instantiate(_enemy, transform))
        {
            AmountEnemies++;
        }
    }

    public void RestartEnemies()
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
            DestroyImmediate(child.gameObject);
        }
        AmountEnemies = 0;
    }
}
