using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCounter : MonoBehaviour
{
    private TextMesh planets;
    private int total = 8;
    private int current = 0;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMesh damageTaken;
    [SerializeField] private TextMesh timesHit;

    //sun time
    [SerializeField] private TextMesh sunMessage;
    [SerializeField] private GameObject oldSun;
    [SerializeField] private GameObject newSun;
    [SerializeField] private GameObject hud;

    // Start is called before the first frame update
    void Start()
    {
        planets = GetComponent<TextMesh>();
        planets.text = current + "/" + total;
    }

    public void UpdateNumber()
    {
        current++;
        planets.text = current + "/" + total;
        //switch to drinkable sun
        if(current == total)
        {
            sunMessage.text = "New Goal: Drink The Sun";
            sunMessage.gameObject.SetActive(true);
            oldSun.SetActive(false);
            newSun.SetActive(true);
            Invoke("HideMessage", 2);
        }
        else if (current == total + 1)//if win
        {
            planets.text = total + "/" + total;
            timesHit.text = "You were hit " + damageTaken.text + " times";
            //destroy all enemies
            EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
            for(int i = enemies.Length - 1; i >= 0; i--)
            {
                Destroy(enemies[i].gameObject);
            }
            Invoke("WinGame", 5);
        }
    }

    void HideMessage()
    {
        sunMessage.gameObject.SetActive(false);
    }

    void WinGame()
    {
        //destroy bullets
        Bullet[] bulls = FindObjectsOfType<Bullet>();
        for(int i = bulls.Length - 1; i >= 0; i--)
        {
            Destroy(bulls[i].gameObject);
        }
        winPanel.SetActive(true);
        hud.SetActive(false);
    }
}
