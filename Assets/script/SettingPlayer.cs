using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPlayer : MonoBehaviour {

    public static SettingPlayer instance;

    public bool isSolo = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
