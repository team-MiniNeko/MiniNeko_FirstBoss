using System;
using System.Collections;
using UnityEngine;

public class Chain : MonoBehaviour
{
    public GameObject target;
    public GameObject Flooring;
    private Floor floor;
    public bool isTrigger;
    public GameObject portal;
    private Vector2 a;
    private Collider2D col;

    void Awake()
    {
        transform.localScale = Vector3.zero;
        a = GetComponent<BoxCollider2D>().offset;
        floor = Flooring.GetComponent<Floor>();
        col = GetComponent<Collider2D>();
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
                portal.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
        else
        {
                portal.transform.localScale = new Vector3(0f, 0f, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !floor.isStay)
            StartCoroutine(floor.LocalScale());
        
        if (collision.CompareTag("Player") && floor.isCheck){
            isTrigger = true;
        col.enabled = false;
        StartCoroutine(StretchRoutine());
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



    }
