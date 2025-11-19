using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageTextScript : MonoBehaviour
{   
    RectTransform RTF;
    float GravityY,Speed;
    // Start is called before the first frame update
    public void SetText(int Damage)
    {
        gameObject.GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(Damage));
    }
    void Start()
    {   
         RTF = gameObject.GetComponent<RectTransform>();
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
