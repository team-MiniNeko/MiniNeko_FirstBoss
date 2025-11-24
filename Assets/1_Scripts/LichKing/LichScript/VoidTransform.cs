using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTransform : MonoBehaviour
{
    public Transform target;
    public Material material;

    private void Awake()
    {
        material.color = new Color(1, 1, 1, 1f);
    }

    void Update()
    {
        transform.position = target.position;       
    }
}
