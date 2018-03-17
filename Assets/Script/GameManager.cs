using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public BossController _boss;
    public HeroManager _hero;
    public DiologPlayer _owl;

    float ftime = 0;
    int play_time = 0;

    private void Start()
    {
        ftime = 0;
        play_time = 0;
    }

    private void Update()
    {
        if (!_boss.Die)
        {
            ftime += Time.deltaTime;
            play_time = (int)ftime;
        }
    }

    void T_ScoreCaculate()
    {
        if(play_time < 60)
        {
            Debug.Log("T_Grade:S");
        }

        if (play_time >= 60 && play_time < 70)
        {
            Debug.Log("T_Grade:A");
        }

        if (play_time >= 70 && play_time < 80)
        {
            Debug.Log("T_Grade:B");
        }

        if (play_time >= 80 && play_time < 90)
        {
            Debug.Log("T_Grade:C");
        }

        if (play_time >= 90 )
        {
            Debug.Log("T_Grade:D");
        }
    }

    void H_ScoreCaculate()
    {
        if (_hero.HP == 10)
        {
            Debug.Log("H_Grade:S");
        }

        if (play_time >= 8 && play_time < 10)
        {
            Debug.Log("H_Grade:A");
        }

        if (play_time >= 6 && play_time < 8)
        {
            Debug.Log("H_Grade:B");
        }

        if (play_time >= 4 && play_time < 6)
        {
            Debug.Log("H_Grade:C");
        }

        if (play_time < 4)
        {
            Debug.Log("H_Grade:D");
        }
    }
}
