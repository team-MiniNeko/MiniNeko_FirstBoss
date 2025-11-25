using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerHealthDisplayScript : MonoBehaviour
{

    private float _curHp;
    PlayerHealth phs;
    public Image hpBar;
    public TextMeshProUGUI healthText;
    public GameObject diedUI;
    public float CurHp;

    float curPer = 0;//
    float targetPer = 1;
    float nuckback = 0f;
    // Start is called before the first frame update
    void Start()
    {
        phs = GetComponent<PlayerHealth>();
        curPer = 1;
    }

    // Update is called once per frame
    void Update()
    {   
        CurHp = phs.CurHp;
        _curHp = phs._curHp;
        if(_curHp <= 0){
                diedUI.SetActive(true);
                phs.enabled = false;
                this.enabled = false;
        }
        curPer = curPer + ((CurHp/phs.maxHp)-curPer)*Time.deltaTime*10f;
        healthText.text = $"{Math.Round(CurHp)}/{phs.maxHp}";
        hpBar.fillAmount = curPer;
    }
}
