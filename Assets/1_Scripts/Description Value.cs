using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionValue : MonoBehaviour
{

    string[] desc = {"공격력 2x 체력 2x\n초보자와 조작이 익숙하지 않은 분들을 위해\n이 난이도가 생겼습니다.",
    "공격력 1.5x 체력 1.5x\n평범한 난이도, 평범한 경험.",
    "공격력 1x 체력 1x\n진정한 고수를 위한 길\nTMI)이 난이도는 표준 난이도였습니다.",
    "공격력 0.85x 체력 0.85x\n+자동 회복 없음\n이걸 진짜 깬다고..?" };
    string[] name = { "병아리", "사람" ,"베테랑","초월자"};
    public Slider sl;
    public TextMeshProUGUI descui;
    public TextMeshProUGUI nameui;
    public Image bg;
    int indexs;
    // Update is called once per frame
    IEnumerator cc()
    {
        while (true)
        {
            Color tgc = new Color(0,0,0,0);
            if(indexs == 2){
                tgc = new Color(0.75f, 0f, 0f, UnityEngine.Random.Range(0.4f, 0.8f));}
            if(indexs == 3){
                tgc = new Color(0.05f, 0f, 0.1f, UnityEngine.Random.Range(0.9f, 1f));}
            bg.color = tgc;
            yield return new WaitForSeconds(0.25f);
        }
    }
    bool shaked = false;
    void Start()
    {
        IEnumerator co = cc();
        StartCoroutine(co);
    }
    void Update()
    {
        indexs = Convert.ToInt32(sl.value - 1);
        descui.text = desc[indexs];
        nameui.text = name[indexs];
        bg.enabled = indexs >= 2;
        if(indexs >= 2 && shaked == false)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>().CameraShake(50f);
            shaked = true;
        }else if(indexs < 2) { shaked = false; }
        
    }
}
