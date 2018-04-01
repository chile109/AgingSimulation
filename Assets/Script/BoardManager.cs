using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{

    public Image[] _star;
    public Sprite[] _evaluation;
    public Image _log;
    public Text HP_value;
    public Text Time_value;

    public string SceneToGo = "";
    private int clickOrder = 0;
    private bool StarShowed = false;
    private int starCount = 0;

    void Start()
    {
        clickOrder = 0;
        starCount = T_ScoreCaculate() + H_ScoreCaculate();
        _log.sprite = null;
        StarShowed = false;
        ShowScore();
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            clickOrder += 1;

            if (clickOrder == 1)
            {
                _log.enabled = true;
                _log.sprite = _evaluation[starCount];
            }

            else if (clickOrder == 2 && !StarShowed)
            {
                StartCoroutine(ShowStar());
                StarShowed = true;
            }

            else if (clickOrder == 3)
            {
                if (SceneToGo == "Game")
                    GameManager._instant.InitGame();
                else
                    GameManager._instant.FadeToWhite();

                SceneManager.LoadSceneAsync(SceneToGo);
            }
        }
    }


    void ShowScore()
    {
        HP_value.text = (HeroManager._instant.HP * 10).ToString();
        int minute = (int)GameManager._instant.play_time / 60;
        int second = GameManager._instant.play_time % 60;
        Time_value.text = minute + ":" + second;
    }

    IEnumerator ShowStar()
    {
        for (int i = 0; i < starCount; i++)
        {
            yield return new WaitForSeconds(1f);
            _star[i].enabled = true;
        }
    }

    int T_ScoreCaculate()
    {
        int time_star = 0;
        if (GameManager._instant.play_time < 70)
        {
            time_star = 2;
        }

        if (GameManager._instant.play_time >= 70 && GameManager._instant.play_time < 90)
        {
            time_star = 1;
        }

        if (GameManager._instant.play_time >= 90)
        {
            time_star = 0;
        }

        return time_star;
    }

    int H_ScoreCaculate()
    {
        int HP_star = 0;
        if (HeroManager._instant.HP > 8)
        {
            HP_star = 2;
        }

        if (HeroManager._instant.HP > 4 && HeroManager._instant.HP <= 8)
        {
            HP_star = 1;
        }

        if (HeroManager._instant.HP <= 3)
        {
            HP_star = 0;
        }

        return HP_star;
    }
}
