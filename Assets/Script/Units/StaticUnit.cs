using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StaticUnit : MonoBehaviour
{
    public Sprite Icon;
    public int MaxHealthUnit;
    public int CurrentHealthUnit;

    public int AttackUnit;
    public float DistanceAttack;
    public int SpeedAttack;
    private float _attackTimer = 0.0f;

    private SpawnEnemy _flageEnemy;
    private Enemy _targetEnemy;
    [SerializeField] private GameObject SelecterUnit;

    public void Add(ProductionElement profile)
    {
        Icon = profile.Icon;
        CurrentHealthUnit = MaxHealthUnit = profile.Health;
        AttackUnit = profile.Attack;
        DistanceAttack = profile.DistanceAttack;
        _attackTimer = SpeedAttack = profile.SpeedAttack;
    }

    public static StaticUnit Instance { get; private set; }
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    private void Awake()
    {
        SelecterUnit = transform.Find("Selected").gameObject;
        _flageEnemy = GameObject.Find("flage").GetComponent<SpawnEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealthUnit <= 0)
        {
            UnitSelector.Instance.RemoveAgent(this);
            Destroy(this.gameObject);
        }
        _targetEnemy = SortTargets(); //пытаемся достать её из общего списка
        if (_targetEnemy != null) //если цели ещё нет
        {
            float distance = Vector3.Distance(_targetEnemy.transform.position, transform.position);
            if (distance < DistanceAttack) //если мы на дистанции атаки и цель перед нами
            {
                if (_attackTimer > 0) _attackTimer -= Time.deltaTime; //если таймер атаки больше 0 - отнимаем его
                if (_attackTimer <= 0) //если же он стал меньше нуля или равен ему
                {
                    _targetEnemy.DamageReceived(AttackUnit);
                    _attackTimer = SpeedAttack; //возвращаем таймер в исходное положение
                }
            }
        }
    }

    public void OnSelector(bool on)
    {
        SelecterUnit.SetActive(on);
    }

    public void UpdateStaticUnit(int xp, int attack)
    {
        MaxHealthUnit = xp;
        AttackUnit = attack;
    }
    public void DamageReceived(int damage)
    {
        CurrentHealthUnit -= damage;
    }

    public void HealthReceived(int health)
    {
        if (CurrentHealthUnit < MaxHealthUnit)
        {
            CurrentHealthUnit += health;
        }
        if(CurrentHealthUnit >= MaxHealthUnit)
        {
            CurrentHealthUnit = MaxHealthUnit;
        }
    }

    private Enemy SortTargets()
    {
        float closestUnitsDistance = 0; //инициализация переменной для проверки дистанции до пушки
        Enemy nearestUnits = null; //инициализация переменной ближайшего война
        List<Enemy> sortingUnits = new List<Enemy>(); //создаём массив для сортировки

        foreach (NavMeshAgent agent in _flageEnemy.WaveEnemies)
        {
            sortingUnits.Add(agent.GetComponent<Enemy>());
        }

        foreach (var units in sortingUnits) //для каждой пушки в массиве
        {
            //если дистанция до пушки меньше, чем closestTurretDistance или равна нулю
            if ((Vector3.Distance(transform.position, units.transform.position) < closestUnitsDistance) || closestUnitsDistance == 0)
            {
                closestUnitsDistance = Vector3.Distance(transform.position, units.transform.position); //Меряем дистанцию от моба до пушки, записываем её в переменную
                nearestUnits = units;//устанавливаем её как ближайшего
            }
        }
        return nearestUnits; //возвращаем ближайший ствол
    }
}
