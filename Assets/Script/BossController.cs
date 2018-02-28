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
        NowCoroutine = BeAngry(5f);
        StartCoroutine(NowCoroutine);
        Debug.Log("SatrtFight");

        this.transform.position = new Vector3(-10, 0.3f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && NowState == BossState.Angry)
        {
            StopAllCoroutines();
            Hp -= 10;
            Hpbar.fillAmount = (float)Hp / 100;
            Debug.Log("HP:" + Hp);

            NowState = BossState.Injured;
            _Ani.SetTrigger("BeInjured");

            StopAllCoroutines();

            if (Hp > 0)
            {
                NowCoroutine = BeIdle(3f);
                StartCoroutine(NowCoroutine);
            }
            else
                IsDead();
        }
    }

    private IEnumerator BeAngry(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Angry;
        _Ani.SetTrigger("BeAngry");
        Debug.Log("Angry");
        NowCoroutine = Attack(5f);
        StartCoroutine(NowCoroutine);
    }

    private IEnumerator Attack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Attack;
        _Ani.SetTrigger("BeAttack");

        Debug.Log("attack");
        StartCoroutine(Damage());

        if (HeroManager.HP == 0)
        {
            StopAllCoroutines();
            NowCoroutine = BeIdle(0f);
        }
        else
            NowCoroutine = BeIdle(3f);

        StartCoroutine(NowCoroutine);
    }

    private IEnumerator BeIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Idle;
        _Ani.SetTrigger("BeIdle");

        NowCoroutine = BeAngry(5f);
        StartCoroutine(NowCoroutine);
    }

    private void IsDead()
    {
        _Ani.SetTrigger("BeDead");
        Debug.Log("Boss is dead!");
    }

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(1f);
        HeroManager.BeHit();
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
