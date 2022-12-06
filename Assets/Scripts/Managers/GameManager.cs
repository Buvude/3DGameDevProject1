using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameManager :Manager<GameManager>
{

    public Action OnPause;// event to be subscribed to 
    public Action OnUnPause;

    private bool paused;

    public GameObject pistolImgHolder, ShotgunImgHolder;
    [HideInInspector]
    public List<GameObject> pistolAmmoImages,shotGunAmmoImages;

    private void Start()
    {
        //grab references to images for UI
        foreach (RectTransform child in pistolImgHolder.transform)
            pistolAmmoImages.Add(child.gameObject);
        foreach (RectTransform child in ShotgunImgHolder.transform)
            shotGunAmmoImages.Add(child.gameObject);
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

    public void updateAmmoUI(int RevolverAmmo,int ShotGunAmmo)
    {
        //change display for revolver
        for (int i = 0; i < 6; i++)
        {
            if (i < RevolverAmmo)
            {
                //display that ammo img
                pistolAmmoImages[i].GetComponent<Image>().enabled =true;
            }
            else
                pistolAmmoImages[i].GetComponent<Image>().enabled = false;
        }

        //update shotty gun
        for (int i = 0; i < 2; i++)
        {
            if (i < ShotGunAmmo)
            {
                //display that ammo img
               shotGunAmmoImages[i].GetComponent<Image>().enabled = true;
            }
            else
                shotGunAmmoImages[i].GetComponent<Image>().enabled = false;
        }
    }
}
