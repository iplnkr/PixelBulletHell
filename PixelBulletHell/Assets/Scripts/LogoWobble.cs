using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoWobble : MonoBehaviour
{
    private float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, startY + (0.125f * Mathf.Sin(Time.timeSinceLevelLoad * 2)), transform.position.z);
        transform.eulerAngles = new Vector3(0, 0, startY + (2f * Mathf.Cos(Time.timeSinceLevelLoad * 2)));
    }
}
