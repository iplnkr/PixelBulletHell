using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private bool isMainMenu = false;

    // Update is called once per frame
    void Update()
    {
        if(isMainMenu)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene("GameplayScene");
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
