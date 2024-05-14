using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWater : MonoBehaviour
{
    private bool drinking = false;
    private bool canDrink = true;
    [SerializeField] private int[] thresholds;
    [SerializeField] private GameObject enemyTemplate;
    private float maxScale;
    private float scalePercentage;
    [SerializeField] private GameObject planet;
    [SerializeField] private GameObject bulletTemplate;

    [SerializeField] private GameObject core;
    [SerializeField] private Material deadPlanetMat;

    [SerializeField] private GameObject wavesObject;
    [SerializeField] private GameObject explodeObject;
    [SerializeField] private Animator expAnim;

    // Start is called before the first frame update
    void Start()
    {
        maxScale = (transform.localScale.x - 1);
        scalePercentage = ((transform.localScale.x - 1) / maxScale) * 100;
        //Application.targetFrameRate = 60;//TODO remove
    }


    void FixedUpdate()
    {
        if((drinking) && (canDrink))
        {
            transform.localScale = transform.localScale - (Vector3.one * 1f  * Time.fixedDeltaTime);
            scalePercentage = ((transform.localScale.x - 1) / maxScale) * 100;
            //check if any spawning thresholds met
            for(int i = 0; i < thresholds.Length; i++)
            {
                if(scalePercentage <= thresholds[i])
                {
                    thresholds[i] = -100;
                    GameObject newEnemy = Instantiate(enemyTemplate);
                    newEnemy.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
                    newEnemy.SetActive(true);
                }
            }
            if(transform.localScale.x <= 1.01)
            {
                canDrink = false;
                core.GetComponent<Renderer>().material = deadPlanetMat;
                StartCoroutine(Explode());
            }
        }
    }

    public void Drink()
    {
        drinking = true;
    }

    public void Undrink()
    {
        drinking = false;
    }

    private IEnumerator Explode()//planet exploding animation
    {
        FindObjectOfType<PlanetCounter>().UpdateNumber();
        float numToFire = 6;
        yield return new WaitForSeconds(1f);

        //explode anim
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        wavesObject.GetComponent<MeshRenderer>().enabled = false;
        core.GetComponent<MeshRenderer>().enabled = false;
        explodeObject.SetActive(true);
        if(expAnim != null)
        {
            if(expAnim.HasState(0, Animator.StringToHash("Base Layer.ExplodeAnim")))
            {
                //core.SetActive(false);
                expAnim.Play("Base Layer.ExplodeAnim");
            }
        }

        //shoot bullets around
        for(int i = 0; i < numToFire; i++)
        {
            ShootBullet(Quaternion.Euler(0, 0, (0 * 120 / numToFire) + (360 / numToFire) * i));
        }
        yield return new WaitForSeconds(0.15f);
        for(int i = 0; i < numToFire; i++)
        {
            ShootBullet(Quaternion.Euler(0, 0, (2 * 120 / numToFire) + (360 / numToFire) * i));
        }
        yield return new WaitForSeconds(0.15f);
        for(int i = 0; i < numToFire; i++)
        {
            ShootBullet(Quaternion.Euler(0, 0, (4 * 120 / numToFire) + (360 / numToFire) * i));
        }
        yield return new WaitForSeconds(0.15f);
        for(int i = 0; i < numToFire; i++)
        {
            ShootBullet(Quaternion.Euler(0, 0, (0 * 120 / numToFire) + (360 / numToFire) * i));
        }
        yield return new WaitForSeconds(0.15f);
        for(int i = 0; i < numToFire; i++)
        {
            ShootBullet(Quaternion.Euler(0, 0, (2 * 120 / numToFire) + (360 / numToFire) * i));
        }
        yield return new WaitForSeconds(0.15f);
        for(int i = 0; i < numToFire; i++)
        {
            ShootBullet(Quaternion.Euler(0, 0, (4 * 120 / numToFire) + (360 / numToFire) * i));
        }
        Destroy(planet);
    }

    private void ShootBullet(Quaternion direction)
    {
        GameObject newBullet = Instantiate(bulletTemplate);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = direction;
        newBullet.transform.localScale = bulletTemplate.transform.lossyScale;
        newBullet.SetActive(true);
        if(newBullet.GetComponent<Bullet>() != null)
        {
            newBullet.GetComponent<Bullet>().Shoot();
        }
    }
}
