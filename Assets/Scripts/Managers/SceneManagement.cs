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
        if (curScene.buildIndex == 0)
        {
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curScene.buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (curScene.buildIndex == 0)
        {
            if (stats.CurHealth <= 0)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
