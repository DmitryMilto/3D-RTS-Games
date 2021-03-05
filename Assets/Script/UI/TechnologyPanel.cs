using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TechnologyPanel : MonoBehaviour
{
    [SerializeField] private List<ProductionElement> _production;

    private List<int> _productionCopyAttack = new List<int>();
    private List<int> _productionCopyXP = new List<int>();
    public GameObject TechnologyElementPrefab;
    [SerializeField]
    private Transform _currentContainer;

    void Start()
    {
        _production = Resources.LoadAll<ProductionElement>("Productions/").ToList();
        foreach(ProductionElement element in _production)
        {
            _productionCopyAttack.Add(element.Attack);
            _productionCopyXP.Add(element.Health);
            var techoGo = Instantiate(TechnologyElementPrefab, _currentContainer);
            techoGo.GetComponent<TechnologyPresenterOnButton>().Present(element);
        }
    }

    void OnDestroy()
    {
        for(int i = 0; i < _production.Count; i++)
        {
            _production[i].Attack = _productionCopyAttack[i];
            _production[i].Health = _productionCopyXP[i];
        }
    }
}
