using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public BoardManager _board;
    public Texture[] WinTexure;
    public Texture[] FailTexure;
    public Renderer _render;

    private int id;
    private Texture[] _tmpTexture;

    private void Start()
    {
        id = 0;

        if (!GameManager._instant.GameOver)
        {
            GameManager._instant.GameReset();
            _tmpTexture = WinTexure;
            _board.SceneToGo = "Story";
        }
        else if (GameManager._instant.life == 0)
        {
            GameManager._instant.GameReset();
            _tmpTexture = FailTexure;
            _board.SceneToGo = "Story";
        }
        else
        {
            _tmpTexture = FailTexure;
            _board.SceneToGo = "Game";
        }

        _render.material.mainTexture = _tmpTexture[0];
    }

    private void Update()
    {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (id < _tmpTexture.Length - 1)
            {
                id += 1;
                _render.material.mainTexture = _tmpTexture[id];
            }
            else
            {
                if (!GameManager._instant.GameOver)
                {
                    _render.material.mainTexture = null;
                }
                _board.gameObject.SetActive(true);
            }
        }
    }
}
