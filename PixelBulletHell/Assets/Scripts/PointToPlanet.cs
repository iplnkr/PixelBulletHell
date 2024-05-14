using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPlanet : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {   
        PlanetWater[] planets = FindObjectsOfType<PlanetWater>();
        if (planets.Length != 0)
        {
            transform.up = GetClosestPlanet(planets).transform.position - new Vector3(transform.position.x, transform.position.y, 2);
        }
    }

    private PlanetWater GetClosestPlanet(PlanetWater[] planets)
    {
        
        float dist = Vector3.Distance(transform.position, planets[0].transform.position);
        int index = 0;
        for(int i = 0; i < planets.Length; i++)
        {
            if(i > 0)
            {
                float newDist = Vector3.Distance(transform.position, planets[i].transform.position);
                if(newDist < dist)
                {
                    dist = newDist;
                    index = i;
                }
            }
        }
        return planets[index];
    }
}
