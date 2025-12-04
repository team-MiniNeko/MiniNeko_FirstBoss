using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigDrone : MonoBehaviour
{   
    public Transform Weapon_1;
    public Transform Weapon_2;
    public SpriteRenderer Sprite;
    public GameObject Ammo;
    public GameObject target;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
            if(target.transform.position.x < transform.position.x)
                Sprite.flipX = false;
            else
                Sprite.flipX = true;
            Vector3 direction = target.transform.position - Weapon_1.transform.position;
            float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle+180f,Vector3.forward);
            Weapon_1.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 180f);
            Weapon_2.rotation = Weapon_1.rotation;
    }
}
