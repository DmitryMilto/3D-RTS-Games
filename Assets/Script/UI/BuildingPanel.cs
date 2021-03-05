using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingPanel : MonoBehaviour
{
    public GameObject BuildingButton;
    [SerializeField]
    private Transform _buttonParent;
    private List<BuildingProfile> _buildings;
    // Start is called before the first frame update
    void Start()
    {
        _buildings = Resources.LoadAll<BuildingProfile>("Buildings/").ToList();
        foreach(var building in _buildings)
        {
            var buttonGo = Instantiate(BuildingButton, _buttonParent);
            buttonGo.GetComponent<BuildingPresenterOnButton>().Present(building);
        }
    }
}
