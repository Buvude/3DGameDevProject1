using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PauseEvent : MonoBehaviour
{
    public Action OnPause;
    public delegate void Notify();

    public event Notify PauseGame; // event

    private void Update()
    {
       
    }

   

}
