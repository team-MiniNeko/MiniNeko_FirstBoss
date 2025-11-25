using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    public string Type;
    public int MultipleSize = 1;
    public float invisibleTime = 0.03f;
    public int EffectType = 0;//0 물리 1 독 부여 ㄱㄴ
    public float EffectRate = 0.1f;//0f~1f;

    bool EffectAttack(){
        if(Random.Range(0f,1f) < EffectRate){
            GameObject.Find("DebuffManageUI").GetComponent<DebuffManage>().DebuffAdd("Poison");
            return true;
        }
        return false;
    }
    void DoDamage()
    {   
        for(int i = 0; i < MultipleSize-1; i++){
            if(EffectType == 1 && EffectAttack()){
                GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Damage(Damage,new Color(0f,0.5f,0f),0.001f);
                continue;
            }
            if(Damage != 0)
                GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Damage(Damage,0.001f);
        }
        if(EffectType == 1 && EffectAttack()){
            GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Damage(Damage,new Color(0f,0.5f,0f),invisibleTime);
            return;
        }
        if(Damage != 0)
            GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Damage(Damage,invisibleTime);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(Type == "Stay"){
            if (collision.CompareTag("Player"))
            {
                DoDamage();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        if(Type == "Enter" || Type == ""){
            if (collision.CompareTag("Player"))
            {
                DoDamage();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
