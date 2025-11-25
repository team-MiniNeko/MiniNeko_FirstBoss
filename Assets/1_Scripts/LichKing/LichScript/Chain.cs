using System;
using System.Collections;
using UnityEngine;

public class Chain : MonoBehaviour
{
    GameObject target;
    public GameObject Flooring;
    private Floor floor;
    public bool isTrigger;
    public GameObject portal;
    private Vector2 a;
    private Collider2D col;
    private float t;
    private PlayerMove playerMove;

    void Awake()
    {
        target = GameObject.FindWithTag("Player");
        transform.localScale = Vector3.zero;
        a = GetComponent<BoxCollider2D>().offset;
        floor = Flooring.GetComponent<Floor>();
        col = GetComponent<Collider2D>();
        portal.transform.localScale = Vector3.zero;
        playerMove = target.GetComponent<PlayerMove>();
    }
    private void Update()
    {
        if (a.x != 0f || a.y != 0f)
        {
            a.x = 0;
            a.y = 0;
        }
        if (transform.localScale.y > 0)
        {
                portal.transform.localScale = new Vector3(Mathf.Lerp(portal.transform.localScale.x, 0.1f, t), Mathf.Lerp(portal.transform.localScale.y, 0.1f, t), 1);
                t += Time.deltaTime;
                if (portal.transform.localScale.x >= 0.1f)
                {
                    t = 0;
                }
        }
        else
        {
                portal.transform.localScale = new Vector3(Mathf.Lerp(portal.transform.localScale.x, 0, t), Mathf.Lerp(portal.transform.localScale.y, 0, t), 1);
                t += Time.deltaTime;
                if (portal.transform.localScale.x == 0)
                {
                    t = 0;
                }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            if (!floor.isStay)
            {
                StartCoroutine(floor.LocalScale());
                Debuff();
            }

            if (floor.isCheck){
                isTrigger = true;
                col.enabled = false;
                StartCoroutine(StretchRoutine());
            }
        }
    }
    IEnumerator StretchRoutine()
    {
        // 체인 늘리기
        yield return new WaitForSeconds(0.2f);
        while (floor.isStay)
        {
            Vector3 dir = target.transform.position - transform.position;
            float look = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, look + 85);

            float targetLength = 10f; // 원하는 길이
            transform.localScale = new Vector3(2, Mathf.MoveTowards(transform.localScale.y, targetLength, Time.deltaTime * 10f), 1);

            yield return null; // 매 프레임 갱신
        }
    }
    void Debuff()
    {
        Debug.Log("Test");
        playerMove.moveSpeed -= 1f;
    }
}
