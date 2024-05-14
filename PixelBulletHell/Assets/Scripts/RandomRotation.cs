using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    [SerializeField] private bool spin = false;
    void Start()
    {
        transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }

    void FixedUpdate()
    {
        if(spin)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }
}
