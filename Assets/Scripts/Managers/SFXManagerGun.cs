using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManagerGun : MonoBehaviour
{
    public AudioSource ShotGun, Pistol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShotGunFired()
    {
        ShotGun.Play();
    }
    public void PistolFired()
    {
        Pistol.Play();
    }
}
