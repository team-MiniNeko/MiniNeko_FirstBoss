using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDestroy : MonoBehaviour
{
    private bool isHit = false;
    private GameObject Chain;

    private bool isFirst = true;
    private void Awake()
    {
        Chain = transform.parent.gameObject;
    }
    private void Start()
    {
        StartCoroutine(Destroy());
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
