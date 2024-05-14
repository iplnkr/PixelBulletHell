using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCounter : MonoBehaviour
{
    private TextMesh planets;
    private int total = 10;
    private int current = 0;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMesh damageTaken;
    [SerializeField] private TextMesh timesHit;

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
        //win game
        if(current == total)
        {
            timesHit.text = "You were hit " + damageTaken.text + " times";
            EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
            for(int i = enemies.Length - 1; i >= 0; i--)
            {
                Destroy(enemies[i].gameObject);
            }
            winPanel.SetActive(true);
        }
    }
}
