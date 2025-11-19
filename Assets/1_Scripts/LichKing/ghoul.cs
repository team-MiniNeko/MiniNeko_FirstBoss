using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ghoul : MonoBehaviour
{
    private Vector3 dir;
    private Rigidbody2D rb;
    public GameObject targetDir;
    private Vector3 myDir;
    private SpriteRenderer sr;
    Animator anim;
    bool isAttacking = false;
    public GameObject[] otherG;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        float ranPos = Random.Range(-1f, 1f);
        transform.position = new Vector3(ranPos, -2f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        myDir = targetDir.transform.position - transform.position;
        dir = myDir.normalized;
        rb.velocity = new Vector2(dir.x * 7f, 0);
        if (targetDir.transform.position.x > transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }
}
