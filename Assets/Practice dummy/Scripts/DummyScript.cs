using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 0;
    int hit = 0;
    float lastDamaged;
    void Start()
    {
        lastDamaged = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyHealthScript>()._health < 100f){
            lastDamaged = Time.time;
            damage += 100-GetComponent<EnemyHealthScript>()._health;
            GetComponent<EnemyHealthScript>()._health = 100;
            hit++;

        }
        if(Time.time - lastDamaged >= 3f){
            damage = 0;
            hit=0;
        }
        transform.GetComponentInChildren<TextMeshProUGUI>().text = $"Damage: {damage}\n\nHit: {hit}";
    }
}
