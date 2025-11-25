using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DebuffManage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject IconSample;
    public Sprite PosionIconSprite;
    int[] debuffStackList = {0};
    int[] debuffTickList = {15};
    IEnumerator TimeChange(TextMeshProUGUI TimeText, float timer){
        float thistime = Time.time;
        TimeText.text = $"{Math.Round(timer-(Time.time - thistime),1)}"; 
        while(Time.time - thistime <= timer){  
            yield return new WaitForSeconds(0.1f);
            TimeText.text = $"{Math.Round(timer-(Time.time - thistime),1)}"; 
        }
    }
    IEnumerator DoDebuff(int debuffType,GameObject Icon){
        TextMeshProUGUI time = Icon.transform.Find("Time").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI stack = Icon.transform.Find("Stack").GetComponent<TextMeshProUGUI>();
        yield return new WaitForSeconds(0.01f);
        while(debuffStackList[debuffType] > 0){
            IEnumerator tc = TimeChange(time,3f);
            StartCoroutine(tc);
            for(int i = 1; i <= debuffTickList[debuffType]; i++){ 
                stack.text = $"x{debuffStackList[debuffType]}";
                GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Damage(debuffStackList[debuffType],new Color(0,0.7f,0),0);
                yield return new WaitForSeconds(0.2f);
            }
            debuffStackList[debuffType]--;

        }
        Destroy(Icon);
    }
    public void DebuffAdd(String DebuffName){ // << 이거 다시쳐만들다가 2시간이 업어짐 아픈 코드 ㅇㅇ..
        if(DebuffName == "Poison"){
            if(debuffStackList[0] == 0){
                GameObject PoisonIcon = Instantiate(IconSample);
                PoisonIcon.GetComponent<Image>().sprite = PosionIconSprite;
                PoisonIcon.transform.parent = GameObject.Find("DebuffManageUI").transform;
                IEnumerator co = DoDebuff(0,PoisonIcon);
                StartCoroutine(co);
            }
            debuffStackList[0]++;
        }
        else{
            Debug.Log($"{DebuffName}이라는 디버프가 없어서 적용에 실패했습니다.");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Transform[] icons = GetComponentsInChildren<Transform>();
        Debug.Log($"SCALE:{GetComponent<RectTransform>().sizeDelta.x}");
        float x = 0f;
        foreach(Transform child in transform){
            Debug.Log(child.name);
            child.GetComponent<RectTransform>().localPosition = new Vector3(x-(GetComponent<RectTransform>().sizeDelta.x/2f),0,0);
            x+=100f;
        }
    }
}
