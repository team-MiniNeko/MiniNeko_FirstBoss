using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
using UnityEngine;

public class NearAppearWall : MonoBehaviour
{
    // Start is called before the first frame update
    public float AppearWeight;
    public float AppearDistance;
    public Transform Target;
    Color WallColor;
    void Start()
    {
        Color startColor = GetComponent<SpriteRenderer>().color;
        WallColor = new Color(startColor.r,startColor.g,startColor.b);
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Math.Abs(Target.transform.position.x - transform.position.x)-(transform.localScale.x/2f);
        if(distance <= AppearDistance)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(WallColor.r,WallColor.g,WallColor.b,(AppearDistance-distance)/AppearDistance);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0);
        }
    }
}
