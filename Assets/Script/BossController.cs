using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    Animator _Ani;
    public Image Hpbar;
    public int Hp;
    public BossState NowState;
    public GameObject render;

    private IEnumerator NowCoroutine;

    // Use this for initialization
    void Start()
    {
        _Ani = this.GetComponent<Animator>();
        Hp = 100;
        NowState = BossState.Idle;
        
    }

    public void StartFight()
    {
        NowCoroutine = BeAngry(3f);
        StartCoroutine(NowCoroutine);
        Debug.Log("SatrtFight");

        gameObject.transform.position = Radius_Position();
        gameObject.transform.LookAt(new Vector3(HeroManager._instant.transform.position.x, 0, HeroManager._instant.transform.position.z));
        transform.Rotate(Vector3.up, -10f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && NowState == BossState.Angry)
        {
            StopCoroutine(NowCoroutine);
            Hp -= 10;
            Hpbar.fillAmount = (float)Hp / 100;
            Debug.Log("HP:" + Hp);

            NowState = BossState.Injured;
            _Ani.SetTrigger("BeInjured");
            AudioManager.SFX_ES.Trigger("Attack");
            if (Hp == 0)
                BeDead();

        }
    }

    private IEnumerator BeAngry(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Angry;
        _Ani.SetTrigger("BeAngry");
        AudioManager.SFX_ES.Trigger("Bark");
        NowCoroutine = Attack(3f);
        StartCoroutine(NowCoroutine);
    }

    private IEnumerator Attack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Attack;
        _Ani.SetTrigger("BeAttack");
        AudioManager.SFX_ES.Trigger("Injure");
        if (HeroManager._instant.HP == 0)
        {
            StopAllCoroutines();
        }
    }

    private void BeIdle()
    {
        Debug.Log("beIdle");
        NowState = BossState.Idle;
        _Ani.SetTrigger("BeIdle");
        render.SetActive(false);
        StartCoroutine(ShowUp());
    }

    public IEnumerator ShowUp()
    {
        gameObject.transform.position = Radius_Position();
        gameObject.transform.LookAt(new Vector3(HeroManager._instant.transform.position.x, 0, HeroManager._instant.transform.position.z));
        transform.Rotate(Vector3.up, -5f, Space.World);
        yield return new WaitForSeconds(2f);
        render.SetActive(true);
        NowCoroutine = BeAngry(3f);
        StartCoroutine(NowCoroutine);
    }

    private void BeDead()
    {
        _Ani.SetTrigger("BeDead");
        GameManager._instant.WinGame();
    }

    private void Damage()
    {
        HeroManager._instant.BeHit();
    }

    private Vector3 Radius_Position()
    {
        int _radius = 7;
        var degrees = Random.Range(0, 360);

        var x = _radius * Mathf.Cos(degrees * Mathf.Deg2Rad);
        var y = _radius * Mathf.Sin(degrees * Mathf.Deg2Rad);

        Debug.Log(new Vector3(x, 0, y));
        return new Vector3(x, 0, y);
    }
}

public enum BossState
{
    Idle,
    Angry,
    Attack,
    Injured,
    Dead,
}
