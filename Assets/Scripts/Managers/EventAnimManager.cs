using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimManager : MonoBehaviour
{
    public List<Enemy> enemyList = new List<Enemy>();
    public Animator Door;
    public Camera MainCam, CutsceneCam;
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
        foreach (Enemy enemyInstance in enemyList)
        {
            enemyInstance.paused = true;
        }
    }

    public void Unpause()
    {
        foreach (Enemy enemyInstance in enemyList)
        {
            enemyInstance.paused = false;
        }
    }

    public void exitOpen()
    {
        Pause();
       /* Time.deltaTime.Equals(0f);*/
        MainCam.gameObject.GetComponent<Camera>().enabled = false;
        CutsceneCam.GetComponent<Camera>().enabled = true;
        Door.SetTrigger("DoorOpen");
    }
    public void exitOpenEndCutscene()
    {
        Unpause();
        MainCam.gameObject.GetComponent<Camera>().enabled = true;
        CutsceneCam.GetComponent<Camera>().enabled = false;
        Time.deltaTime.Equals(.02f);
    }
}
