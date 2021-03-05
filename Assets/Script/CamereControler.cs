using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereControler : MonoBehaviour
{
    public float camSpeed = 20f;
    public float camBorderThickess = 10f;

    public float scrollSpeed = 20f;
    public float scrollLimitMinY = 20f;
    public float scrollLimitMaxY = 120f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - camBorderThickess)
        {
            pos.z += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= camBorderThickess)
        {
            pos.z -= camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - camBorderThickess)
        {
            pos.x += camSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= camBorderThickess)
        {
            pos.x -= camSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed *50f * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, scrollLimitMinY, scrollLimitMaxY);

        transform.position = pos;
    }
}
