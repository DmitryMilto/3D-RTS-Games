using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPresenterOnButton : MonoBehaviour
{
    public Text BuildingName;
    public Image Icon;
    public Button Button;
    public void Present(BuildingProfile profile)
    {
        Icon.sprite = profile.Icon;
        BuildingName.text = profile.Prise.ToString();

        Button.onClick.AddListener(() =>
        {
            if (MoneyLogic.Instance.GetMoney() >= profile.Prise)
            {
                PlaceLogic.Instance.Place(profile);
            }
        });
    }
}
