using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public string Name;
    public int Health;
    public int Attack;
    public int SpeedAttack;

    public GameObject Prefab;
}
