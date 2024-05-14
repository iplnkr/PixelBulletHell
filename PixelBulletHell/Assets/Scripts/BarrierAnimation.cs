using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BarrierAnimation : MonoBehaviour
{
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        mat.mainTextureScale = new Vector2(transform.localScale.x, 0.9f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mat.mainTextureOffset = mat.mainTextureOffset + new Vector2(-0.025f, 0);
    }
}
