using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{   
    public string CameraMode;
    public Transform Target;
    public float yValue;
    public float cameraSpeed;
    public float ylimit = -5f;
    public float Size = 14f;
    public GameObject Icon;
    public Material material;
    float ShakeForce = 0f;
    float debuffTime = 0f;
    public float originSize;
    float originYv;
    public GameObject deBuffLight;
    public GameObject deBuffDark;

    public string deBuffType;

    public GameObject player;
    void Awake()
    {
        originSize = Size;
        originYv = yValue;
        if(Icon != null)
            Icon = Instantiate(Icon);
        debuffTime = Time.time;
    }
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
        if(ShakeForce >= 0.01f)
        {
            float xdif = UnityEngine.Random.Range(-ShakeForce,ShakeForce);
            float ydif = UnityEngine.Random.Range(-ShakeForce,ShakeForce);
            transform.position = new Vector3(transform.position.x+(xdif*Time.deltaTime),transform.position.y+(ydif*Time.deltaTime),transform.position.z);
            ShakeForce -= ShakeForce*5f*Time.deltaTime;
        }
        GetComponent<Camera>().orthographicSize = GetComponent<Camera>().orthographicSize+((Size-GetComponent<Camera>().orthographicSize)*Time.deltaTime*3f);
        if(Icon != null && material != null){
            if(Time.time <= debuffTime){
                Icon.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString((int)(debuffTime-Time.time)+1);
                Size = originSize/2f;
                yValue = 0;
                material.color = new Color(0.15f, 0.15f, 0.15f, 1f);//
                Icon.transform.SetParent(GameObject.FindWithTag("DebuffIcon").transform);

            }else{
                Size = originSize;
                yValue = originYv;
                material.color = new Color(1f, 1f, 1f, 1f);//
                Icon.transform.SetParent(transform, false);//?
            }
        }
        if (deBuffDark != null && deBuffType == "Dark")
        {
            if (deBuffLight.activeSelf == true)
            {
                debuffTime = 0;
                StartCoroutine(DebuffDamage());
                deBuffDark.SetActive(false);
                deBuffLight.SetActive(false);
            }
            else if (Time.time <= debuffTime)
            {
                Icon.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString((int)(debuffTime - Time.time) + 1);
                Size = originSize / 2f;
                yValue = 0;
                deBuffDark.SetActive(true);
                Icon.transform.SetParent(GameObject.FindWithTag("DebuffIcon").transform);

            }
            else
            {
                Size = originSize;
                yValue = originYv;
                deBuffDark.SetActive(false);
                Icon.transform.parent = transform;//?
            }
        }
        if (deBuffLight != null && deBuffType == "Light")
        {
            if (deBuffDark.activeSelf == true)
            {
                debuffTime = 0;
                StartCoroutine(DebuffDamage());
                deBuffDark.SetActive(false);
                deBuffLight.SetActive(false);
            }
            else if (Time.time <= debuffTime)
            {
                Icon.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString((int)(debuffTime - Time.time) + 1);
                Size = originSize / 2f;
                yValue = 0;
                deBuffLight.SetActive(true);
                Icon.transform.SetParent(GameObject.FindWithTag("DebuffIcon").transform);

            }
            else
            {
                Size = originSize;
                yValue = originYv;
                deBuffLight.SetActive(false);
                Icon.transform.parent = transform;
            }
        }
    }
    IEnumerator DebuffDamage()
    {
        yield return new WaitForSeconds(0.1f);
        player.GetComponent<PlayerHealth>().Damage(50);
    }
    public void CameraShake(float a)
    {
        ShakeForce = a;
    }
    public void Debuff(float times)
    {   
        if(debuffTime < Time.time)
        {
            debuffTime = Time.time + times;
        }
        else
        {
            debuffTime += times;
            if(debuffTime - Time.time >= 15f)
                debuffTime = Time.time + 15f;
        }
    }
}
