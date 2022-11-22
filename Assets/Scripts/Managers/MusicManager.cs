using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource BasePlayer, bGMPlayer, FranticBGMPlayer;
    // Start is called before the first frame update
    void Start()
    {
        BasePlayer.volume = .3f;
        bGMPlayer.volume = .3f;
        FranticBGMPlayer.volume = .3f;
    }

    // Update is called once per frame
    void Update()
    {
        //COMMENT OUT OF FINAL BUILD, THIS IS FOR TESTING PURPOSES ONLY
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            backToNormal();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            CombatTrack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            FranticTrack();
        }
    }

    //hard switch for music TODO: possibly add a fading switch
    public void backToNormal()
    {
        bGMPlayer.mute = true;
        FranticBGMPlayer.mute = true;
    }

    public void CombatTrack()
    {
        bGMPlayer.mute = false;
        FranticBGMPlayer.mute = true;
    }

    public void FranticTrack()
    {
        bGMPlayer.mute = false;
        FranticBGMPlayer.mute = false;
    }
}
