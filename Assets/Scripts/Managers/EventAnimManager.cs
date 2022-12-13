using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimManager : MonoBehaviour
{
    public ThirdPersonMovement playerScript;
/*    public List<Enemy> enemyList = new List<Enemy>();
*/    public Animator Door;
    public Camera MainCam, CutsceneCam;
    internal static bool doorOpen = false;
    public SceneManagement SM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
    }
    public void Pause()
    {
        /* foreach (Enemy enemyInstance in enemyList)
         {
             playerScript.playerPaused = true;
             enemyInstance.paused = true;
         }*/
        GameManager.Instance.Pause();
    }

    public void Unpause()
    {
        /*foreach (Enemy enemyInstance in enemyList)
        {
            playerScript.playerPaused = true;
            enemyInstance.paused = false;
        }*/
        GameManager.Instance.UnPause();
    }

    public void exitOpen()
    {
        Pause();
        doorOpen = true;
        /* Time.deltaTime.Equals(0f);*/
        MainCam.gameObject.GetComponent<Camera>().enabled = false;
        CutsceneCam.GetComponent<Camera>().enabled = true;
        CutsceneCam.GetComponent<AudioListener>().enabled = true;
        MainCam.gameObject.GetComponent<AudioListener>().enabled = false;
        CutsceneCam.GetComponent<AudioSource>().Play();
        Door.SetTrigger("DoorOpen");
    }
    public void exitOpenEndCutscene()
    {
        Unpause();
        /*doorOpen = true;*/
        MainCam.gameObject.GetComponent<Camera>().enabled = true;
        CutsceneCam.GetComponent<Camera>().enabled = false;
        CutsceneCam.GetComponent<AudioListener>().enabled = false;
        MainCam.gameObject.GetComponent<AudioListener>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player") && doorOpen)
        {
            print("Player has entered");
            GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreManager>().vicotry = true;
            SM.endRun();
        }
    }

}
