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
    bool isAttacking = false;
    float atkTime;
    private EnemyHealthScript ghoulHealth;
    private EnemyHealthScript Boss3Health;
    void Awake()
    {
        
        if(targetDir == null){targetDir = GameObject.FindWithTag("Player");}
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ghoulHealth = GetComponent<EnemyHealthScript>();
        Boss3Health = GameObject.FindWithTag("Boss").GetComponent<EnemyHealthScript>();
        atkTime = Time.time;
    }

    private void OnEnable()
    {
        int ranPos = Random.Range(-5, 5);
        transform.localPosition = new Vector3(ranPos, -0.4f, 0);
        GetComponent<EnemyHealthScript>().Health = GetComponent<EnemyHealthScript>().StartHealth;
    }
    private void OnDisable()
    {
        if(Boss3Health.gameObject.GetComponent<LichKingAttack>().page2)
            Boss3Health.EnemyDamage(-20);
    }

    // Update is called once per frame
    void Update()
    {   
        if(ghoulHealth.Health > 0 && Time.time-atkTime > 0.3f && Boss3Health.Health > 0 && Boss3Health.Health < 1500)
        {
            Boss3Health.EnemyDamage(-3);
            atkTime = Time.time;
        }
        else if (Boss3Health.Health <= 0)
        {
            ghoulHealth.Health = 0;
        }
        myDir = targetDir.transform.position - transform.position;
        dir = myDir.normalized;
        rb.velocity = new Vector2(-dir.x * 2f, 0);
        sr.flipX = targetDir.transform.position.x <= transform.position.x;
        
    }
}
