using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Tank", menuName = "Tank", order = 1)]
public class Tank : ScriptableObject
{
    public int id;
    public GameObject gameObject;
    public Sprite sprite;
    public bool isSelected;
    public bool isBought;
}
