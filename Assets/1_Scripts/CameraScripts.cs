using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{   
    public string CameraMode;
    public Transform Target;
    public float yValue;
    public float cameraSpeed;
    public float ylimit = -5f;
    void Update()
    {
        if(Target.transform.position.y > ylimit+yValue)
            transform.position = new Vector3(transform.position.x+(Target.position.x-transform.position.x)*Time.deltaTime/1.2f*cameraSpeed,
                                        transform.position.y+((Target.position.y+yValue)-transform.position.y)*Time.deltaTime/1.2f*cameraSpeed,
                                        -10);
        else
            transform.position = new Vector3(transform.position.x+(Target.position.x-transform.position.x)*Time.deltaTime/1.2f*cameraSpeed,
                                        transform.position.y+(ylimit+yValue-transform.position.y)*Time.deltaTime/1.2f*cameraSpeed,
                                        -10);
        
    }
}
