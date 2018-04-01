using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class StoryPlayer : MonoBehaviour
{
    public VideoPlayer _player;
    private void Start()
    {
        GameManager._instant.FadeFromWhite();
        _player.loopPointReached += PlayGame;

    }
    private void Update()
    {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            _player.Play();
        }
    }

    private void PlayGame(UnityEngine.Video.VideoPlayer vp)
    {
        GameManager._instant.FadeToBlack();
        StartCoroutine(JumpScene());
    }

    IEnumerator JumpScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync("Game");
        GameManager._instant.InitGame();
    }

}
