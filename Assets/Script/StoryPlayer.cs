using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Text))]
public class StoryPlayer : MonoBehaviour
{

    public string FileName;
    public  Text _Lang;

    private Object[] pages;
    private Image _player;
    private int id;

    public SteamVR_TrackedObject trackedObj;

    private void Awake()
    {
        pages = Resources.LoadAll("Openning", typeof(Sprite));
    }

    void Start()
    {

        _player = this.GetComponent<Image>();

        id = 0;

        _player.sprite = (Sprite)pages[0];
    }

    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (id < pages.Length - 1)
            {
                id += 1;
                _player.sprite = (Sprite)pages[id];
            }

            else
                _Lang.text = "End";
        }
    }


}
