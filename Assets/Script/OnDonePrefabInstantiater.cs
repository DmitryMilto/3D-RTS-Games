using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BuilderProduction))]
public class OnDonePrefabInstantiater : MonoBehaviour
{

    private void Start()
    {
        GetComponent<BuilderProduction>().OnDone += (element) =>
        {
            var offset = Random.insideUnitCircle * 5;
            var production = Instantiate(element.Prefab, 
                        new Vector3(transform.position.x + offset.x, transform.position.y, transform.position.z + offset.y),
                        Quaternion.Euler(-90f, 0f, 0f));
            UnitSelector.Instance.Add(production.GetComponent<StaticUnit>());
            var i = production.GetComponent<StaticUnit>();
            i.Add(element);
        };
    }
}
