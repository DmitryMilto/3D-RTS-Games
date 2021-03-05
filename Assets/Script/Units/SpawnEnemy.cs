using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] public List<NavMeshAgent> WaveEnemies;
    [SerializeField] private EnemyScriptableObject _enemyObject;
    public GameObject PositionStartSpawn;
    private int _countEnemyStart = 10;
    private int _currentCountEnemy;

    public static SpawnEnemy Instance { get; private set; }
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    public void SpawnEnemyUnit(int wave)
    {
        _currentCountEnemy = wave != 1 ? _countEnemyStart + _countEnemyStart * 35 / 100 : _countEnemyStart;
        for(int i = 0; i < _currentCountEnemy; i++)
        {
            var enemyUnit = Instantiate(_enemyObject.Prefab, PositionStartSpawn.transform.position, Quaternion.identity);
            enemyUnit.GetComponent<Enemy>().Present(_enemyObject, wave);
            WaveEnemies.Add(enemyUnit.GetComponent<NavMeshAgent>());
        }
    }
    private ISquardPositionGenerator _generator;
    // Start is called before the first frame update
    void Start()
    {
        _generator = GetComponent<ISquardPositionGenerator>();
        PositionStartSpawn.transform.GetOrAddComponent<Transform, ObserverableTransform>().OnChangePosition += (target) =>
        {
            SetSquardCenter(target.position);
        };
    }
    public void SetSquardCenter(Vector3 center)
    {
        var position = _generator.GetPosition(WaveEnemies.Count);
        for (int i = 0; i < position.Length; i++)
        {
            WaveEnemies[i].SetDestination(center + (position[i] * 3));
        }
    }
    public void DeleteUnit(NavMeshAgent agent)
    {
        WaveEnemies.Remove(agent);
    }
}
