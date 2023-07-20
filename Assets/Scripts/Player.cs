using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private Projectiles _projectiles;

    private float _timeDurationMove = 0.5f;
    private float _timeDurationShooting = 0.25f;
    private bool _isCanShoot;
    private Vector3 startPos = new Vector3(0, -4, 0);

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        InitValue();
        InitTank();
    }

    private void InitValue()
    {
        _isCanShoot = true;
    }

    public void InitTank()
    {
        GameObject GameObjectTank = FindTank();
        if(GameObjectTank!=null)
        {
            Instantiate(GameObjectTank, transform);
        }
    }

    private GameObject FindTank()
    {
        foreach(Tank tank in UserController.Instance.listTanks)
        {
            if(tank.isSelected)
            {
                return tank.gameObject;
            }
        }
        return null;
    }

    private void Update()
    {
        if(GameController.Instance.CurrentStateGame !=GameController.StateGame.Play)
        {
            return;
        }

        //if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        if (Input.GetMouseButton(0))
        {
            Vector2 newPos = new Vector2();

            newPos = this.transform.position;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.x = worldPosition.x;
            if (worldPosition.y > 3.5f)
            {
                return;
            }
            if (newPos.x >= 2.5f || newPos.x <= -2.5f)
            {
                return;
            }

            this.transform.DOMove(newPos, _timeDurationMove);
        }
        StartCoroutine(InitProjectile());
    }

    private IEnumerator InitProjectile()
    {
        if(_isCanShoot)
        {
            _isCanShoot = false;
            AudioController.Instance.PlayAudio(1);
            _projectiles.InitProjectile(transform.position + Vector3.up, transform.rotation);
            for( float timer = _timeDurationShooting; timer >= 0 ; timer -= Time.deltaTime )
            {
                if (GameController.Instance.CurrentStateGame != GameController.StateGame.Play)
                {
                    yield return new WaitUntil(() => GameController.Instance.CurrentStateGame == GameController.StateGame.Play);
                }
                yield return null;
            }
            _isCanShoot = true;
        }
    }

    public void DeleteTank()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameController.Instance.LoseGame();
            AudioController.Instance.PlayAudio(4);
        }
    }

    public void RestartPlayer()
    {
        this.gameObject.transform.position = startPos;
    }
}

