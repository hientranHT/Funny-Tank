using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileScriptableObject", menuName = "ProjectileScriptableObject", order = 1)]
public class ProjectileScriptableObject : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public bool isSelected;
    public bool isBought;
}
