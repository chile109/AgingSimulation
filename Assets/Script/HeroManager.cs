using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour {
    public Image Herohp;
    bool GameOver = false;
    public static int HP = 10;
	// Use this for initialization
	void Start () {
        GameOver = false;
        HP = 10;
	}
	
	// Update is called once per frame
	void Update () {

        Herohp.fillAmount = (float) HP/10 ;

		if(HP == 0 && !GameOver) {
            GameOver = true;
            Debug.Log("GameOver!!");
        }
           
	}

    public static void BeHit()
    {
        HP = HP - 1;
        Debug.Log("HP:" + HP);
    }
}
