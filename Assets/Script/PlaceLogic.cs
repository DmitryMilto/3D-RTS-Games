using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceLogic : MonoBehaviour
{
    public static PlaceLogic Instance { get; private set; }

    private PlacedEntity _currentPlace;

    public void Place(BuildingProfile profile)
    {
        if (_currentPlace != null) return;

        GameObject ghost = Instantiate(profile.BuildingView);
        _currentPlace = new PlacedEntity(ghost.GetComponent<BuildingView>(),
                                            ghost.GetComponent<BuilderProduction>(),
                                         ghost.GetComponent<CollisionTrigger>(),
                                         profile);
    }

    public void Update()
    {
        var inputModule = (CustomStandaloneInputModule)EventSystem.current.currentInputModule;
        if (_currentPlace != null)
        {
            if(Input.GetMouseButtonDown(0) && _currentPlace.TryPlace())
            {
                _currentPlace = null;
            }
            else
            _currentPlace.MoveView(inputModule.GetMousePositionOnGameObject());
        }
    }

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    public class PlacedEntity
    {
        private BuildingView _view;
        private BuilderProduction _production;
        private CollisionTrigger _trigger;
        private BuildingProfile _profile;
        private bool _isPlaced = false;

        public PlacedEntity(BuildingView view, BuilderProduction production, CollisionTrigger trigger, BuildingProfile profile)
        {
            _view = view;
            _production = production;
            _trigger = trigger;
            _profile = profile;
        }

        public void MoveView(Vector3 pos)
        {
            _view.CurrentTransform.position = pos;
        }
        public bool TryPlace()
        {
            if (_isPlaced) throw new System.InvalidOperationException();
            if (_trigger.IsCollision) return false;

            Destroy(_trigger);
            
            MoneyLogic.Instance.RemoveMoney(_profile.Prise);
            _production.Init(_profile);
            _isPlaced = true;
            _view.Place();
            return true;
        }
    }
}
