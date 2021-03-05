using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionTrigger : MonoBehaviour
{
    private bool _isCollision;

    public bool IsCollision { get => _isCollision; }

    public UnityEvent OnCollision = new UnityEvent();
    public UnityEvent OnFree = new UnityEvent();

    public void OnCollisionStay(Collision collision)
    {
        if (!_isCollision)
        {
            OnCollision.Invoke();
        }
        _isCollision = true;
    }
    public void OnCollisionExit(Collision collision)
    {
        OnFree.Invoke();
        _isCollision = false;
    }
}
