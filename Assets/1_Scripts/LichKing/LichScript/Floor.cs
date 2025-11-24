using System.Collections;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private float t;
    public GameObject target;
    public Chain[] chain;
    private SpriteRenderer sr;
    private Collider2D cor;
    public bool isCheck;
    public bool isStay;
    public bool canShrinkChain;
    private Collider2D[] chainCollider;
    private Vector3 targetPos;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cor = GetComponent<Collider2D>();
        chainCollider = new Collider2D[chain.Length];
        for (int i = 0; i < chain.Length; i++)
        {
            chainCollider[i] = chain[i].GetComponent<Collider2D>();
        }

        sr.enabled = false;
        cor.enabled = false;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target.GetComponent<PlayerMove>().moveSpeed *= 0.5f;
            target.GetComponent<PlayerMove>().JumpPower = 0;
            target.GetComponent<PlayerMove>().DashForce = 0;
            t += Time.deltaTime;
            if (t >= 2.5f && canShrinkChain)
            {
                Debug.Log("Shrink Chain");
                canShrinkChain = false;
                StartCoroutine( ShrinkChain());
                targetPos = new Vector3(transform.position.x, -3f, targetPos.z);
                target.transform.position = targetPos;
                target.GetComponent<PlayerMove>().moveSpeed = 7f;
                target.GetComponent<PlayerMove>().JumpPower = 5;
                target.GetComponent<PlayerMove>().DashForce = 5;
            }
        }
    }
    public IEnumerator LocalScale()
    {
        if(isStay) yield break;
        
        canShrinkChain = true;
        isStay = true;
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
    }
    private void OnTriggerExit2D(Collider2D a)
    {
        if (a.CompareTag("Player") && canShrinkChain)
        {
            canShrinkChain = false;
            target.GetComponent<PlayerMove>().moveSpeed = 7f;
            target.GetComponent<PlayerMove>().JumpPower = 5;
            target.GetComponent<PlayerMove>().DashForce = 5;
            StartCoroutine( ShrinkChain());
            StartCoroutine( ReduceFloor());
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
            if (_chain != null)
                _chain.transform.localScale = new Vector3(_chain.transform.localScale.x, 0f, _chain.transform.localScale.z);
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
        float t = 0;
        while (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 0, t), 1, 1);
            t += Time.deltaTime;
            yield return null;
        }
    }
}