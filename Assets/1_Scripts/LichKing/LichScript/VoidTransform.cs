using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTransform : MonoBehaviour
{
    private GameObject target;
    public Material material;
    private void Awake()
    {
            target = GameObject.FindWithTag("Boss");
        material.color = new Color(1, 1, 1, 1f);
    }
    private void OnEnable()
    {
        if (target == null)
        {
            target = GameObject.FindWithTag("Boss");
        }
        transform.position = target.transform.position;
    }

   
}
