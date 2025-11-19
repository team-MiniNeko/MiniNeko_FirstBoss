using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public Vector2 lastFace = Vector2.left;
    bool isDashing = false;
    bool isJumping = true;
    Vector2 Dashing = new Vector2(0,0);
    public float moveSpeed;
    public float DashForce;
    public float JumpPower;
    public float GravityScale;
    float sumdash = 0f;
    float gravity = 0f;
    RaycastHit2D raycast;
    public Collider2D colider2d;
    public GameObject atk;
    public Animator Anims;
    double atkTime;

    public bool isStop = false;
    void Start()
    {
        colider2d = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        atkTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2();
        if (!isStop)
        {
            if (Input.GetKey(KeyCode.A))
            {
                move += Vector2.left * 1f;
                lastFace = Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                move += Vector2.right * 1f;
                lastFace = Vector2.right;
            }
            if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
            {
                gameObject.GetComponent<PlayerHealth>().invisibleTime = Time.time + 0.1f;
                isDashing = true;
                Dashing = lastFace * DashForce;
            }
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                isJumping = true;
                rb.AddForce(Vector2.up * JumpPower * 100);
            }
        }
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
            
        }

        if (isDashing)
        {
            sumdash = sumdash + Dashing.x;
            transform.Translate(Dashing * Time.deltaTime * moveSpeed);
            Dashing /= 1.01f;
            if (Math.Abs(Dashing.x) <= 0.5f)
            {
                Debug.Log("Dash Charged");
                sumdash = 0f;
                Dashing = new Vector2(0, 0);
                isDashing = false;
            }
            move += Dashing;
        }
        
        transform.Translate(move * Time.deltaTime * moveSpeed);
    }
    void FixedUpdate()
    {
        RaycastHit2D Boxcast = Physics2D.BoxCast(colider2d.bounds.center, colider2d.bounds.size, 0f, Vector2.down, 0.02f, LayerMask.GetMask("Ground"));
        if (Boxcast.collider)
            isJumping = false;
        else
            isJumping = true;
    }
}
