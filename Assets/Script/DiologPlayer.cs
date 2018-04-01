using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiologPlayer : MonoBehaviour {
    public BossController _boss;
    public Text Owlsaying;
    public bool Over = false;
    public string[] Dialog;
   
    private int id;

    void Start () {
        id = 0;
        Over = false;
        Owlsaying.text = Dialog[id];
    }
	
	// Update is called once per frame
	void Update () {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (id < Dialog.Length - 1)
            {
                id += 1;
                Owlsaying.text = Dialog[id];
            }

            else
            {
                Over = true;
                this.gameObject.SetActive(false);
                _boss.StartFight();
            }
        }
    }
}
