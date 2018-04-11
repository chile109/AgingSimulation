using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{

    public Image[] _star;
    public Sprite[] _evaluation;
    public string[] _Context;
    public Image _bey;
    public Text _log;
    public Text HP_value;
    public Text Time_value;

    public string SceneToGo = "";
    private int clickOrder = 0;
    private bool AvoidTwice = false;
    private int starCount = 0;
    private Color NewCol;

    void Start()
    {
        clickOrder = 0;
        starCount = T_ScoreCaculate() + H_ScoreCaculate();
        _bey.sprite = null;
        AvoidTwice = false;
        ShowScore();
    }

    // Update is called once per frame
    void Update()
    {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            clickOrder += 1;

            if (clickOrder == 1 && !AvoidTwice)
            {
                AvoidTwice = true;
                StartCoroutine(ShowStar());  
            }

            else if (clickOrder == 2 && !AvoidTwice)
            {
                AvoidTwice = true;
                StartCoroutine(ShowBey());
            }
            else if (clickOrder == 3 && !AvoidTwice)
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

        AvoidTwice = false;
    }

    IEnumerator ShowBey()
    {
        float elapsedTime = 0;

        _bey.enabled = true;
        _bey.sprite = _evaluation[starCount];

        Vector3 InitialScale = _bey.transform.localScale;
        NewCol = _bey.color;

        while (elapsedTime < 1.5f)
        {   
            _bey.transform.localScale = Vector3.Lerp(InitialScale, Vector3.one, 1.5f);
            NewCol.a = Mathf.Lerp(0, 1f, (elapsedTime / 1.5f));
            _bey.color = NewCol;

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(ShowLog());
    }

    IEnumerator ShowLog()
    {
        float elapsedTime = 0;

        if (SceneToGo == "Game")
            _log.text = "別灰心!你還有" + GameManager._instant.life + "次機會~";
        else if (GameManager._instant.life == 0)
            _log.text = "挑戰失敗，重頭再來吧!";
        else
            _log.text = _Context[starCount - 1];

        NewCol = _log.color;
        while (elapsedTime < 1.5f)
        {
            NewCol.a = Mathf.Lerp(0, 1f, (elapsedTime / 1.5f));
            _log.color = NewCol;

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        AvoidTwice = false;
    }

    int T_ScoreCaculate()
    {
        int time_star = 0;
        if (GameManager._instant.play_time < 120)
        {
            time_star = 2;
        }

        if (GameManager._instant.play_time >= 120 && GameManager._instant.play_time < 150)
        {
            time_star = 1;
        }

        if (GameManager._instant.play_time >= 150)
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
