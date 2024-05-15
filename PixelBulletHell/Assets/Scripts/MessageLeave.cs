using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageLeave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Vanish", 2);
    }

    void Vanish()
    {
        gameObject.SetActive(false);
    }
}
