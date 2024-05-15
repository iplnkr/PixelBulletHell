using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunBulletLauncher : MonoBehaviour
{
    [SerializeField] private GameObject bulletTemplate;
    [SerializeField] private float cooldown = 0.75f;
    [SerializeField] private float angle = -15;
    private float timer = 0;

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if(timer <= 0)
        {
            timer = cooldown;
            transform.Rotate(0, 0, 120);
            ShootBullet();
            transform.Rotate(0, 0, 120);
            ShootBullet();
            transform.Rotate(0, 0, 120);            
            ShootBullet();
            transform.Rotate(0, 0, angle);
        }
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
}
