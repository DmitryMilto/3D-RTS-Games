using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building/Productions")]
public class ProductionElement : ScriptableObject
{
    public float TimeForConstruct;

    public Sprite Icon;
    public Sprite IconClassUnit;
    public string Name;

    public int PriceUnits;

    public int Health;
    public int PriceTechnologyHealth;
    public int HealthAddPercentage;

    public int Attack;
    public int PriceTechnologyAttack;
    public int AttackAddPercentage;

    public float DistanceAttack;
    public int SpeedAttack;

    public GameObject Prefab;
}
