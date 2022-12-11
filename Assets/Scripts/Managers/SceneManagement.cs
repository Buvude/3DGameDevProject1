using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagement : MonoBehaviour
{
    public ScoreManager scoMan;
    Scene curScene;
    PlayerStats stats;
    private bool startedFinalCalc=false;
    string sceneName;
    public TextMeshProUGUI Line1, Line2, Line3, Line4;

    // Start is called before the first frame update
    void Start()
    {
        scoMan = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreManager>();
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
        /* if (curScene.buildIndex == 2)
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
         }*/
        if (curScene.buildIndex == 1)
        {
            if (stats.CurHealth <= 0)
            {
                scoMan.vicotry = false;
                SceneManager.LoadScene(2);
            }
            
            //Testing purposes only, comment out or delete before fianl build
            if (Input.GetKey(KeyCode.V)&& Input.GetKey(KeyCode.I)&& Input.GetKey(KeyCode.C))
            {
                scoMan.vicotry = true;
                SceneManager.LoadScene(2);
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.A))
            {
                scoMan.vicotry = false;
                SceneManager.LoadScene(2);
            }
            //end of testing stuff
        }
        if (curScene.buildIndex == 2)
        {
            if (!startedFinalCalc)
            {
                startedFinalCalc = true;
                scoMan.startFinalCalcMethod(Line1, Line2, Line3, Line4);
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
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
