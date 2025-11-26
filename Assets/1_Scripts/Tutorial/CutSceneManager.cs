using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static Animator TutoUiAnim;
    public GameObject Stone;
    public CameraScripts CamSc;
    int uilv = 1;
    public void SetUiLevel(int a)
    {
        transform.Find($"TutorialPanel({uilv})").gameObject.SetActive(false);
        uilv = a;
        transform.Find($"TutorialPanel({a})").gameObject.SetActive(true);
        GameObject.FindWithTag("Player").GetComponent<PlayerMove>().JumpPower = (a >= 2 ? 100f : 0f);
        if(a == 2)
            DropStone();
        
    }
    public void DropStone()
    {
        Stone.SetActive(true);
        CamSc.CameraShake(100f);
    }
    void Start()
    {
        TutoUiAnim = gameObject.GetComponent<Animator>();
        SetUiLevel(1);
    }
}
