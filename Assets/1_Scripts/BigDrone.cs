using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BigDrone : MonoBehaviour
{   
    public Transform Weapon_1;
    public Transform Weapon_2;
    public SpriteRenderer Sprite;
    public GameObject Ammo;
    public GameObject target;
    public GameObject explosionAttackRange;
    public GameObject explosionAttack;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        IEnumerator co = AttackPatterns();
        StartCoroutine(co);
    }
    public void MoveTo(Transform T)
    { //대충 타겟쪽으로 움직이는 코드
        transform.position = new Vector3(
            transform.position.x + (T.position.x - transform.position.x) * 0.001f
            , transform.position.y + (T.position.y - transform.position.y + 2.5f) * 0.001f
            , transform.position.z
        );

    }
    IEnumerator AttackPatterns()
    {
        while (true)
        {
            MoveTo(target.transform);
            Vector3 direction = target.transform.position - transform.position;
            yield return new WaitForSeconds(0.001f);

            if (Math.Abs(target.transform.position.x - transform.position.x) <= 10f)
            { // 사거리 내로 타겟이 들어오면
                Vector3 vec = target.transform.position;
                var attackRange = Instantiate(explosionAttackRange, vec, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Destroy(attackRange );
                var attack = Instantiate(explosionAttack, vec, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
                Destroy(attack );
                yield return new WaitForSeconds(2f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveTo(target.transform);
        if (target.transform.position.x < transform.position.x)
            Sprite.flipX = false;
        else
            Sprite.flipX = true;
        Vector3 direction = target.transform.position - Weapon_1.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
        Weapon_1.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f);
        Weapon_2.rotation = Weapon_1.rotation;
        if (!Sprite.flipX) // 포신 위치 조정
            Weapon_2.transform.localPosition = new Vector3(Weapon_1.transform.localPosition.x + -0.145f, Weapon_2.transform.localPosition.y, Weapon_2.transform.localPosition.z);
        else
            Weapon_2.transform.localPosition = new Vector3(Weapon_1.transform.localPosition.x + 0.145f, Weapon_2.transform.localPosition.y, Weapon_2.transform.localPosition.z);
    }
}
