using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DummyScript : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 0;
    float lastDamaged;
    void Start()
    {
        lastDamaged = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<EnemyHealthScript>().Health < 100f){
            GetComponent<EnemyHealthScript>().Health += 1;
            lastDamaged = Time.time;
            damage += 1;
        }
        if(Time.time - lastDamaged >= 3f)
            damage = 0;
        transform.GetComponentInChildren<TextMeshProUGUI>().text = $"Damage: {damage}";
    }
}
