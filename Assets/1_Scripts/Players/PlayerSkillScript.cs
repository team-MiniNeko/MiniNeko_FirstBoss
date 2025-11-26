using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int AttackDamage = 20;
    public String AttackType = "Enter";
    float beforeAtkTime;

    void Start()
    {
        beforeAtkTime = Time.time;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(AttackType == "Enter"){
            if(col.CompareTag("Boss")||col.CompareTag("DestoryableStructer")||col.CompareTag("Enemy")){
                EnemyHealthScript healthScript = col.gameObject.transform.GetComponent<EnemyHealthScript>();
                healthScript.EnemyDamage(AttackDamage);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(AttackType == "Stay" && Time.time-beforeAtkTime >= 0.1f){
            if(col.CompareTag("Boss")||col.CompareTag("DestoryableStructer")||col.CompareTag("Enemy")){
                beforeAtkTime = Time.time;
                EnemyHealthScript healthScript = col.gameObject.transform.GetComponent<EnemyHealthScript>();
                healthScript.EnemyDamage(AttackDamage);
            }
        }else{return;}
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
