using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager :Manager<GameManager>
{

    public Action OnPause;// event to be subscribed to 
    public Action OnUnPause;

    private bool paused;

    private void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)&&!paused)
        {
            OnPauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.P) && paused)
        {
            OnUnPauseGame();
        }
    }

    protected virtual void OnPauseGame() //protected virtual method
    {
        //if onpause is not null then call delegate
        OnPause?.Invoke();
        paused = true;
     
    }
    protected virtual void OnUnPauseGame()
    {
        //if onunpause is not null then call delegate
        OnUnPause?.Invoke();

        paused = false;
      
    }

    public void Pause()
    {
        OnPauseGame();
    }
    public void UnPause()
    {
        OnUnPauseGame();
    }
}
