using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthFountainController : MonoBehaviour
{
    [SerializeField] private float HealthRecoveryRate;
    [SerializeField] private int RestoredHealth;
    [SerializeField] private int RecoveryDistance;

    private float _speedHealth;

    private List<StaticUnit> _unit;
    // Start is called before the first frame update
    void Start()
    {
        _speedHealth = HealthRecoveryRate;
    }

    // Update is called once per frame
    void Update()
    {
        _unit = GameObject.Find("Main Camera").GetComponent<UnitSelector>().GetListStaticUnit();
        if (_unit != null)
        {
            foreach(StaticUnit agent in _unit)
            {
                float distance = Vector3.Distance(agent.transform.position, transform.position);
                if(distance < RecoveryDistance)
                {
                    if (_speedHealth > 0) _speedHealth -= Time.deltaTime; //если таймер атаки больше 0 - отнимаем его
                    if (_speedHealth <= 0) //если же он стал меньше нуля или равен ему
                    {
                        agent.HealthReceived(RestoredHealth);
                        _speedHealth = HealthRecoveryRate;
                    }
                }
            }
        }
    }
}
