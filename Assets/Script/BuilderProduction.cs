using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BuilderProduction : MonoBehaviour, IPointerClickHandler
{
    public event UnityAction OnProductionChange;
    public event UnityAction<InProduction> OnTimeProductionChange;
    public event UnityAction<ProductionElement> OnDone;

    private Queue<InProduction> _elementsInProgress = new Queue<InProduction>();
    [SerializeField] private List<ProductionElement> _possibleProduction;

    public IEnumerable<ProductionElement> PossibleProduction 
    { 
        get 
        {
            return _possibleProduction; 
        } 
    }
    public IEnumerable<InProduction> ElementsInProgress
    {
        get
        {
            return _elementsInProgress;
        }
    }

    private BuildingProfile _profile;
    public void Init(BuildingProfile profile)
    {
        _possibleProduction = profile.PossibleProduction.Where((e)=> true).ToList();
        _profile = profile;
    }
    public void AddInProduction(ProductionElement element)
    {
        if (!_possibleProduction.Contains(element)) throw new System.InvalidOperationException();

        _elementsInProgress.Enqueue(new InProduction(element, 0));

        if(OnProductionChange != null)
        {
            OnProductionChange.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ProductionPanel.Instance.DisplayProduction(this, _profile);
    }

    private void Update()
    {
        if (_elementsInProgress.Count == 0) return;
        var inProgress = _elementsInProgress.Peek();
        inProgress.ApplyDelta(Time.deltaTime);

        if(OnTimeProductionChange != null)
        {
            OnTimeProductionChange.Invoke(inProgress);
        }
        if(inProgress.NormalizeTime >= 1)
        {
            _elementsInProgress.Dequeue();
            if(OnProductionChange != null)
            {
                OnProductionChange.Invoke();
            }
            if(OnDone != null)
            {
                OnDone.Invoke(inProgress.Element);
            }
        }
    }
    public void RemoveFromProduction(InProduction inProgress)
    {
        _elementsInProgress = new Queue<InProduction>(_elementsInProgress.Where((x) => x != inProgress));
        if (OnProductionChange != null)
        {
            OnProductionChange.Invoke();
        }
    }

    public class InProduction
    {
        public ProductionElement Element { get; private set; }
        public float ProductionTime { get; private set; }

        public float NormalizeTime => ProductionTime / Element.TimeForConstruct;

        public void ApplyDelta(float delta)
        {
            ProductionTime += delta;
        }

        public InProduction(ProductionElement element, float productionTime)
        {
            Element = element;
            ProductionTime = productionTime;
        }
    }
}
