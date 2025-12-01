using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using UnityEngine;
using TMPro;

public class NearAppearFont : MonoBehaviour
{
    // Start is called before the first frame update
    public float AppearWeight;
    public float AppearDistance = 10f;
    public Transform Target;
    Color WallColor;
    void Start()
    {
        Color startColor = GetComponent<TextMeshProUGUI>().color = new Color(1,1,1);
        WallColor = new Color(startColor.r,startColor.g,startColor.b);
        if(Target == null)
            Target = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Math.Abs(Target.transform.position.x - transform.position.x)-(transform.localScale.x/2f);
        if(distance <= AppearDistance)
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(WallColor.r,WallColor.g,WallColor.b,(AppearDistance-distance)/AppearDistance);
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0,0,0,0);
        }
    }
}
