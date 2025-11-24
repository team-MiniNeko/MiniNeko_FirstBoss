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
    public int maxHp;
    private float _curHp;
    float curPer = 0;
    float targetPer = 1;
    public Image hpBar;
    public TextMeshProUGUI healthText;
    public GameObject diedUI;
    float nuckback = 0f;
    public float invisibleTime;
    public GameObject DamageText;
    public GameObject CameraCanvas;
    Vector2 lF;
    float mvs;
    void Start()
    {
        CameraCanvas = Instantiate(CameraCanvas);
        invisibleTime = Time.time;
        CurHp = maxHp;
        mvs = gameObject.GetComponent<PlayerMove>().moveSpeed;
    }
    public float CurHp
    {
        get { return _curHp;}
        set
        {
            float target = value;
            Debug.Log(target);
            if (target >= maxHp)
                _curHp = maxHp;
            else if (target <= 0)
            {
                _curHp = 0;
                diedUI.SetActive(true);
            }
            else
            {
                _curHp = value;
            }
            targetPer = _curHp / maxHp;
            DOTween.To(() => curPer, x => curPer = x, targetPer, 0.5f).SetEase(Ease.OutQuad);
        }   
    }
    public void Damage(int num,float addInv = 0.1f){
        if (invisibleTime < Time.time){
            CurHp -= num;
            invisibleTime = Time.time + addInv;
            GameObject dmgText = Instantiate(DamageText);
            dmgText.transform.SetParent(CameraCanvas.transform);
            dmgText.GetComponent<DamageTextScript>().SetText(num);
            dmgText.transform.position = transform.position;
            dmgText.transform.position = new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
            Destroy(dmgText,0.5f);
        }
    }
    void Update()
    {
        hpBar.fillAmount = curPer;
        healthText.text = $"{Math.Round(CurHp)}/{maxHp}";
        if (transform.position.y < -50)
        {
            transform.position = new Vector2(0, 10);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            Damage(100);
        }
    }
    void knockback()
    {
        if (nuckback > 1f)
        {
            gameObject.GetComponent<PlayerMove>().moveSpeed = 0f;
        }
        else
        {
            gameObject.GetComponent<PlayerMove>().moveSpeed = mvs;
        }

        gameObject.GetComponent<Transform>().Translate(lF * -nuckback * Time.deltaTime);
        nuckback *= 0.995f;
    }
}