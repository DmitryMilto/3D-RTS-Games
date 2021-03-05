using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyLogic : MonoBehaviour
{
    [SerializeField] private int _startMoney = 200;
    public static MoneyLogic Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    public int GetMoney()
    {
        return _startMoney;
    }
    public void AddMoney( int sum)
    {
        _startMoney += sum;
    }
    public void RemoveMoney(int remove)
    {
        _startMoney -= remove;
    }
}
