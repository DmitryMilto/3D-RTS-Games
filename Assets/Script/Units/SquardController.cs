using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SquardController : MonoBehaviour
{
    [SerializeField] private List<NavMeshAgent> _agent;
    private ISquardPositionGenerator _generator;
    public Transform Target;
    public static SquardController Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _generator = GetComponent<ISquardPositionGenerator>();
        Target.GetOrAddComponent<Transform, ObserverableTransform>().OnChangePosition += (target) =>
        {
            SetSquardCenter(target.position);
        };  
    }
    public void SetSquardCenter(Vector3 center)
    {
        var position = _generator.GetPosition(_agent.Count);
        for(int i = 0; i < position.Length; i++)
        {
            _agent[i].SetDestination(center + (position[i] * 3));
        }
    }
    public void Add(NavMeshAgent agent)
    {
        _agent.Add(agent);
    }
    public void Remove()
    {
        _agent.Clear();
    }
    public List<NavMeshAgent> GetList() => _agent;
}
