using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechnologyPresenterOnButton : MonoBehaviour
{
    public Text TextXPUp;
    public Text TextAttackUp;
    public Image Icon;
    public Button ButtonXP;
    public Button ButtonAttack;
    public Image IconClassUnit;

    public void Present(ProductionElement profile)
    {
        IconClassUnit.sprite = profile.IconClassUnit;
        Icon.sprite = profile.Icon;
        TextXPUp.text = profile.Health.ToString();
        TextAttackUp.text = profile.Attack.ToString();

        ButtonXP.onClick.AddListener(() =>
        {
            if (MoneyLogic.Instance.GetMoney() >= profile.PriceTechnologyHealth)
            {
                profile.Health += profile.Health * profile.HealthAddPercentage / 100;
                TextXPUp.text = profile.Health + " + " + profile.Health * profile.HealthAddPercentage / 100;
                MoneyLogic.Instance.RemoveMoney(profile.PriceTechnologyHealth);

                UpdateParament(profile.Health, profile.Attack);
            }
        });
        ButtonAttack.onClick.AddListener(() =>
        {
            if (MoneyLogic.Instance.GetMoney() >= profile.PriceTechnologyAttack)
            {
                profile.Attack += profile.Attack * profile.AttackAddPercentage / 100;
                TextAttackUp.text = profile.Attack + " + " + profile.Attack * profile.AttackAddPercentage / 100;
                MoneyLogic.Instance.RemoveMoney(profile.PriceTechnologyAttack);

                UpdateParament(profile.Health, profile.Attack);
            }
        });
    }

    private void UpdateParament(int health, int attack)
    {
        List<StaticUnit> _units = UnitSelector.Instance.GetListStaticUnit();
        foreach(StaticUnit unit in _units)
        {
            unit.UpdateStaticUnit(health,attack);
        }
    }

    //private ProductionElement onClickAttack(ProductionElement profile)
    //{
    //    ProductionElement result = new ProductionElement();
    //    return result;
    //}

    //private void onClickHealth(ProductionElement profile)
    //{
    //    //ProductionElement result = new ProductionElement();
    //    //return result;
    //    TextXPUp.text = profile.Health.ToString();
    //}
}
