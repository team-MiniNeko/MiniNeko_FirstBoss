using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class SkillCooltimeDisplayScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerAttack plrATK;
    public TextMeshProUGUI cooltxt;
    public int skilltype;
    float[] ct = {10f,15f};
    void Start()
    {
        plrATK = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        float ys = plrATK.getCooltime(skilltype);
        transform.localScale = new Vector3(1, (ys < 0)?0:ys,1);
        cooltxt.gameObject.SetActive(ys*ct[skilltype] > 0);
        cooltxt.text = (ys*ct[skilltype]).ToString("0.00");
        
    }
}
