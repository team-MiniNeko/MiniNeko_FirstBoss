using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{   
    public GameObject atk;
    public GameObject Skill1;
    public GameObject Skill2;
    public AudioSource NormalAttackSound;
    public AudioSource Skill2Sound;
    public GameObject Cooltime1;
    public GameObject Cooltime2;
    public bool isStop = false;
    Animator Anims;
    Vector2 lastFace;
    float atkTime;
    float[] cooltimes = new float [2];

    float[] cooltimestatic = {10f,15f};
    // Start is called before the first frame update
    IEnumerator ShakeEff(float forces,float times)
    {
        yield return new WaitForSeconds(times);
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>().CameraShake(100f);
    }
    IEnumerator skill2SoundPlay()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < 15; i++){
            Skill2Sound.pitch = Random.Range(1f,2f);
            Skill2Sound.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Start()
    {
        Anims = GetComponent<Animator>();
        atkTime = Time.time;
        cooltimes[0] = Time.time;
        cooltimes[1] = Time.time;
    }
    public float getCooltime(int i)
    {
        if(Time.time - atkTime > 0f && Time.time - cooltimes[i] > 0f)
            return 0;
        else
            return (System.Math.Max(cooltimes[i] , atkTime) - Time.time)/cooltimestatic[i];
    }
    // Update is called once per frame
    void Update()
    {   
        if(!isStop){
            // --- 공격 입력 (대쉬 중에도 공격 가능하도록 Update에 유지) ---
            lastFace = GetComponent<PlayerMove>().lastFace;
            
            if (Input.GetKeyDown(KeyCode.Z) && Time.time - cooltimes[0] > 0f&& Time.time - atkTime > 0f)
            {
                Anims.SetTrigger("Skill1");
                cooltimes[0] = Time.time+10;
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
            if (Input.GetKeyDown(KeyCode.X) && Time.time - cooltimes[1] > 0f&& Time.time - atkTime > 0f)
            {
                Anims.SetTrigger("Skill2");
                cooltimes[1] = Time.time+15;
                atkTime = Time.time+3f;
                GameObject ins = Instantiate(Skill2);
                ins.transform.position = transform.position;
                ins.transform.Translate(lastFace * 2f);
                ins.transform.parent = transform;
                IEnumerator co = skill2SoundPlay();
                StartCoroutine(co);
                if (lastFace == Vector2.left)
                    ins.transform.Rotate(0,180,0);
                Destroy(ins, 3f);
            }
            if (Input.GetMouseButtonDown(0) && Time.time - atkTime > 0f)
            {

                Anims.SetTrigger("NormalAttacking");
                NormalAttackSound.pitch = Random.Range(2f,3f);
                NormalAttackSound.Play();
                atkTime = Time.time+0.3f;
                GameObject ins = Instantiate(atk);
                ins.transform.position = transform.position;
                ins.transform.Translate(lastFace * 2f);
                if (lastFace == Vector2.left)
                    ins.transform.Rotate(0,180,0);
                Destroy(ins, 0.15f);
            }
        }
    }
}
