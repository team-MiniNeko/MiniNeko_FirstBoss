using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class wall : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerMove playerMove;
    public GameObject player;
    public GameObject boss;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMove.isStop = true;
            playerMove.DashForce = 0;
            if(player.transform.position.x > transform.position.x)
            player.transform.position = new Vector3(player.transform.position.x + 1,player.transform.position.y,player.transform.position.z);
            else
            player.transform.position = new Vector3(player.transform.position.x - 1,player.transform.position.y,player.transform.position.z);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            rb.velocity = Vector2.zero;
            if(boss.transform.position.x > transform.position.x)
                boss.transform.position = new Vector3(boss.transform.position.x + 1,boss.transform.position.y,boss.transform.position.z);
            else
                boss.transform.position = new Vector3(boss.transform.position.x - 1,boss.transform.position.y,boss.transform.position.z);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMove.isStop = false;
            playerMove.DashForce = 5;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            rb.velocity = Vector2.one;
        }
    }
}
