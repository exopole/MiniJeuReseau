﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InactivateByTime : MonoBehaviour
{

    public float lifetime;
    public bool inactivateInStart = false;

    void Start()
    {
        if(inactivateInStart)
            Invoke("inactivate", lifetime);
    }

    // Use this for initialization
    public void InactivateWithlifeTime()
    {
        Invoke("inactivate", lifetime);
    }

    public virtual void inactivate()
    {
        gameObject.SetActive(false);
    }


}

