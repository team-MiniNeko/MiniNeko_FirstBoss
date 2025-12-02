using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghoulAttacking : MonoBehaviour
{
    Animator anim;
    public GameObject targetDir;
    private SpriteRenderer sr;
    bool isAttacking;

    void Awake()
    {
        if(targetDir == null){targetDir = GameObject.FindWithTag("Player");}
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            if (Mathf.Abs(targetDir.transform.position.x - transform.position.x) < 5 && !isAttacking)
            {
                if (targetDir.transform.position.x < transform.parent.position.x)
                {
                    transform.localPosition = new Vector3(-Mathf.Abs(transform.localPosition.x), transform.localPosition.y, 0);
                    transform.localRotation = Quaternion.Euler(0, 0, -30);
                    sr.flipX = true;
                }
                else
                {
                    transform.localPosition = new Vector3(Mathf.Abs(transform.localPosition.x), transform.localPosition.y, 0);
                    transform.localRotation = Quaternion.Euler(0, 0, 30);
                    sr.flipX = false;
                }

                isAttacking = true;
                Debug.Log("Attack");
                anim.SetBool("Attack", true);
                yield return new WaitForSeconds(0.5f);
                anim.SetBool("Attack", false);
                yield return new WaitForSeconds(2);
                isAttacking = false;
            }
            else
            {
                yield return null;
            }
        }
    }
}
