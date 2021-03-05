using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class UnitSelector : MonoBehaviour
{
    public RectTransform selectedImage;

    [SerializeField] private List<StaticUnit> _agent;
    public static UnitSelector Instance { get; private set; }


    Rect selectionRect;

    Vector2 startPos;
    Vector2 endPos;

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DeactivateAllUnits();

            startPos = Input.mousePosition;

            selectionRect = new Rect();
            CheakSelectedUnits(selectionRect, startPos);
        }
        if (Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            DrawRectangle();
            //calculation
            if (Input.mousePosition.x < startPos.x)
            {
                selectionRect.xMin = Input.mousePosition.x;
                selectionRect.xMax = startPos.x;
            }
            else
            {
                selectionRect.xMax = Input.mousePosition.x;
                selectionRect.xMin = startPos.x;
            }
            //Y
            if (Input.mousePosition.y < startPos.y)
            {
                selectionRect.yMin = Input.mousePosition.y;
                selectionRect.yMax = startPos.y;
            }
            else
            {
                selectionRect.yMax = Input.mousePosition.y;
                selectionRect.yMin = startPos.y;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            CheakSelectedUnits();

            startPos = endPos = Vector2.zero;
            DrawRectangle();
        }
    }

    public void Add(StaticUnit agent)
    {
        _agent.Add(agent);
    }
    public void RemoveAgent(StaticUnit unit)
    {
        _agent.Remove(unit);
    }
    public List<StaticUnit> GetListStaticUnit() => _agent;

    void DrawRectangle()
    {
        Vector2 boxStart = startPos;
        Vector2 center = (boxStart + endPos) / 2;

        selectedImage.position = center;

        float sizeX = Mathf.Abs(boxStart.x - endPos.x);
        float sizeY = Mathf.Abs(boxStart.y - endPos.y);

        selectedImage.sizeDelta = new Vector2(sizeX, sizeY);
    }
    void CheakSelectedUnits()
    {
        foreach (StaticUnit unit in _agent)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(unit.transform.position)))
            {
                unit.OnSelector(true);
                SquardController.Instance.Add(unit.GetComponent<NavMeshAgent>());
            }
        }
    }
    void CheakSelectedUnits(Rect selectionRect, Vector2 vector)
    {
        selectionRect.xMin = vector.x - 15;
        selectionRect.xMax = vector.x + 15;
        selectionRect.yMin = vector.y - 15;
        selectionRect.yMax = vector.y + 15;

        foreach (StaticUnit unit in _agent)
        {
            if (selectionRect.Contains(Camera.main.WorldToScreenPoint(unit.transform.position)))
            {
                unit.OnSelector(true);
                SquardController.Instance.Add(unit.GetComponent<NavMeshAgent>());
            }
        }
    }

    void DeactivateAllUnits()
    {
        foreach (StaticUnit unit in _agent)
        {
            unit.OnSelector(false);
            SquardController.Instance.Remove();
        }
    }
}
