using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverableTransform : MonoBehaviour
{
    public event Action<Transform> OnChangePosition;
    private Vector3 _lastPose;
    private Transform _transform;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _lastPose = _transform.position;
    }
    // Update is called once per frame
    private void Update()
    {
        if(transform.position != _lastPose)
        {
            if(OnChangePosition != null)
            {
                OnChangePosition.Invoke(_transform);
            }
        }
        _lastPose = transform.position;
    }
}
