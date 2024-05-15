using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] private float angVel = 0.1f;
    [SerializeField] private bool createAsteroidBelt = false;
    [SerializeField] private GameObject bulletTemp;

    void Start()
    {
        if(createAsteroidBelt)
        {
            Vector3 firstPos = bulletTemp.transform.position;
            for(int i = 1; i < 24; i++)
            {
                transform.Rotate(0, 0, 360/24f);
                GameObject newBullet = Instantiate(bulletTemp);
                newBullet.transform.position = firstPos;
                newBullet.transform.eulerAngles = new Vector3(0, 0, 90);
                newBullet.transform.localScale = bulletTemp.transform.lossyScale;
                newBullet.transform.parent = transform;
            }
        }
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, angVel);
    }
}
