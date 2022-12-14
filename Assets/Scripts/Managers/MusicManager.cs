using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public bool doorOpened = false, franticPlaying = false;
    private int enemiesInCloseQuarters;
    public AudioSource BasePlayer, bGMPlayer, FranticBGMPlayer;
    // Start is called before the first frame update
    void Start()
    {
        BasePlayer.volume = .3f;
        bGMPlayer.volume = .0f;
        bGMPlayer.mute = false;
        FranticBGMPlayer.volume = .0f;
        FranticBGMPlayer.mute = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer.ToString() == "Enemy")
        {
            if (enemiesInCloseQuarters == 0 &&!doorOpened)
            {
                CombatTrack();
            }
            enemiesInCloseQuarters++;
            Debug.Log("+"+enemiesInCloseQuarters);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer.ToString() == "Enemy")
        {
            if (enemiesInCloseQuarters == 0 &&!doorOpened)
            {
                backToNormal();
            }
        }
    }

    //hard switch for music TODO: possibly add a fading switch
    public void backToNormal()
    {
        /*bGMPlayer.mute = true;
        FranticBGMPlayer.mute = true;*/
        StartCoroutine("BackToNormalFade");
    }

    public void CombatTrack()
    {
        /* bGMPlayer.mute = false;
         FranticBGMPlayer.mute = true;*/
        StartCoroutine("CombatTrackFadeIn");
    }

    public void FranticTrack()
    {
        /* bGMPlayer.mute = false;
         FranticBGMPlayer.mute = false;*/
        StartCoroutine("FranticTrackFadeIn");
    }
    IEnumerator CombatTrackFadeIn()
    {
        while (bGMPlayer.volume < .30f)
        {
            bGMPlayer.volume += .01f;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator BackToNormalFade()
    {
       
        while (bGMPlayer.volume > .00f)
        {
            bGMPlayer.volume -= .01f;
            yield return new WaitForEndOfFrame();
        }
       
    }

    IEnumerator FranticTrackFadeIn()
    {
        while (bGMPlayer.volume < .30f)
        {
            bGMPlayer.volume += .01f;
            yield return new WaitForEndOfFrame();
        }
        while (FranticBGMPlayer.volume < .30f)
        {
            FranticBGMPlayer.volume += .01f;
            yield return new WaitForEndOfFrame();
        }
    }
}
