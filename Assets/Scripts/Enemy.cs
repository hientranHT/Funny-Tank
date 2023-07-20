using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float _amplitudeX = 2.75f;
    private float _amplitudeY = 3f;
    private float _speed = 1f;
    private int direction = -1;
    private float typeMove;
    private bool isHaveCoin;
    public bool IsHaveCoin { get => isHaveCoin; set => isHaveCoin = value; }

    private int numberHpEnemy;
    public int NumberHpEnemy { get => numberHpEnemy; set => numberHpEnemy = value; }

    [SerializeField] private List<Sprite> lstSpriteEnemies;
    [SerializeField] private SpriteRenderer spriteRendererEnmemy;
    [SerializeField] private ParticleSystem CFX_Hit_C_Whitev;
    [SerializeField] private BoxCollider2D boxCollider2D;

    private bool isLive;

    private void Awake()
    {
        InitPos();
        InitTypeMove();
        InitHpEnemy();
        SetStateHaveCoinEnemy();
        SetSpriteEnemy();
    }

    private void SetSpriteEnemy()
    {
        if (IsHaveCoin)
        {
            spriteRendererEnmemy.sprite = lstSpriteEnemies[1];
        }
        else
        {
            spriteRendererEnmemy.sprite = lstSpriteEnemies[0];
        }
    }

    private void SetStateHaveCoinEnemy()
    {
        int num = UnityEngine.Random.Range(1, 6);
        if (num == 1)
        {
            IsHaveCoin = true;
        }
        else
        {
            IsHaveCoin = false;
        }
    }

    private void InitHpEnemy()
    {
        NumberHpEnemy = GameController.Instance.CurrentLevelGame.NumberHpEnemy;
        isLive = true;
    }

    private void InitTypeMove()
    {
        typeMove = UnityEngine.Random.Range(2, 5);
    }

    private void InitPos()
    {
        int random = UnityEngine.Random.Range(0, 2);

        if (random == 0)
        {
            this.transform.position = new Vector3(-_amplitudeX, _amplitudeY, 0);
        }
        else
        {
            this.transform.position = new Vector3(_amplitudeX, _amplitudeY, 0);
        }
    }
    private void FixedUpdate()
    {
        if (GameController.Instance.CurrentStateGame != GameController.StateGame.Play || !isLive)
        {
            return;
        }
        if (transform.position.x >= _amplitudeX)
        {
            direction = -1;
        }
        if (transform.position.x <= -_amplitudeX)
        {
            direction = 1;
        }
        SetPos(direction);
    }


    private void SetPos(int direction)
    {
        float newPosX = transform.position.x + direction * _speed * Time.fixedDeltaTime;
        float newPosY;
        if (typeMove % 2 == 0)
        {
            newPosY = CalSinNewPosY_2(newPosX, typeMove);
        }
        else
        {
            newPosY = CalSinNewPosY_1(newPosX, typeMove);
        }
        Vector3 newPos = new Vector3(newPosX, (float)newPosY, transform.position.z);
        transform.position = newPos;
    }

    private float CalSinNewPosY_1(float posX, float n)
    {
        return (float)((Math.Sin(n * Math.PI * ((posX / _amplitudeX) - (1 / (2 * n)))) * 3.5) - (0.5));
    }
    // chan thi -3/2n le thi -1/2n
    private float CalSinNewPosY_2(float posX, float n)
    {
        return (float)((Math.Sin(n * Math.PI * ((posX / _amplitudeX) - (3 / (2 * n)))) * 3.5) - (0.5));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Projectile projectile = collision.GetComponent<Projectile>();
            if (projectile != null)
            {
                CFX_Hit_C_Whitev.Play();
                DecreaseEnemyHP(projectile.NumberPowerProjectile);
                projectile.DestroyProjectTile();
            }
        }
    }
    
    private IEnumerator SetupDestroy()
    {
        boxCollider2D.enabled = false;
        isLive = false;
        spriteRendererEnmemy.DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void DecreaseEnemyHP(int numberPowerProjectile)
    {
        NumberHpEnemy = NumberHpEnemy - numberPowerProjectile;
        if (NumberHpEnemy <= 0)
        {
            AudioController.Instance.PlayAudio(3);
            SetRewardUser();
            UpdateLevel();
            StartCoroutine(SetupDestroy());
            Enemies enemies = transform.parent.GetComponent<Enemies>();
            if (enemies != null)
            {
                enemies.AmountEnemies--;
                if (enemies.AmountEnemies <= 0)
                {
                    enemies.Init();
                }
            }
        }
    }

    private void SetRewardUser()
    {
        if (isHaveCoin)
        {
            UserController.Instance.UserCoin += GameController.Instance.CurrentLevelGame.NumberHpEnemy;
            UIController.Instance.SetValueTxtCoin();
            AudioController.Instance.PlayAudio(2);
        }
        else
        {
            UserController.Instance.CurrentPoint += GameController.Instance.CurrentLevelGame.NumberHpEnemy;
            UIController.Instance.SetValueTxtPoint();
        }
    }

    private void UpdateLevel()
    {
        if (UserController.Instance.CurrentPoint <= 0)
        {
            return;
        }
        if (UserController.Instance.CurrentPoint % 5 == 0)
        {
            GameController.Instance.CurrentLevelGame.UpLevelGameHpEnemy(1);
        }
        if (UserController.Instance.CurrentPoint % 10 == 0)
        {
            GameController.Instance.CurrentLevelGame.UpLevelGameNumEnemies(1);
        }
    }
}
