using UnityEngine.SceneManagement;
using UnityEngine;

public class ResultManager : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj;
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
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        if (GameManager._instant._boss.Die)
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
                    T_ScoreCaculate();
                    H_ScoreCaculate();
                    _render.material.mainTexture = null;
                    SceneManager.LoadSceneAsync("Game");
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
                    T_ScoreCaculate();
                    H_ScoreCaculate();
                    _render.material.mainTexture = null;
                    SceneManager.LoadSceneAsync("Game");
                }
            }
        }
    }

    void T_ScoreCaculate()
    {
        if (GameManager._instant.play_time < 60)
        {
            Debug.Log("T_Grade:S");
        }

        if (GameManager._instant.play_time >= 60 && GameManager._instant.play_time < 70)
        {
            Debug.Log("T_Grade:A");
        }

        if (GameManager._instant.play_time >= 70 && GameManager._instant.play_time < 80)
        {
            Debug.Log("T_Grade:B");
        }

        if (GameManager._instant.play_time >= 80 && GameManager._instant.play_time < 90)
        {
            Debug.Log("T_Grade:C");
        }

        if (GameManager._instant.play_time >= 90)
        {
            Debug.Log("T_Grade:D");
        }
    }

    void H_ScoreCaculate()
    {
        if (GameManager._instant._hero.HP == 10)
        {
            Debug.Log("H_Grade:S");
        }

        if (GameManager._instant._hero.HP >= 8 && GameManager._instant._hero.HP < 10)
        {
            Debug.Log("H_Grade:A");
        }

        if (GameManager._instant._hero.HP >= 6 && GameManager._instant._hero.HP < 8)
        {
            Debug.Log("H_Grade:B");
        }

        if (GameManager._instant._hero.HP >= 4 && GameManager._instant._hero.HP < 6)
        {
            Debug.Log("H_Grade:C");
        }

        if (GameManager._instant._hero.HP < 4)
        {
            Debug.Log("H_Grade:D");
        }
    }
}
