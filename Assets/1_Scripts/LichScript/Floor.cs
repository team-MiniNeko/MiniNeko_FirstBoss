using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private float t;
    GameObject target;
    public Chain[] chain;
    private SpriteRenderer sr;
    private Collider2D cor;
    public bool isCheck;
    public bool isStay;
    public bool canShrinkChain;
    private Collider2D[] chainCollider;
    private Vector3 targetPos;
    private PlayerMove playerMove;
    public GameObject DebuffIcon;
    private float playerSpeed;
    void Awake()
    {
        if(target==null){target=GameObject.FindWithTag("Player");}
        DebuffIcon = Instantiate(DebuffIcon);
        sr = GetComponent<SpriteRenderer>();
        cor = GetComponent<Collider2D>();
        chainCollider = new Collider2D[chain.Length];
        playerMove = target.GetComponent<PlayerMove>();
        for (int i = 0; i < chain.Length; i++)
        {
            chainCollider[i] = chain[i].GetComponent<Collider2D>();
        }

        sr.enabled = false;
        cor.enabled = false;
        playerSpeed = playerMove.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Floor Enter");
        if (collision.CompareTag("Player"))
        {
            playerMove.moveSpeed -= 3f;    
            playerMove.JumpPower = 0;
            playerMove.DashForce = 0;
            DebuffIcon.transform.parent = GameObject.FindWithTag("DebuffIcon").transform;
            
            isStay = true;
        }
}

private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            t += Time.deltaTime;
            if (t >= 2.5f && canShrinkChain)
            {
                Debug.Log("Shrink Chain");
                canShrinkChain = false;
                StartCoroutine( ShrinkChain());
                targetPos = new Vector3(transform.position.x, -3f, targetPos.z);
                target.transform.position = targetPos;
                playerMove.moveSpeed = playerSpeed;
                playerMove.JumpPower = 100f;
                playerMove.DashForce = 100f;
                DebuffIcon.transform.parent = transform;
            }
        }
    }
    public IEnumerator LocalScale()
    {
        if(isCheck) yield break;
        canShrinkChain = true;
        isCheck = true;
        float t = 0;
        if (!sr.enabled && !cor.enabled)
        {
                    transform.position = new Vector2(target.transform.position.x, transform.position.y);
                    sr.enabled = true;
                    cor.enabled = true;
                    while (transform.localScale.x < 20)
                    {
                        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 20, t), transform.localScale.y, transform.localScale.z);
                        t += Time.deltaTime;
                        yield return null;
                    }
        }
        yield return new WaitForSeconds(0.3f);
        if (!isStay)
        {
            StartCoroutine( ShrinkChain());
            StartCoroutine( ReduceFloor());
        }
    }
    private void OnTriggerExit2D(Collider2D a)
    {
        Debug.Log("Floor Exit");
        if (a.CompareTag("Player") && canShrinkChain)
        {
            canShrinkChain = false;
            StartCoroutine( ShrinkChain());
            StartCoroutine( ReduceFloor());
            DebuffIcon.transform.parent = transform;
        }
    }

    
    IEnumerator ShrinkChain()
    {
        Debug.Log("A");
        isCheck = false;
        isStay = false;
        cor.enabled = false;
        sr.enabled = false;
        t = 0;
        
        bool anyChainLeft = true;
        
        // 최소 하나라도 줄일 체인이 있을 때 반복
        while (anyChainLeft)
        {
            anyChainLeft = false;

            foreach (Chain _chain in chain)
            {
                if (_chain == null) 
                    continue;

                if (_chain.transform.localScale.y > 0.01f)
                {
                    // y값을 조금씩 줄이기
                    float newY = Mathf.MoveTowards(_chain.transform.localScale.y, 0f, 35 * Time.deltaTime);
                    _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, newY, _chain.transform.localScale.z);

                    anyChainLeft = true; // 아직 줄일 체인이 있음
                }
            }

            yield return null; // 다음 프레임까지 대기
        }

        // 모든 체인 완전히 0으로 맞춤
        foreach (Chain _chain in chain)
        {
            if (_chain != null && _chain.isTrigger)
            {
                _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, 0f, _chain.transform.localScale.z);
            }
        }
        
        for (int i = 0; i < chain.Length; i++)
        {
                chainCollider[i].enabled = true;
        }
        foreach (Chain _chain in chain)
        {
            if (_chain != null)
            {
                _chain.isTrigger = false;
            }
        }
    }

    IEnumerator ReduceFloor()
    {
        Debug.Log("Debuff remove"); 
        playerMove.moveSpeed = playerSpeed;
        playerMove.JumpPower = 100f;
        playerMove.DashForce = 100f;
        float t = 0;
        while (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0, t), 1, 1);
            t += Time.deltaTime;
            yield return null;
        }
        sr.enabled = false;
        cor.enabled = false;
        yield return new WaitForSeconds(2f);
    }
}