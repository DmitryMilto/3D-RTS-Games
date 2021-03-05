using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject TargetAttack;
    [SerializeField] private StaticUnit Target;

    public int Health;
    public int Attack;

    public float attackTimer = 0.0f;
    public float SpeedAttact;

    private NavMeshAgent _agent;
    // Start is called before the first frame update
    public void Present(EnemyScriptableObject profile, int wave)
    {
        Attack = profile.Attack + profile.Attack * wave * 10 / 100;
        Health = profile.Health + profile.Health * wave * 10 / 100;
        attackTimer = SpeedAttact = profile.SpeedAttack;
    }

    public void Start()
    {
        TargetAttack = GameObject.Find("CastleTarget");
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Update()
    {
        Target = SortTargets(); //пытаемся достать её из общего списка
        if (Target == null) //если цели ещё нет
        {
           
            if(Target == null)
            {
                _agent.SetDestination(TargetAttack.transform.position);
                float distance = Vector3.Distance(TargetAttack.transform.position, transform.position);
                if (distance < 5f) //если мы на дистанции атаки и цель перед нами
                {
                    if (attackTimer > 0) attackTimer -= Time.deltaTime; //если таймер атаки больше 0 - отнимаем его
                    if (attackTimer <= 0) //если же он стал меньше нуля или равен ему
                    {
                        HealthBarCastle.Instance.DamageCastle(Health / 2);
                        SpawnEnemy.Instance.DeleteUnit(_agent);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        else //если у нас есть цель
        {
            _agent.SetDestination(Target.transform.position);
            float distance = Vector3.Distance(Target.transform.position, transform.position);
            if (distance < 10f) //если мы на дистанции атаки и цель перед нами
            {
                if (attackTimer > 0) attackTimer -= Time.deltaTime; //если таймер атаки больше 0 - отнимаем его
                if (attackTimer <= 0) //если же он стал меньше нуля или равен ему
                {
                    Target.DamageReceived(Attack);
                    attackTimer = SpeedAttact; //возвращаем таймер в исходное положение
                }
            }
        }
    }

    private StaticUnit SortTargets()
    {
        float closestUnitsDistance = 0; //инициализация переменной для проверки дистанции до пушки
        StaticUnit nearestUnits = null; //инициализация переменной ближайшего война
        List<StaticUnit> sortingUnits = UnitSelector.Instance.GetListStaticUnit(); //оздаём массив для сортировки

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

    public void DamageReceived(int damage)
    {
        Health -= damage;
        DeadEnemy();
    }
    private void DeadEnemy()
    {
        if(Health <= 0)
        {
            MoneyLogic.Instance.AddMoney(50);
            SpawnEnemy.Instance.DeleteUnit(_agent);
            Destroy(gameObject);
        }
    }
}
