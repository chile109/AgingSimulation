using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour {

    public static HeroManager _instant;
    public Text Herohp;
    bool GameOver = false;
    public int HP = 10;
	// Use this for initialization
	void Start () {
        _instant = this;
        GameOver = false;
        HP = 10;
	}
	
	// Update is called once per frame
	void Update () {

        Herohp.text = (HP*10).ToString();

		if(HP == 0 && !GameOver) {
            GameOver = true;
            GameManager._instant.Game_Over();
        }
           
	}

    public void BeHit()
    {
        HP = HP - 1;
        Debug.Log("HP:" + HP);
    }
}
