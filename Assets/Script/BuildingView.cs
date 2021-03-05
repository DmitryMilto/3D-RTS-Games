using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingView : MonoBehaviour
{
    public Transform CurrentTransform;
    private State _currentState = State.InPlacing;

    public List<GameObject> PlaceHelpers;

    public void Awake()
    {
        CurrentTransform = GetComponent<Transform>();
        StartPlace();
    }

    public void Place()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Production");
        PlaceHelpers.ForEach((x) => x.SetActive(false));
        _currentState = State.Idle;
    }
    public void StartPlace() 
    {
        this.gameObject.layer = LayerMask.NameToLayer("BuildinGhost");
        PlaceHelpers.ForEach((x) => x.SetActive(true));
        _currentState = State.InPlacing;
    }

    public enum State
    {
        Idle,
        InPlacing
    }
}
