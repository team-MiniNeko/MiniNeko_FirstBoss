using System.Collections; // Coroutine 사용을 위해 추가
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // --- 원래 변수 그대로 유지 ---
    Rigidbody2D rb;
    public Vector2 lastFace = Vector2.left;
    bool isDashing = false;
    bool isJumping = true;
    public float moveSpeed = 5f; // 기본값 추가
    public float DashForce = 20f; // 대쉬 초기 속도
    public float dashCoolTime = 0.2f; //대쉬 쿨타임
    public float JumpPower = 10f; // 기본값 추가 (AddForce와 호환되도록 조정)
    public float GravityScale = 3f; // 사용되지 않았으나 Rigidbody2D 기본값 복원용으로 사용 가능
    
    public Collider2D colider2d;
    public GameObject atk;
    public Animator Anims;
    public ParticleSystem DashParticle;
    double atkTime;

    public bool isStop = false;

    // --- 대쉬 지속 시간 추가 (새로운 변수) ---
    public float dashDuration = 0.15f; // 대쉬가 지속될 시간
    
    void Start()
    {
        colider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = GravityScale; // Rigidbody의 중력 스케일 적용
        atkTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(!CutSceneStatus.CheckStatus()){
            if (!isStop && !isDashing) // 일반 이동/점프/대쉬 입력은 정지 상태가 아니고 대쉬 중이 아닐 때만 받음
            {
                // --- 일반 이동 방향 결정 ---
                float horizontalInput = 0f;
                if (Input.GetKey(KeyCode.A))
                {
                    horizontalInput = -1f;
                    lastFace = Vector2.left;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    horizontalInput = 1f;
                    lastFace = Vector2.right;
                }
                // Anims 처리 (필요하다면 여기서)

                // FixedUpdate에서 Rigidbody.velocity를 설정하기 위해 입력값을 사용
                // Update에서는 키 입력만 확인하고 lastFace와 horizontalInput을 기반으로 상태만 업데이트합니다.

                // --- 대쉬 입력 ---
                if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
                {
                    StartCoroutine(DashCoroutine());
                }

                // --- 점프 입력 ---
                if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
                {
                    isJumping = true;
                    // AddForce를 사용해 원래의 점프 방식을 유지
                    rb.AddForce(Vector2.up * JumpPower * 100); 
                }
            }
            
            // --- 공격 입력 (대쉬 중에도 공격 가능하도록 Update에 유지) ---
            if (Input.GetMouseButton(0) && Time.time - atkTime > 0.5)
            {
                atkTime = Time.time;
                GameObject ins = Instantiate(atk);
                ins.transform.position = transform.position;
                ins.transform.Translate(lastFace * 2f);
                if (lastFace == Vector2.left)
                    ins.transform.Rotate(0,180,0);
                Destroy(ins, 0.1f);
            }
            
            if (Input.GetKeyDown(KeyCode.S)){
                // 원래 S 키 기능 유지
            }
        }
    }
    
    void FixedUpdate()
    {
        if(!CutSceneStatus.CheckStatus()){
            RaycastHit2D Boxcast = Physics2D.BoxCast(colider2d.bounds.center, colider2d.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
            if (Boxcast.collider)
                isJumping = false;
            else
                isJumping = true;

            // --- 일반 이동 처리 (FixedUpdate에서 물리 기반으로 처리) ---
            if (!isStop && !isDashing)
            {
                float horizontalInput = 0f;
                if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
                if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

                // Rigidbody.velocity를 직접 조작하여 물리 엔진에 맡깁니다.
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            }
        }
    }

    // --- 새로운 대쉬 코루틴 함수 ---
    IEnumerator DashCoroutine()
    {
        isDashing = true;
        
        // --- 기존 무적 시간 기능 유지 ---
        gameObject.GetComponent<PlayerHealth>().invisibleTime = Time.time + dashDuration; 
        
        // --- 기존 파티클 기능 유지 ---
        if(DashParticle != null)
            DashParticle.Play();

        // --- 대쉬 이동 처리 (Rigidbody.velocity 사용) ---
        Vector2 dashDirection = lastFace.normalized;
        rb.velocity = new Vector2((dashDirection * DashForce).x,rb.velocity.y);
        
        // 대쉬 중에는 중력을 일시적으로 절반으로 설정하여 수직 이동을 약간 보정
        float originalGravity = rb.gravityScale;
        rb.gravityScale = originalGravity/2f;
        
        // 대쉬 지속 시간만큼 대기
        yield return new WaitForSeconds(dashDuration);

        // --- 대쉬 종료 ---

        // 대쉬 속도를 멈추고 중력을 일반적인 상태로 복귀
        if(DashParticle != null)
            DashParticle.Stop();
        rb.gravityScale = originalGravity;

        // X축 속도는 0으로 설정하거나 천천히 감속하도록 조정할 수 있습니다.
        rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y);
        
        yield return new WaitForSeconds(dashCoolTime);
        isDashing = false;
        Debug.Log("Dash Charged"); // 원래 로직의 Debug.Log 유지
    }
}