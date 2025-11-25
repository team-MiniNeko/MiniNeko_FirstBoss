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
    float atkTime;
    void Awake()
    {
        if(targetDir == null){targetDir = GameObject.FindWithTag("Player");}
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        atkTime = Time.time;
    }

    private void OnEnable()
    {
        int ranPos = Random.Range(-5, 5);
        transform.localPosition = new Vector3(ranPos, -0.4f, 0);
        GetComponent<EnemyHealthScript>().Health = GetComponent<EnemyHealthScript>().StartHealth;
    }

    // Update is called once per frame
    void Update()
    {   
        if(GetComponent<EnemyHealthScript>().Health > 0 && Time.time-atkTime > 0.3f)
        {
            GameObject.FindWithTag("Boss").GetComponent<EnemyHealthScript>().EnemyDamage(-1);
            atkTime = Time.time;
        }
        myDir = targetDir.transform.position - transform.position;
        dir = myDir.normalized;
        rb.velocity = new Vector2(-dir.x * 2f, 0);
        sr.flipX = targetDir.transform.position.x <= transform.position.x;
        
    }
}
