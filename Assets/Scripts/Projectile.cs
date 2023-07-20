using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _power;
    private float _endPosY = 10f;
    private float _newPosY;
    private float _speed = 10f;
    private const int StartPowerProjectile = 1;

    private int numberPowerProjectile;
    public int NumberPowerProjectile { get => numberPowerProjectile; set => numberPowerProjectile = value; }

    [SerializeField] private SpriteRenderer spriteRendererProjectile;
    private void Awake()
    {
        _newPosY = transform.position.y + _endPosY;
        InitNumberPowerProjectile();
        InitSpriteProjectile();
    }

    private void InitSpriteProjectile()
    {
        foreach (ProjectileScriptableObject projectileScriptableObject in UserController.Instance.lstProjectiles)
        {
            if(projectileScriptableObject.isSelected)
            {
                spriteRendererProjectile.sprite = projectileScriptableObject.sprite;
            }
        }
    }

    private void InitNumberPowerProjectile()
    {
        NumberPowerProjectile = StartPowerProjectile;
    }

    private void FixedUpdate()
    {
        if (GameController.Instance.CurrentStateGame != GameController.StateGame.Play)
        {
            return;
        }
       
        if (transform.position.y >= _endPosY)
        {
            DestroyProjectTile();
        }
        else
        {
            float newPosY = transform.position.y + _speed * Time.fixedDeltaTime;
            Vector3 newPos = new Vector3(transform.position.x, newPosY, transform.position.z);
            transform.position = newPos;
        }
    }

    public void DestroyProjectTile()
    {
        Destroy(gameObject);
    }
}
