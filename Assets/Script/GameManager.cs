using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager _instant;

    public bool GameOver = false;
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

        InitGame();
    }

    public void InitGame()
    {
        ftime = 0;
        play_time = 0;
        HeroManager._instant.Hero_init();
        GameOver = false;
        SteamVR_Fade.Start(Color.black, 0f);
        Invoke("FadeFromBlack", 3f);
    }

    private void Update()
    {
        if (!GameOver)
        {
            ftime += Time.deltaTime;
            play_time = (int)ftime;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), play_time.ToString());
    }

    public void WinGame()
    {
        StartCoroutine(Game_Over());
    } 

    public IEnumerator Game_Over()
    {
        yield return new WaitForSeconds(5f);
        Invoke("FadeToBlack", 0f);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync("Result");
        yield return new WaitForSeconds(3f);
        Invoke("FadeFromBlack", 0f);
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
