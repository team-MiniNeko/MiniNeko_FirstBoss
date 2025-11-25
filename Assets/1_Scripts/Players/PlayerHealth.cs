using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{  
    public float maxHp;
    public float _curHp;
    public float invisibleTime;
    public GameObject DamageText;
    public GameObject CameraCanvas;
    
    float curPer = 0;
    float targetPer = 1;
    float nuckback = 0f;
    public float CurHp
    {
        get { return _curHp;}
        set
        {
            float target = value;
            if (target >= maxHp){//over heal
                _curHp = maxHp;
            }
            else if (target <= 0){//died
                _curHp = 0;
            }
            else{_curHp = value;}
            targetPer = _curHp / maxHp;
        }   
    }
    Vector2 lF;
    Vector2 StPos;
    float lastAutoHealTime;
    void Start()
    {
        maxHp = PlayerStatsManager.Instance.playerHp;
        lastAutoHealTime = Time.time;
        StPos = new Vector2(transform.position.x,transform.position.y);
        _curHp = maxHp;
        CameraCanvas = Instantiate(CameraCanvas);
        invisibleTime = Time.time;
    }
    
    public void Damage(int num,float addInv = 0.1f){
        if (invisibleTime < Time.time || addInv == 0f){
            CurHp -= num;
            if(addInv > 0.001f)
                invisibleTime = Time.time + addInv;
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(CameraCanvas.transform);
            if(num < 0)
                dmgText.GetComponent<DamageTextScript>().SetText($"+{Math.Abs(num)}");
            else;
                dmgText.GetComponent<DamageTextScript>().SetText(num);
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
            Destroy(dmgText,2.5f);
        }
    }
    
    public void Damage(int num,Color color,float addInv = 0.1f){//effect damage 용 색 바꿔서 텍스트 띄우기
        if (invisibleTime < Time.time|| addInv == 0f){
            CurHp -= num;
            if(addInv > 0.001f)
                invisibleTime = Time.time + addInv;
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(CameraCanvas.transform);
            if(num < 0)
                dmgText.GetComponent<DamageTextScript>().SetText(Math.Abs(num)) ;
            else;
                dmgText.GetComponent<DamageTextScript>().SetText(num);
            dmgText.GetComponent<DamageTextScript>().SetTextColor(color);
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
            Destroy(dmgText,2.5f);
        }
    }
    void Update()
    {
        if (transform.position.y < -50)
        {
            // transform.position = new Vector2(0, );
            GetComponent<Rigidbody2D>().velocity = StPos;
            Damage(100);
        }
        PlayerHealth php = GetComponent<PlayerHealth>();
        if(Time.time-lastAutoHealTime >= 1f && (php.maxHp-php.CurHp) > 0){
            Damage(-3,new Color(1f,0.6f,0.6f),0);
            lastAutoHealTime = Time.time;
        }
        if(Time.time < invisibleTime){
            GetComponent<SpriteRenderer>().color = new Color(0,1f,0.8f);
        }
        else{
            GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);
        }
    }
}