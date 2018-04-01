using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public BoardManager _board;
    public Texture[] WinTexure;
    public Texture[] FailTexure;
    public Renderer _render;
    private int id;

    private void Start()
    {
        id = 0;
    }

    private void Update()
    {
        var device = SteamVR_Controller.Input((int)HeroManager._instant.trackedObj.index);

        if (GameManager._instant.GameOver)
        {
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (id < WinTexure.Length - 1)
                {
                    id += 1;
                    _render.material.mainTexture = WinTexure[id];
                }
                else
                {
                    GameManager._instant.life = 3;
                    Destroy(GameManager._instant.gameObject);
                    Destroy(HeroManager._instant.gameObject);
                    _board.SceneToGo = "Story";
                    _board.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (id < FailTexure.Length - 1)
                {
                    id += 1;
                    _render.material.mainTexture = FailTexure[id];
                }
                else
                {
                    _board.SceneToGo = "Game";
                    _board.gameObject.SetActive(true);
                }
            }
        }
    }
}
