using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool firing = true;
    private float moveSpeed = 0.08125f;

    void FixedUpdate()
    {
        if(firing)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, moveSpeed);
            
        }
    }

    public void Shoot()
    {
        firing = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //damage player
        if(col.gameObject.CompareTag("Player"))
        {
            if(col.gameObject.GetComponent<PlayerMovement>() != null)
            {
                col.gameObject.GetComponent<PlayerMovement>().TakeDamage();
                Destroy(gameObject);
            }
        }
        //self destruct
        if(col.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
