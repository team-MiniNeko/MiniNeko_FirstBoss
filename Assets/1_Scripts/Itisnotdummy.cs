using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Itisnotdummy : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 0;
    int hit = 0;
    float lastDamaged;
    float phase = 0;
    GameObject plr;
    PlayerMove plrMove;
    PlayerAttack plrAtk;
    PlayerHealth plrHp;
    public ConversationScript talksc;
    void Start()
    {
        plr = GameObject.FindWithTag("Player");
        plrMove = plr.GetComponent<PlayerMove>();
        plrAtk = plr.GetComponent<PlayerAttack>();
        plrHp = plr.GetComponent<PlayerHealth>();
        lastDamaged = Time.time;
    }
    IEnumerator Phase1CutScene()
    {
        plr.transform.position = new Vector3(plr.transform.position.x,plr.transform.position.y+5f,plr.transform.position.z);
        plrMove.isStop = true;
        plrAtk.isStop = true;
        GameObject.Find("DamageTextUI").SetActive(false);
        talksc.DisplayText("너 뭐야",0.1f);
        yield return new WaitForSeconds(0.9f);
        talksc.DisplayText("어케왔누",0.1f);
        yield return new WaitForSeconds(0.9f);
        talksc.DisplayText("근데 오자마자 보이는거 패는게 맞냐",0.1f);
        yield return new WaitForSeconds(1.9f);
        talksc.DisplayText("걍 죽으셈 ㅅㄱ",0.1f);
        yield return new WaitForSeconds(1.3f);
        for(int i = 0; i < 11; i++){
            plrHp.Damage(100);
            yield return new WaitForSeconds(0.3f);}
    }
    // Update is called once per frame
    void Update()
    {
        
        if(phase == 0){
            if(GetComponent<EnemyHealthScript>()._health < 100f){
                lastDamaged = Time.time;
                damage += 100-GetComponent<EnemyHealthScript>()._health;
                GetComponent<EnemyHealthScript>()._health = 100;
                hit++;

            }
            if(Time.time - lastDamaged >= 3f){
                damage = 0;
                hit=0;
            }
            transform.GetComponentInChildren<TextMeshProUGUI>().text = $"Damage: {damage}\n\nHit: {hit}";
            if(damage >= 500)
            {
                phase = 0.5f;
                IEnumerator co = Phase1CutScene();
                StartCoroutine(co);
            }
        }
        GetComponent<EnemyHealthScript>()._health = 100;
    }
}
