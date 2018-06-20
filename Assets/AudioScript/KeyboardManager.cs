﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioManager.SFX_ES.Trigger("bgm01");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.BGM_ES.Trigger("BGM02");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.SFX_ES.Trigger("Bark");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.SFX_ES.Trigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.BTN_ES.Trigger("BTN01");
        } 
        if (Input.GetKeyDown(KeyCode.X))
        {
            AudioManager.BTN_ES.Trigger("BTN02");
        }
    }
}
