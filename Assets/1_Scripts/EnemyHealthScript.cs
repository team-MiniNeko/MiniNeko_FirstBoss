using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int StartHealth;
    public int Health;
    int BFH;
    double lastchanged;
    public bool isBoss;
    public RectTransform HPBar;
    public RectTransform HPBarEF;
    public TextMeshProUGUI HealthText;
    void Start()
    {
        Health = StartHealth;
        BFH = Health;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("hitt");
        if (col.CompareTag("PlayerAttack")){
            Health -= 20;
            Debug.Log("HHiitt");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -50)
        {
            Health -= 200;
            transform.position = new Vector3(0, 10, transform.position.z);
        }
        if (isBoss){
            if (BFH != Health){
                lastchanged = Time.time;
                BFH = Health;
                HealthText.text = $"{Health}/{StartHealth}";
            }
            HPBar.localScale = new Vector3(HPBar.localScale.x + (((float)Health / (float)StartHealth) - HPBar.localScale.x) * Time.deltaTime * 100, 1f, 1f);
            if (Time.time - lastchanged > 1){
                if (HPBarEF.localScale.x - HPBar.localScale.x < 0.001f)
                {
                    HPBarEF.localScale = new Vector3(HPBar.localScale.x, 1f, 1f);

                }
                else
                    HPBarEF.localScale = new Vector3(HPBarEF.localScale.x + (HPBar.localScale.x - HPBarEF.localScale.x) * Time.deltaTime * 4, 1f, 1f);
            }
            else{HPBarEF.localScale = new Vector3(HPBarEF.localScale.x + (HPBar.localScale.x - HPBarEF.localScale.x) * Time.deltaTime / 10, 1f, 1f);}
        }
        if (Health <= 0){Health = 0; Destroy(gameObject); }

    }
    
}
