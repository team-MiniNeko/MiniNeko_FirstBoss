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
    public int _health;
    public int preHealth;
    int BFH;
    double lastchanged;
    public bool isBoss;
    public RectTransform HPBar;
    public RectTransform HPBarEF;
    public TextMeshProUGUI HealthText;
    public AudioSource HitSound;
    public GameObject HitEffect;
    public GameObject DamageText;
    float invinsibleTime;

    public GameObject portal;

    public ShieldHp shield;
    IEnumerator hitColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(1,0,0);
        yield return new WaitForSeconds(0.01f);
        GetComponent<SpriteRenderer>().color = new Color(1,1,1);
    }
    void Start()
    {
        _health = StartHealth;
        preHealth = StartHealth;
        BFH = Health;
        invinsibleTime = Time.time;
    }
    public void EnemyHeal(float HealAmount)
    {
        _health += Convert.ToInt32(HealAmount);
        if (DamageText != null){
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(GameObject.FindWithTag("FieldUI").transform);
            dmgText.GetComponent<DamageTextScript>().SetText(Convert.ToInt32(-HealAmount));
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y+3f,transform.position.z);
            Destroy(dmgText,2.5f);
        }
        if(HitEffect != null)
        {
            GameObject hiteffect = Instantiate(HitEffect);
            Vector2 playerLoc = GameObject.FindWithTag("Player").transform.position;
            hiteffect.transform.position = transform.position;
            hiteffect.transform.rotation = quaternion.Euler(0f,0f,UnityEngine.Random.Range(0f,360f));
            Destroy(hiteffect,0.3f);
        }
        if(HitSound != null)
        {
            HitSound.Play();
        }
    }
    public void EnemyDamage(float Damage)
    {
        IEnumerator co = hitColor();
        StartCoroutine(co);
        _health -= Convert.ToInt32(Damage*Convert.ToInt32(PlayerStatsManager.Instance.PlayerAttack));
        int damage = Convert.ToInt32(Damage * Convert.ToInt32(PlayerStatsManager.Instance.PlayerAttack));
        if (DamageText != null){
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(GameObject.FindWithTag("FieldUI").transform);
            dmgText.GetComponent<DamageTextScript>().SetText(Convert.ToInt32(damage));
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y+3f,transform.position.z);
            Destroy(dmgText,2.5f);
        }
        if(HitEffect != null)
        {
            GameObject hiteffect = Instantiate(HitEffect);
            Vector2 playerLoc = GameObject.FindWithTag("Player").transform.position;
            hiteffect.transform.position = transform.position;
            hiteffect.transform.rotation = quaternion.Euler(0f,0f,UnityEngine.Random.Range(0f,360f));
            Destroy(hiteffect,0.3f);
        }
        if(HitSound != null)
        {
            HitSound.Play();
        }
    }
    public void EnemyDamage(float Damage, float iT)
    {
        IEnumerator co = hitColor();
        StartCoroutine(co);
        if(Time.time - invinsibleTime < 0f)
            return;
        invinsibleTime = Time.time + iT;
        _health -= (int)(Damage * Convert.ToInt32(PlayerStatsManager.Instance.PlayerAttack));
        int damage = (int)(Damage * Convert.ToInt32(PlayerStatsManager.Instance.PlayerAttack));
        if (DamageText != null){
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(GameObject.FindWithTag("FieldUI").transform);
            dmgText.GetComponent<DamageTextScript>().SetText(Convert.ToInt32(damage));
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y+3f,transform.position.z);
            Destroy(dmgText,2.5f);
        }
        if(HitEffect != null)
        {
            GameObject hiteffect = Instantiate(HitEffect);
            Vector2 playerLoc = GameObject.FindWithTag("Player").transform.position;
            hiteffect.transform.position = transform.position;
            hiteffect.transform.rotation = quaternion.Euler(0f,0f,UnityEngine.Random.Range(0f,360f));
            Destroy(hiteffect,0.3f);
        }
        if(HitSound != null)
        {
            HitSound.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shield != null)
        {
            if (_health != preHealth && shield.curHp > 0)
            {
                shield.curHp -= (preHealth - _health);
                _health = StartHealth;
            }
            else
            {
                Health = _health;
            }
        }
        else
        {
            Health = _health;
        }
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
                if(portal != null)
                    portal.SetActive(true);
                gameObject.SetActive(false);
            }
        }

    }
    
}
