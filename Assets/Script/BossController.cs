using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Image Hpbar;
    public int Hp;
    BossState NowState;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        Hp = 100;
        NowState = BossState.Idle;
    }

    public void StartFight()
    {
        coroutine = BeAngry(5f);
        StartCoroutine(coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && NowState == BossState.Angry)
        {
            Hp -= 10;
            Hpbar.fillAmount = (float)Hp / 100;
            Debug.Log("HP:" + Hp);
            NowState = BossState.Injured;

            Debug.Log("Boss Injured");
            StopAllCoroutines();

            if (Hp > 0)
            {
                coroutine = BeIdle(3f);
                StartCoroutine(coroutine);
            }
            else
                IsDead();
        }
    }

    private IEnumerator BeAngry(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Angry;
        Debug.Log("Boss Angry");

        coroutine = Attack(5f);
        StartCoroutine(coroutine);
    }

    private IEnumerator Attack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Attack;
        Debug.Log("Boss Attack");

        HeroManager.BeHit();
        if (HeroManager.HP == 0)
        {
            StopAllCoroutines();
            coroutine = BeIdle(0f);
        }
        else
            coroutine = BeIdle(3f);

        StartCoroutine(coroutine);
    }

    private IEnumerator BeIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Idle;
        Debug.Log("Boss Idle");

        coroutine = BeAngry(5f);
        StartCoroutine(coroutine);
    }

    private void IsDead()
    {
        Debug.Log("Boss is dead!");
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
