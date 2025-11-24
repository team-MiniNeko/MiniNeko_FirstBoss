using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(col.enabled)
        col.enabled = false;
    }

    public void OnEnable()
    {
        if(!col.enabled)
        col.enabled = true;
    }
}
