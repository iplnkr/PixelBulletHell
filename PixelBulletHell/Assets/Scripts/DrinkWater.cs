using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkWater : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Water"))
        {
            if(col.gameObject.GetComponent<PlanetWater>() != null)
            {
                col.gameObject.GetComponent<PlanetWater>().Drink();
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Water"))
        {
            if(col.gameObject.GetComponent<PlanetWater>() != null)
            {
                col.gameObject.GetComponent<PlanetWater>().Undrink();
            }
        }
    }
}
