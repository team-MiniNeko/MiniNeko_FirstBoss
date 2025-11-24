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
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        int ranPos = Random.Range(-5, 5);
        transform.localPosition = new Vector3(ranPos, -0.4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        myDir = targetDir.transform.position - transform.position;
        dir = myDir.normalized;
        rb.velocity = new Vector2(dir.x * 7f, 0);
        sr.flipX = targetDir.transform.position.x <= transform.position.x;

    }
}
