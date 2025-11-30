using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public GameObject atk;
    public GameObject Skill1;
    public GameObject Skill2;
    Animator Anims;
    Vector2 lastFace;
    float atkTime;
    float[] cooltimes = new float [2];
    // Start is called before the first frame update
    IEnumerator ShakeEff(float forces,float times)
    {
        yield return new WaitForSeconds(times);
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>().CameraShake(100f);
    }
    void Start()
    {
        Anims = GetComponent<Animator>();
        atkTime = Time.time;
        cooltimes[0] = Time.time;
        cooltimes[1] = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // --- 공격 입력 (대쉬 중에도 공격 가능하도록 Update에 유지) ---
        lastFace = GetComponent<PlayerMove>().lastFace;
        if (Input.GetMouseButton(0) && Time.time - atkTime > 0f)
        {

            Anims.SetTrigger("NormalAttacking");
            atkTime = Time.time+0.3f;
            GameObject ins = Instantiate(atk);
            ins.transform.position = transform.position;
            ins.transform.Translate(lastFace * 2f);
            if (lastFace == Vector2.left)
                ins.transform.Rotate(0,180,0);
            Destroy(ins, 0.15f);
        }
        if (Input.GetKeyDown(KeyCode.Z) && Time.time - cooltimes[0] > 0f)
        {
            Anims.SetTrigger("Skill1");
            cooltimes[0] = Time.time+5;
            atkTime = Time.time+1.5f;
            GameObject ins = Instantiate(Skill1);
            ins.transform.position = transform.position;
            ins.transform.Translate(lastFace * 2f);
            ins.transform.parent = transform;
            
            IEnumerator co = ShakeEff(50f,0.283f);
            StartCoroutine(co);

            if (lastFace == Vector2.left)
                ins.transform.Rotate(0,180,0);
            Destroy(ins, 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.X) && Time.time - cooltimes[1] > 0f)
        {
            Anims.SetTrigger("Skill2");
            cooltimes[1] = Time.time+5;
            atkTime = Time.time+1.5f;
            GameObject ins = Instantiate(Skill2);
            ins.transform.position = transform.position;
            ins.transform.Translate(lastFace * 2f);
            ins.transform.parent = transform;

            if (lastFace == Vector2.left)
                ins.transform.Rotate(0,180,0);
            Destroy(ins, 3f);
        }
    }
}
