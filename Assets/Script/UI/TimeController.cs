using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    private int _startWave;
    public Color StartColor;
    public Color MiddleColor;
    public Color EndColor;

    private float _timeInSecondsP;
    [SerializeField] private float _timeInSecondsStart = 15;
    private int _minutsP;
    private int _secondsP;
    public Text Info;
    public Text Timer;

    void Start()
    {
        _startWave = 1;
        _timeInSecondsP = _timeInSecondsStart;

    }

    // Update is called once per frame
    void Update()
    {
        _timeInSecondsP -= Time.deltaTime;

        _secondsP = (int)(_timeInSecondsP % 60);
        _minutsP = (int)(_timeInSecondsP / 60);

        Info.text = TextInfo();
        Info.color = Timer.color = ColorTextTimer(_timeInSecondsP);
        Timer.text = TimerText(_minutsP, _secondsP);

        if (_secondsP <= 0 && _minutsP == 0)
        {
            _timeInSecondsP = _timeInSecondsStart;
            _startWave++;
            MoneyLogic.Instance.AddMoney(100);
            SpawnEnemy.Instance.SpawnEnemyUnit(_startWave);
        }
    }
    private string TextInfo()
    {
        return _startWave == 1 ? "До волны осталось:" : "Волна " + _startWave + " прибудет через";
    }
    private Color ColorTextTimer(float time)
    {
        int start = Convert.ToInt32(_timeInSecondsStart);
        int difference = Convert.ToInt32(_timeInSecondsStart / 3);
        int position = (int)time;
        if(position > start - difference)
        {
            return StartColor;
        }
        else
        {
            if(position > start - 2 * difference)
            {
                return MiddleColor;
            }
            else
            {
                return EndColor;
            }
        }
    }

    private string TimerText(int minutes, int second)
    {
        return minutes < 10 ? 
            second < 10 ? 0 + minutes.ToString() + ":" + 0 + second.ToString() : 0 + minutes.ToString() + ":" + second.ToString() :
            second < 10 ? minutes.ToString() + ":" + 0 + second.ToString() : minutes.ToString() + ":" + second.ToString();
    }
}
