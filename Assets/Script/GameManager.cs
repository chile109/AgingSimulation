using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager _instant;
    public BossController _boss;
    public DiologPlayer _owl;

    public int life = 3;
    public float ftime = 0;
    public int play_time = 0;

    private void Start()
    {
        if (_instant == null)
        {
            DontDestroyOnLoad(this.gameObject);
            _instant = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        ftime = 0;
        play_time = 0;

        SteamVR_Fade.Start(Color.black, 0f);
        Invoke("FadeFromBlack", 3f);
    }

    private void Update()
    {
        if (!_boss.Die && _owl.Over)
        {
            ftime += Time.deltaTime;
            play_time = (int)ftime;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), play_time.ToString());
    }

    public void Game_Over()
    {
        Invoke("FadeToBlack", 3f);
        SceneManager.LoadSceneAsync("Result");
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
