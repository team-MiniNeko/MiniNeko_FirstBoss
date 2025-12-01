using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainDestroy : MonoBehaviour
{
    private bool isHit = false;
    private GameObject Chain;
    //private GameObject spark;
    public FallenAngelAttack fallenAngel;

    public bool isPhaseTwo;

    private bool isFirst = true;
    private void Awake()
    {
        Chain = transform.parent.gameObject;
    }
    private void Start()
    {
        var _fallenAngel = GameObject.Find("FallenAngel").transform;
        fallenAngel = _fallenAngel.Find("FallenAngel(Sprite)").gameObject.GetComponent<FallenAngelAttack>();
        StartCoroutine(Destroy());
        Destroy(Chain, 5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHit = true;
        Collider2D player = collision;
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Hit(player));
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        if (!isHit)
        {
            Destroy(Chain);
        }
    }
    IEnumerator Hit(Collider2D collision)
    {
        if (isFirst)
        {
            Collider2D player = collision;
            Debug.Log("Fir");
            player.gameObject.GetComponent<PlayerMove>().isStop = true;
            isFirst = false;
            player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            if (fallenAngel.phase == 1)
            {
                int value = Random.Range(0, 2);
                if (value == 0)
                {
                    yield return StartCoroutine(fallenAngel.LightSwordAttack());
                }
                else
                {
                    yield return StartCoroutine(fallenAngel.LightAttack(GameObject.FindWithTag("Player").transform.position));
                }
            }
            else
            {
                int value = Random.Range(0, 4);
                if (value == 0)
                {
                    yield return StartCoroutine(fallenAngel.LightSwordAttack());
                }
                else if (value == 1)
                {
                    yield return StartCoroutine(fallenAngel.LightAttack(GameObject.FindWithTag("Player").transform.position));
                }
                else if (value == 2)
                {
                    yield return StartCoroutine(fallenAngel.DarkSwordAttack());
                }
                else if (value == 3)
                {
                    yield return StartCoroutine(fallenAngel.LightDarkLightAttack());
                }
            }
                yield return new WaitForSeconds(2f);
            Debug.Log("Sec");
            player.gameObject.GetComponent<PlayerMove>().isStop = false;
            player.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Chain.GetComponent<ChainAttack>().DisAppear();
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Thi");
            Destroy(Chain);
        }
    }
}
