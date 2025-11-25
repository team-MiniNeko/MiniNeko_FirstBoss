using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeKingAttack : MonoBehaviour
{
    public GameObject Target;
    public GameObject Wave;
    public GameObject Missile;
    float atkbftime = -5.0f;
    float mscooltime = 0.0f;
    Vector2 atksee;
    object enemyhealthsc;
    IEnumerator coroutine;
    IEnumerator SummonMissile(int missile_num)
    {
        Debug.Log("missile Launched");
        for(int i = 0; i < missile_num; i++){
            yield return new WaitForSeconds(0.1f);
            GameObject CopyMissile = Instantiate(Missile);
            CopyMissile.transform.position = transform.position;
            CopyMissile.GetComponent<MissileScript>().Target = Target;
        }
    }
    IEnumerator SummonWave()
    {
        yield return new WaitForSeconds(1f);
        GameObject CopyWave = Instantiate(Wave);
        CopyWave.transform.position = new Vector3(transform.position.x,transform.position.y-1,CopyWave.transform.position.z);
        Destroy(CopyWave,0.8f);
    }
    void Start()
    {
        mscooltime = Time.time;
        Target = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        //1 phase pattern
        if (Time.time - atkbftime > 2f)
        {
            if(Math.Abs(transform.position.x-Target.transform.position.x) >= 10){
                IEnumerator co;
                co = SummonWave();
                StartCoroutine(co);
                atkbftime = Time.time;
                if (transform.position.x > Target.transform.position.x)
                    atksee = Vector2.left * 3000f + Vector2.up * 20000f;
                if (transform.position.x < Target.transform.position.x)
                    atksee = Vector2.right * 3000f + Vector2.up * 20000f;
                gameObject.GetComponent<Rigidbody2D>().AddForce(atksee);
            }
            else
            {
            }
        }
        //2 phase
        if (gameObject.GetComponent<EnemyHealthScript>().Health < gameObject.GetComponent<EnemyHealthScript>().StartHealth / 2)
        {
            Debug.Log("HPHALF");
            Debug.Log("COLORCHANGE");
            if (Time.time - mscooltime > 5f)
            {
                mscooltime = Time.time;
                coroutine = SummonMissile(4);
                StartCoroutine(coroutine);
            }
        }
    }
}
