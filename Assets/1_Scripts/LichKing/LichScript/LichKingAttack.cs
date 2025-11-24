using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class LichKingAttack : MonoBehaviour
{
    public GameObject Target;
    private Vector3 flipx;
    Rigidbody2D rb;
    public Transform sword;
    public Transform crack;

    public int speed;
    bool page2 = true;
    bool isCrecked;
    public Sprite[] Attack;
    public GameObject[] chain;
    private Chain[] ChainScript;
    private Collider2D[] chainCollider;
    SpriteRenderer sr;
    bool isDelay;
    
    Animator anim;

    private IEnumerator cor = null;

    public GameObject[] ghoulObj;
    private EnemyHealthScript[] ghoulHp;

    public GameObject[] voidBall;

    void Awake()
    {
        flipx = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        sr = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        chainCollider = new Collider2D[chain.Length];
        ChainScript = new Chain[chain.Length];
        ghoulHp = new EnemyHealthScript[ghoulObj.Length];
        for (int i = 0; i < chain.Length; i++)
        {
            chainCollider[i] = chain[i].GetComponent<Collider2D>();
            ChainScript[i] = chain[i].GetComponent<Chain>();
        }

        for (int i = 0; i < ghoulObj.Length; i++)
        {
            ghoulHp[i] = ghoulObj[i].GetComponent<EnemyHealthScript>();
        }
    }

    private void Start()
    {
            StartCoroutine(BossAttackLoop());
    }

    private void Update()
    {
        if (isCrecked)
            return;
        if (transform.transform.position.x > Target.transform.position.x)
        {
            flipx.x = Mathf.Abs(flipx.x); 
            transform.localScale = flipx;
            rb.velocity = Vector2.left * 0.7f * Math.Abs(transform.transform.position.x - Target.transform.position.x);
        }
        else
        {
            flipx.x = -Mathf.Abs(flipx.x);
            transform.localScale = flipx;
            rb.velocity = Vector2.right * 0.7f * Math.Abs(transform.transform.position.x - Target.transform.position.x);
        }
            }

    IEnumerator BossAttackLoop()
    {
        int lastPettenSycle = -1;
        int pettenSycle = Random.Range(0, 5);
        while (true)
        {
            if (cor == null)
            {

                
                while (pettenSycle == lastPettenSycle)
                {
                   pettenSycle = Random.Range(0, 5);
                }

                
                
                lastPettenSycle = pettenSycle; // 다음 회차를 위해 저장
                switch (pettenSycle)
                {
                    case 0:
                        cor = Coroutine_SwingAttack();
                        yield return cor;
                        break;
                    case 1:
                        cor = Coroutine_Creck();
                        yield return cor;
                        break;

                }

                if (page2)
                {
                    switch (pettenSycle)
                    {
                        case 2:
                            cor = Chain();
                            yield return cor;
                            break;
                        case 3:
                            cor = Ghoul();
                            yield return cor;
                            break;
                        case 4:
                            cor = VoidBall();
                            yield return cor;
                            break;
                    }
                }
            }
        }
    }

    IEnumerator SwordMoveUp()
    {
        float t = 0f;
        var swordMoveUp = new Vector3();
        while (t < 1f)
        {
            t += Time.deltaTime;
            swordMoveUp = Vector3.Lerp(sword.transform.GetChild(3).gameObject.transform.position, new Vector3(sword.transform.GetChild(3).gameObject.transform.position.x,1.3f,0) , t);
            sword.transform.GetChild(3).gameObject.transform.position = swordMoveUp;
            yield return null;
        }
    }
    IEnumerator SwordMoveDown(Action onDown = null)
    {
        var swordMoveDown = new Vector3();
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            swordMoveDown = Vector3.Lerp(sword.transform.GetChild(3).gameObject.transform.position, new Vector3(sword.transform.GetChild(3).gameObject.transform.position.x,0,0) , t);
            sword.transform.GetChild(3).gameObject.transform.position = swordMoveDown;
            if(sword.transform.GetChild(3).gameObject.transform.position.y <= 0.1f)
                onDown?.Invoke();

            yield return null;
        }
    }

    IEnumerator Coroutine_SwingAttack()
    {
        isDelay = true;
        int lastRan = -1; // 처음엔 아무 패턴도 없다고 표시
        int ran = Random.Range(0, 3);
        float ranDelay = Random.Range(0, 0.2f);
        Quaternion startRot = sword.localRotation;
        Quaternion targetRot = Quaternion.Euler(0, 0, 90f);
        if (page2)
        {
            sword.transform.GetChild(0).GetComponent<AttackEffectScript>().Damage += 30;
            sword.transform.GetChild(1).GetComponent<AttackEffectScript>().Damage += 30;
            sword.transform.GetChild(2).GetComponent<AttackEffectScript>().Damage += 30;
        }
        for (int i = 0; i < 3; i++)
        {

            // 바로 여기서 이전 값과 다를 때까지 다시 뽑기
            while (ran == lastRan)
            {
                ran = Random.Range(0, 3);
            }
            lastRan = ran; // 다음 회차를 위해 저장

            switch (ran)
            {
                case 0:
                    ranDelay = Random.Range(0, 0.2f);
                    
                    sword.transform.GetChild(0).gameObject.SetActive(true);
                    sword.transform.GetChild(1).gameObject.SetActive(false);
                    sword.transform.GetChild(2).gameObject.SetActive(false);
                    
                    if (transform.transform.position.x > Target.transform.position.x)
                    {
                        anim.SetTrigger("Attack1Left");
                    }
                    else
                    {
                        anim.SetTrigger("Attack1Right");
                    }                    
                    while (Quaternion.Angle(sword.localRotation, targetRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            targetRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                        if (isDelay && Quaternion.Angle(sword.localRotation, Quaternion.identity) > 30f)
                        { 
                            anim.speed = 0;
                            yield return new WaitForSeconds(ranDelay);
                            anim.speed = 1;
                            isDelay = false;
                        }
                    }

                    yield return new WaitForSeconds(1f);

                    while (Quaternion.Angle(sword.localRotation, startRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            startRot,
                            speed * Time.deltaTime
                        );
                        
                        yield return null;
                    }

                    if (Mathf.Abs(Target.transform.position.x - transform.position.x) <= 2)
                    {
                        yield return Coroutine_Creck();
                    }
                    break;

                case 1:
                    
                    sword.transform.GetChild(0).gameObject.SetActive(false);
                    sword.transform.GetChild(1).gameObject.SetActive(true);
                    sword.transform.GetChild(2).gameObject.SetActive(false);
                    sword.transform.GetChild(3).gameObject.SetActive(false);
                    
                    anim.SetTrigger("Attack2");
                    
                    yield return new WaitForSeconds(2);
                    if (Mathf.Abs(Target.transform.position.x - transform.position.x) <= 2)
                    {
                        yield return Coroutine_Creck();
                    }
                    break;

                case 2:
                    
                    ranDelay = Random.Range(0, 0.2f);
                    
                    isDelay = true;
                    
                    sword.transform.GetChild(0).gameObject.SetActive(false);
                    sword.transform.GetChild(1).gameObject.SetActive(false);
                    sword.transform.GetChild(2).gameObject.SetActive(true);
                    
                    if (transform.transform.position.x > Target.transform.position.x)
                    {
                        anim.SetTrigger("Attack1Left");
                    }
                    else
                    {
                        anim.SetTrigger("Attack1Right");
                    }           
                    if (transform.transform.position.x > Target.transform.position.x)
                    {
                        rb.AddForce(Vector2.left * 500);
                    }
                    else
                    {
                        rb.AddForce(Vector2.right * 500);
                    }

                    while (Quaternion.Angle(sword.localRotation, targetRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            targetRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                        if (isDelay && Quaternion.Angle(sword.localRotation, Quaternion.identity) > 30f)
                        { 
                            anim.speed = 0;
                            yield return new WaitForSeconds(ranDelay);
                            anim.speed = 1;
                            isDelay = false;
                        }
                    }

                    yield return new WaitForSeconds(1f);

                    while (Quaternion.Angle(sword.localRotation, startRot) > 0.1f)
                    {
                        sword.localRotation = Quaternion.RotateTowards(
                            sword.localRotation,
                            startRot,
                            speed * Time.deltaTime
                        );
                        yield return null;
                    }
                    if (Mathf.Abs(Target.transform.position.x - transform.position.x) <= 2)
                    {
                        yield return Coroutine_Creck();
                    }
                    break;
            }

            yield return new WaitForSeconds(1f);
        }
        cor = null;
    }

   IEnumerator Coroutine_Creck()
{
    isCrecked = true;
    rb.velocity = Vector2.zero;
    int ran = Random.Range(0, 3);
    sr.sprite = Attack[1];
    crack.gameObject.SetActive(true);

    if (Target.transform.position.x - transform.position.x < 4)
    {
        sword.transform.GetChild(0).gameObject.SetActive(false);
        sword.transform.GetChild(1).gameObject.SetActive(false);
        sword.transform.GetChild(2).gameObject.SetActive(false);
        sword.transform.GetChild(3).gameObject.SetActive(true);

        switch (ran)
{
    case 0:
        yield return SwordMoveUp();

        yield return SwordMoveDown(() => crack.transform.GetChild(0).gameObject.SetActive(true));

        yield return SwordMoveUp();
        crack.transform.GetChild(1).gameObject.SetActive(true);  // 다음 crack 켜기
        crack.transform.GetChild(0).gameObject.SetActive(false); // 이전 crack 끄기

        yield return SwordMoveDown();
        crack.transform.GetChild(1).gameObject.SetActive(true); // 내려올 때 유지

        yield return SwordMoveUp();
        crack.transform.GetChild(1).gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        break;

    case 1:
        yield return SwordMoveUp();

        yield return SwordMoveDown(() => crack.transform.GetChild(0).gameObject.SetActive(true));

        yield return SwordMoveUp();
        crack.transform.GetChild(1).gameObject.SetActive(true);
        crack.transform.GetChild(0).gameObject.SetActive(false);

        yield return SwordMoveDown();
        crack.transform.GetChild(1).gameObject.SetActive(true);

        yield return SwordMoveUp();
        crack.transform.GetChild(2).gameObject.SetActive(true);
        crack.transform.GetChild(1).gameObject.SetActive(false);

        yield return SwordMoveDown();
        crack.transform.GetChild(2).gameObject.SetActive(true);

        yield return SwordMoveUp();
        crack.transform.GetChild(2).gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        break;

    case 2:
        yield return SwordMoveUp();

        yield return SwordMoveDown(() => crack.transform.GetChild(0).gameObject.SetActive(true));

        yield return SwordMoveUp();
        crack.transform.GetChild(1).gameObject.SetActive(true);
        crack.transform.GetChild(0).gameObject.SetActive(false);

        yield return SwordMoveDown();
        crack.transform.GetChild(1).gameObject.SetActive(true);

        yield return SwordMoveUp();
        crack.transform.GetChild(2).gameObject.SetActive(true);
        crack.transform.GetChild(1).gameObject.SetActive(false);

        yield return SwordMoveDown();
        crack.transform.GetChild(2).gameObject.SetActive(true);

        yield return SwordMoveUp();
        crack.transform.GetChild(3).gameObject.SetActive(true);
        crack.transform.GetChild(2).gameObject.SetActive(false);

        yield return SwordMoveDown();
        crack.transform.GetChild(3).gameObject.SetActive(true);

        yield return SwordMoveUp();
        crack.transform.GetChild(3).gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        break;
}

    }

    sr.sprite = Attack[0];
    isCrecked = false;

    // 모든 crack 초기화
    for (int i = 0; i < 4; i++){
        crack.transform.GetChild(i).gameObject.SetActive(false);
    }
    sword.transform.GetChild(3).gameObject.SetActive(false);
    sr.sprite = Attack[0];
    isCrecked = false;
    crack.transform.GetChild(0).gameObject.SetActive(false);
    crack.transform.GetChild(1).gameObject.SetActive(false);
    crack.transform.GetChild(2).gameObject.SetActive(false);
    crack.transform.GetChild(3).gameObject.SetActive(false);   
    sword.transform.GetChild(3).gameObject.SetActive(false);
    cor = null;
}

IEnumerator Chain()
{
    
    Vector3[] dir = new Vector3[5];
    float[] Look = new float[5];
    float[] startScale = new float[5];
    float[] targetScale = new float[5];

    // 방향과 목표 길이 미리 계산
    for (int i = 0; i < 5; i++)
    {
        chainCollider[i].enabled = true;
        dir[i] = Target.transform.position - chain[i].transform.position;
        Look[i] = Mathf.Atan2(dir[i].y, dir[i].x) * Mathf.Rad2Deg;
        startScale[i] = chain[i].transform.localScale.y;
        targetScale[i] = dir[i].magnitude;
    }

    // 체인 늘리기
    float elapsed = 0f;
    while (elapsed < 1f)
    {
        elapsed += Time.deltaTime * 3f;
        float t = Mathf.Clamp01(elapsed);

        for (int i = 0; i < 5; i++)
        {
            chain[i].transform.rotation = Quaternion.Euler(0, 0, Look[i] + 85f);
            float newY = Mathf.Lerp(startScale[i], targetScale[i], t);
            chain[i].transform.localScale = new Vector3(2, newY, 1);
        }

        yield return null;
    }
    yield return new WaitForSeconds(0.2f);
    
    while (true)
    {
        bool allDone = true;

        for (int i = 0; i < 5; i++)
        {
            // 이미 Trigger 된 체인은 무시
            if (ChainScript[i].isTrigger) continue;

            float currentY = ChainScript[i].transform.localScale.y;

            // 아직 크기가 남아있으면 줄여야 함
            if (currentY > 0.01f)
            {
                allDone = false;

                float newY = Mathf.MoveTowards(currentY, 0f, 35 * Time.deltaTime);
                ChainScript[i].transform.localScale =
                    new Vector3(
                        ChainScript[i].transform.localScale.x,
                        newY,
                        ChainScript[i].transform.localScale.z
                    );

                if (newY < 0.09f)
                {
                    ChainScript[i].transform.localScale =
                        new Vector3(
                            ChainScript[i].transform.localScale.x,
                            0f,
                            ChainScript[i].transform.localScale.z
                        );
                }
            }
        }

        // 모든 체인이 줄어들었으면 종료
        if (allDone) break;

        yield return null;
    }
    yield return new WaitForSeconds(6f);

    cor = null;
}

IEnumerator Ghoul()
{
    for (int i = 0; i < ghoulObj.Length; i++)
    {
        ghoulHp[i].StartHealth = 10;
        ghoulHp[i].Health = 10;
        ghoulObj[i].SetActive(true);
    }
        yield return new WaitForSeconds(3f);
        cor = null;
}

IEnumerator VoidBall()
{
    rb.velocity = Vector2.zero;
    int i;
    for (i = 0; i < voidBall.Length; i++)
    {
        voidBall[i].SetActive(true);
        
        yield return new WaitForSeconds(0.5f);
    }

    yield return new WaitForSeconds(8f);
    rb.velocity = Vector2.one;
    cor = null;
}






public void EndPoint()
{
    sword.transform.GetChild(0).gameObject.SetActive(true);
    sword.transform.GetChild(1).gameObject.SetActive(false);
}

}
