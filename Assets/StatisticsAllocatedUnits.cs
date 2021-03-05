using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StatisticsAllocatedUnits : MonoBehaviour
{
    public GameObject OneSelectedUnit;
    public Image IconUnitOne;
    public Text HealthUnitOne;
    public Text NameUnitOne;
    //public GameObject ManySelectedUnit;

    private List<NavMeshAgent> _units;
    // Start is called before the first frame update
    void Start()
    {
        OneSelectedUnit.SetActive(false);
        //ManySelectedUnit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _units = GameObject.Find("Main Camera").GetComponent<SquardController>().GetList();
        if(_units.Count == 1)
        {
            OneSelectedUnit.SetActive(true);
            foreach(NavMeshAgent agent in _units)
            {
                var unit = agent.GetComponent<StaticUnit>();
                IconUnitOne.sprite = unit.Icon;
                NameUnitOne.text = unit.name;
                HealthUnitOne.text = unit.CurrentHealthUnit + "/" + unit.MaxHealthUnit;
            }
        }
        if (_units.Count > 1)
        {
            OneSelectedUnit.SetActive(false);
            //ManySelectedUnit.SetActive(true);
        }
        if (_units.Count == 0)
        {
            OneSelectedUnit.SetActive(false);
            //ManySelectedUnit.SetActive(false);
        }
    }
}
