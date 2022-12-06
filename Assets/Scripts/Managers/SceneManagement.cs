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
                startGame();
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                quitGame();
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
    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void endRun()
    {
        SceneManager.LoadScene(2);
    }
}
