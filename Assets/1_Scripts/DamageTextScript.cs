using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DamageTextScript : MonoBehaviour
{   
    RectTransform RTF;
    float GravityY,Speed;
    // Start is called before the first frame update
    // commit test
    public void SetText(int Damage)
    {
        if(Damage < 0){
            GetComponent<TextMeshProUGUI>().color = new Color(0,0.8f,0);
            Damage = Math.Abs(Damage);
        }
        GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(Damage));
    }
    public void SetText(float Damage)
    {
        if(Damage < 0f){
            GetComponent<TextMeshProUGUI>().color = new Color(0,0.8f,0);
            Damage = Math.Abs(Damage);
        }
        GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(Damage));
    }
    public void SetTextColor(Color color){GetComponent<TextMeshProUGUI>().color = color;}
    void Start()
    {   
         RTF = GetComponent<RectTransform>();//Rect Transform 준말
         GravityY = UnityEngine.Random.Range(50,50);
         Speed = UnityEngine.Random.Range(-5f,5f);
    }

    // Update is called once per frame
    void Update()
    {   
        float TX = RTF.localPosition.x;
        float TY = RTF.localPosition.y;
        if(TY <= -300)
            Destroy(gameObject);
        RTF.localPosition = new Vector3(TX+(Speed*Time.deltaTime),TY+(GravityY*Time.deltaTime),0);
        GravityY-=100f*Time.deltaTime;
    }
}
