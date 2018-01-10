using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Text))]
public class StoryPlayer : MonoBehaviour
{
    public Text _Lang;

    public Object[] pages;
    private Image _player;
    private int id;

    public SteamVR_TrackedObject trackedObj;
    public GameObject HeroCamera;
    public GameObject HeroUI;

    public DiologPlayer Owl;

    private void Awake()
    {
        Owl.enabled = false;
        HeroCamera.SetActive(false);
        pages = Resources.LoadAll("StoryBoard/Third/Sequence", typeof(Sprite));
        StoryInit();
    }

    public void GetStory(string Name)
    {
        pages = Resources.LoadAll(Name, typeof(Sprite));
        StoryInit();
    }

    void StoryInit()
    {
        HeroUI.SetActive(false);
        _player = this.GetComponent<Image>();
        id = 0;
        _player.sprite = (Sprite)pages[id];
        _Lang.text = KernelData.Sequence3[id];
    }

    void Update()
    {
        Debug.Log(id);
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (id < pages.Length - 1)
            {
                id += 1;
                _player.sprite = (Sprite)pages[id];
                _Lang.text = KernelData.Sequence3[id];
            }

            else
            {
                _Lang.text = "End";
                HeroUI.SetActive(true);
                HeroCamera.SetActive(true);
                this.gameObject.SetActive(false);
                Owl.enabled = true;
            }
        }
    }


}
