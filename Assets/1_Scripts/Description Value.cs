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

    string[] desc = {"공격력 2x 체력 2x\n 초보자들 및 게임 조작이 익숙하지 않은 분들을\n위해 만들어졌습니다.", "공격력 1.5x 체력 1.5x\n일반적인 플레이어를 위한 난이도입니다.", "공격력 1x 체력 1x\n정말 도전적입니다. 클리어가 가능할까요?" };
    string[] name = { "병아리","사람" ,"\"신\"" };
    public Slider sl;
    public TextMeshProUGUI descui;
    public TextMeshProUGUI nameui;
    public Image bg;
    // Update is called once per frame
    IEnumerator cc()
    {
        while (true)
        {
            Color tgc = new Color(0.75f, 0f, 0f, UnityEngine.Random.Range(0.4f, 0.8f));
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
        int indexs = Convert.ToInt32(sl.value - 1);
        descui.text = desc[indexs];
        nameui.text = name[indexs];
        bg.enabled = (indexs == 2);
        if(indexs == 2 && shaked == false)
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>().CameraShake(50f);
            shaked = true;
        }else if(indexs != 2) { shaked = false; }
        
    }
}
