using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationSet : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = quaternion.Euler(0f,0f,0f);
    }
}
