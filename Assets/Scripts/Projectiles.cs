using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    public void InitProjectile(Vector3 posStart, Quaternion quaternion)
    {
        Instantiate(_projectile, posStart, quaternion, transform);
    }

    public void RestartProjectiles()
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
    }
}
