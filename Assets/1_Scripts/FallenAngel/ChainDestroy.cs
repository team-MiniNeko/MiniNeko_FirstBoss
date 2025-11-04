using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDestroy : MonoBehaviour
{
    private bool isHit = false;
    private GameObject Chain;
    private void Awake()
    {
        Chain = transform.parent.gameObject;
    }
    private void Start()
    {
        Invoke("Destroy", 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if (collision.CompareTag("Player"))
        {
            isHit = true;
            Debug.Log("Hit");
        }
    }
    void Destroy()
    {
        if (isHit)
        {
            Destroy(Chain, 1.5f);
        }
        else
        {
            Destroy(Chain);
        }
    }
}
