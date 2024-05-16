using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private int phase = 4;//-1 = null, 0 = planning, 1 = moving, 2 = shooting, 3 = wait, 4 = spawning
    private Vector2 destination;
    private PlayerMovement player;
    private float moveSpeed = 0.15f;
    [SerializeField] private GameObject bulletTemplate;
    [SerializeField] private AudioSource shootSound;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
        }
    }

    void FixedUpdate()
    {
        if (phase == 0)
        {
            destination = player.transform.position + (5 * new Vector3(Random.Range(-1f, 1), Random.Range(-1f, 1), 0).normalized);
            transform.up = new Vector3(destination.x, destination.y, transform.position.z) - transform.position;
            phase = 1;
        }
        else if(phase == 1)
        {
            if(Vector3.Distance(player.transform.position, transform.position) > 15)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * 10);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed);
            }            
            if(Vector3.Distance(transform.position, destination) <= 0.1f)
            {
                phase = 2;
            }
        }
        else if(phase == 2)
        {
            transform.up = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) - transform.position;
            shootSound.Play();
            ShootBullet();
            phase = 3;
        }
        else if(phase == 3)
        {
            phase = -1;//null phase
            Invoke("RestartCycle", 1);
        }
        else if(phase == 4)
        {
            destination = transform.position + (-10 * (player.transform.position - transform.position).normalized);
            transform.up = new Vector3(destination.x, destination.y, transform.position.z) - transform.position;
            phase = 1;
        }
    }

    void RestartCycle()
    {
        destination = Vector2.zero;
        phase = 0;
    }

    private void ShootBullet()
    {
        GameObject newBullet = Instantiate(bulletTemplate);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = transform.rotation;
        newBullet.transform.localScale = bulletTemplate.transform.lossyScale;
        newBullet.SetActive(true);
        if(newBullet.GetComponent<Bullet>() != null)
        {
            newBullet.GetComponent<Bullet>().Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //damage player on crashing
        if(col.gameObject.CompareTag("Player"))
        {
            if(col.gameObject.GetComponent<PlayerMovement>() != null)
            {
                col.gameObject.GetComponent<PlayerMovement>().TakeDamage();
            }
        }
    }
}
