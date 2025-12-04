using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SmallDroneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer droneSprite;
    GameObject target;
    Animator anim;
    PlayerMove tMove;
    public void MoveTo(Transform T){
        transform.position = new Vector3(
            transform.position.x+(T.position.x-transform.position.x)*0.001f
            ,transform.position.y+(T.position.y-transform.position.y+2.5f)*0.001f
            ,transform.position.z
        );
        
    }
    IEnumerator AttackPatterns(){
        while (true)
        {
            MoveTo(target.transform);
            Vector3 direction = target.transform.position - transform.position;
            yield return new WaitForSeconds(0.001f);

            if(Math.Abs(target.transform.position.x - transform.position.x) <= 10f){
                Quaternion targetRotation;
                anim.SetBool("Shooting",true);
                float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                targetRotation = Quaternion.AngleAxis(angle+180f,Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f);
                yield return new WaitForSeconds(0.001f);
                for(int i = 0; i < 1000; i++){
                    angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                    targetRotation = Quaternion.AngleAxis(angle+180f,Vector3.forward);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.001f * 2f);
                    yield return new WaitForSeconds(0.001f);
                }
                yield return new WaitForSeconds(2f);
                droneSprite.flipY = transform.position.x < target.transform.position.x;
            }
            else
            {
                anim.SetBool("Shooting",false);
            }
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        tMove = target.GetComponent<PlayerMove>();
        IEnumerator co = AttackPatterns();
        StartCoroutine(co);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
