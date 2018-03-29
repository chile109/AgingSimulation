using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager _instant;
    public BossController _boss;
    public HeroManager _hero;
    public DiologPlayer _owl;

    float ftime = 0;
    int play_time = 0;

    private void Awake()
    {
        _instant = this;
    }
    private void Start()
    {
        ftime = 0;
        play_time = 0;

        SteamVR_Fade.Start(Color.black, 0f);
        Invoke("FadeFromBlack", 3f);
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
        if (play_time < 60)
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

        if (play_time >= 90)
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

    public void Game_Over()
    {
        Invoke("FadeToBlack", 3f);
        //scene pass
        SceneManager.LoadSceneAsync("");
    }
    private void FadeToBlack()
    {
        SteamVR_Fade.Start(Color.black, 3f);
    }

    private void FadeToWhite()
    {
        SteamVR_Fade.Start(Color.white, 3f);
    }

    private void FadeFromBlack()
    {
        //set start color
        SteamVR_Fade.Start(Color.black, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.clear, 3f);
    }
    private void FadeFromWhite()
    {
        //set start color
        SteamVR_Fade.Start(Color.white, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.clear, 3f);
    }
}
