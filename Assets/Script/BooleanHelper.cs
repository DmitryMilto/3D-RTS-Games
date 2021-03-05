using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooleanHelper : MonoBehaviour
{
    public List<GameObject> State1Active;
    public List<GameObject> State2Active;
    public void State1()
    {
        State2Active.ForEach((x) => x.SetActive(true));
        State1Active.ForEach((x) => x.SetActive(false));
    }
    public void State2()
    {
        State1Active.ForEach((x) => x.SetActive(true));
        State2Active.ForEach((x) => x.SetActive(false));
    }
}
