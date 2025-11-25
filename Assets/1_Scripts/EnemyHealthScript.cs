using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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
    public AudioSource HitSound;
    public GameObject DamageText;
    void Start()
    {
        Health = StartHealth;
        BFH = Health;
    }
    public void EnemyDamage(int Damage)
    {
        Health -= Damage;
        if(DamageText != null){
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(GameObject.FindWithTag("FieldUI").transform);
            dmgText.GetComponent<DamageTextScript>().SetText(Convert.ToInt32(Damage));
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
            Destroy(dmgText,2.5f);
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
        if (isBoss && HealthText != null){
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
        if (Health <= 0){
            Health = 0;
            if(gameObject.CompareTag("DestoryableStructer")){
                Destroy(gameObject.GetComponent<Rigidbody2D>());
                transform.Rotate(0f,0f,-90f);
                transform.position = new Vector3(177f,-42f,2.5f);
                GetComponentInChildren<ParticleSystem>().Play();
                Destroy(GetComponent<EnemyHealthScript>());
                enabled = false;
            }else{
                gameObject.SetActive(false);
            }
        }

    }
    
}
