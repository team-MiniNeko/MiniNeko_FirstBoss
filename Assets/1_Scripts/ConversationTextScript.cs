using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationScript : MonoBehaviour
{
    public Transform target;

    IEnumerator CoDisplayText(string str,float f){
        TextMeshProUGUI thistext = GetComponent<TextMeshProUGUI>();
        thistext.text = "";
        foreach(char i in str)
        {
            Debug.Log(i);
            thistext.text = $"{thistext.text}{i}";
            yield return new WaitForSeconds(f);
            if(i == ',')
                yield return new WaitForSeconds(f);
        }
        yield return new WaitForSeconds(0f);
    }
    public void DisplayText(string str,float f = 0.1f){
        Debug.Log($"display : {str}");
        StopAllCoroutines();
        IEnumerator co = CoDisplayText(str,f);
        StartCoroutine(co);
    }
    // Start is called before the first frame update
    void Start()
    {
        DisplayText("");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position,new Vector2(target.transform.position.x, target.transform.position.y+10f),10f*Time.deltaTime);
    }
}
