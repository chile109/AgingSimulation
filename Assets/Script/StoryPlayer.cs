using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class StoryPlayer : MonoBehaviour
{
    public VideoPlayer _player;
    public GameObject _Tip;
    private void Start()
    {
        GameManager._instant.FadeFromWhite();
        _player.loopPointReached += PlayGame;

    }
    private void Update()
    {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger) || Input.GetKeyDown(KeyCode.Space))
        {
            _Tip.SetActive(false);
            _player.Play();
        }
    }

    private void PlayGame(UnityEngine.Video.VideoPlayer vp)
    {
        _player.Stop();
        GameManager._instant.FadeToBlack();
        StartCoroutine(JumpScene());
    }

    IEnumerator JumpScene()
    {
        Debug.Log("corotine");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("Game");
        GameManager._instant.InitGame();
    }

}
