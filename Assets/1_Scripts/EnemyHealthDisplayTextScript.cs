using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplayTextScript : MonoBehaviour
{
    public EnemyHealthScript enemy;
    float Health;
    float StartHealth;

    // Update is called once per frame
    void Start()
    {
        StartHealth = enemy.StartHealth;
    }
    void Update()
    {

        Health = enemy.Health;
        if(Health <= 0)
            Destroy(gameObject);
        GetComponent<TextMeshProUGUI>().text = $"{Health}/{StartHealth}";
    }
}
