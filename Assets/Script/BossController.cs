using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    Animator _Ani;
    public Image Hpbar;
    public int Hp;
    public bool Die = false;
    public BossState NowState;
    public SkinnedMeshRenderer render;
    public Transform _Hero;

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

        gameObject.transform.position = Radius_Position();
        gameObject.transform.LookAt(new Vector3(_Hero.position.x, 0, _Hero.position.z));
        transform.Rotate(Vector3.up, -10f, Space.World);
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
                NowCoroutine = BeIdle(2f);
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

        NowCoroutine = Attack(5f);
        StartCoroutine(NowCoroutine);
    }

    private IEnumerator Attack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Attack;
        _Ani.SetTrigger("BeAttack");

        StartCoroutine(Damage());

        if (HeroManager._instant.HP == 0)
        {
            StopAllCoroutines();
            NowCoroutine = BeIdle(0f);
        }
        else
            NowCoroutine = BeIdle(1f);

        StartCoroutine(NowCoroutine);
    }

    private IEnumerator BeIdle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NowState = BossState.Idle;
        _Ani.SetTrigger("BeIdle");
        render.enabled = false;

        yield return new WaitForSeconds(1f);

        gameObject.transform.position = Radius_Position();
        gameObject.transform.LookAt(new Vector3(_Hero.position.x, 0, _Hero.position.z));
        transform.Rotate(Vector3.up, -5f, Space.World);
        render.enabled = true;
        NowCoroutine = BeAngry(5f);
        StartCoroutine(NowCoroutine);
    }

    private void IsDead()
    {
        _Ani.SetTrigger("BeDead");
        Die = true;
        Debug.Log("Boss is dead!");
    }

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(1f);
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
