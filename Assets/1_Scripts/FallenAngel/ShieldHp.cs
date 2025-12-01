using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class ShieldHp : MonoBehaviour
{
    public Image shieldHp;
    public float curHp;
    public float maxHp;
    public CameraScripts cameraScript;
    public FallenAngelAttack fallenAngel;
    public int prePhase = 0;
    public float forcePower = 5000f;
    public GameObject player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        if (fallenAngel.phase != prePhase)
        {
            Debug.Log("페이즈 변화");
            curHp = maxHp;
            prePhase = fallenAngel.phase;
        }
        shieldHp.fillAmount = curHp / maxHp;
    }
    private void FixedUpdate()
    {
        if (curHp <= 0)
        {
            Debug.Log("충격파 발생");
            Vector3 dir = player.GetComponent<Rigidbody2D>().transform.position - this.transform.position;
            dir = dir.normalized;
            player.GetComponent<Rigidbody2D>().AddForce(dir * forcePower, ForceMode2D.Impulse);
            cameraScript.CameraShake(100);
            this.gameObject.SetActive(false);
        }
        else if (curHp >= maxHp)
        {
            curHp = maxHp;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
