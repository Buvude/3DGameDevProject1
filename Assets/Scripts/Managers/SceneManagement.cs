using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    Scene curScene;
    PlayerStats stats;
    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        curScene = SceneManager.GetActiveScene();
        string sceneName = curScene.name;
        if (curScene.buildIndex == 1)
        {
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curScene.buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (curScene.buildIndex == 1)
        {
            if (stats.CurHealth <= 0)
            {
                SceneManager.LoadScene(2);
            }
        }
    }
}
