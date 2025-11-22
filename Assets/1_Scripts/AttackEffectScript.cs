using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    public string Type;
    public float invisibleTime = 0.01f;
    void OnTriggerStay2D(Collider2D collision)
    {
        if(Type == "Stay"){
            Debug.Log(collision.name);
            if (collision.CompareTag("Player"))
            {
                Debug.Log("attack HIT");
                collision.gameObject.GetComponent<PlayerHealth>().Damage(Damage,invisibleTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        if(Type == "Enter" || Type == ""){
            Debug.Log(collision.name);
            if (collision.CompareTag("Player"))
            {
                Debug.Log("attack HIT");
                collision.gameObject.GetComponent<PlayerHealth>().Damage(Damage,invisibleTime);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
