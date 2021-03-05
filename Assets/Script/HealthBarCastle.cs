using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBarCastle : MonoBehaviour
{
    public Slider slider;
    public Gradient HealthCastleColor;
    public Image FillHealth;
    public Text StaticHealthCastle;
    [SerializeField] private int HealthCastle;
    public static HealthBarCastle Instance { get; private set; }

    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        if (Instance == this) Instance = null;
    }

    private void Awake()
    {
        if (HealthCastle != 0)
        {
            StartHealthCastle(HealthCastle);
        }
        else
        {
            StartHealthCastle(1000);
        }
    }

    public void StartHealthCastle(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        FillHealth.color = HealthCastleColor.Evaluate(1f);
    }
    
    //public void SetHealthCastle(int health)
    //{
    //    slider.value = health;

    //    FillHealth.color = HealthCastleColor.Evaluate(slider.normalizedValue);
    //}

    public void DamageCastle(int damage)
    {
        slider.value -= damage;
        FillHealth.color = HealthCastleColor.Evaluate(slider.normalizedValue);
        if (slider.value == slider.minValue)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void Update()
    {
        StaticHealthCastle.text = slider.value + "/" + slider.maxValue;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DamageCastle(10);
        }
    }
}
