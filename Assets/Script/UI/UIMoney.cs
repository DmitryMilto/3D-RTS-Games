using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMoney : MonoBehaviour
{
    public Text TextMoney;
    // Update is called once per frame
    void Update()
    {
        TextMoney.text = MoneyLogic.Instance.GetMoney().ToString();
    }
}
