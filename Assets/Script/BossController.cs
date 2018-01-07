using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public int Hp;
    BossState NowState;

    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        Hp = 100;
        NowState = BossState.Idle;

        coroutine = BeAngry(5f);
        StartCoroutine(coroutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon" && NowState == BossState.Angry)
        {
            Hp -= 10;
            Debug.Log("HP:" + Hp);
            NowState = BossState.Injured;

            Debug.Log("Boss Injured");
            StopAllCoroutines();
            coroutine = BeIdle(3f);
            StartCoroutine(coroutine); ;
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
}

public enum BossState
{
    Idle,
    Angry,
    Attack,
    Injured,
    Dead,
}
