using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomStandaloneInputModule : StandaloneInputModule
{
    public Vector3 GetMousePositionOnGameObject()
    {
        var mainPointerData = GetLastPointerEventData(-1);
        if (mainPointerData == null) return Vector3.zero;
        return mainPointerData.pointerCurrentRaycast.worldPosition;
    }
    public GameObject GetCuttentHoveredGameObject()
    {
        var mainPointerData = GetLastPointerEventData(-1);
        if (mainPointerData == null) return null;
        return mainPointerData.hovered.FirstOrDefault();
    }
}
