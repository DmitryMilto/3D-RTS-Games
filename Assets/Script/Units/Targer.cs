using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targer : MonoBehaviour
{
    public GesturerClick Ground;
    // Start is called before the first frame update
    public void Start()
    {
        Ground.OnClick += (pos) =>
        {
            transform.position = pos;
        };
    }

}
