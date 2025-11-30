using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoidBall : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    private CameraScripts camera;
    void Awake()
    {
        if(target == null)
            target = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>();
    }
    void OnEnable()
    {
        transform.position = GameObject.FindWithTag("Boss").transform.position;
        float angle = transform.position.x < target.transform.position.x
            ? Random.Range(-30f, 30f)
            : Random.Range(150f, 210f);
        Boss3Audiomanager.instance.PlayAudio(2);
        StartCoroutine(VoidMove(angle));
    }
    IEnumerator VoidMove (float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
        while (gameObject.activeSelf)
        {
            transform.position += transform.right * Time.deltaTime * 20;        
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            camera.Debuff(3f);
        }

        if (other.CompareTag("LeftWall") || other.CompareTag("RightWall"))
        {
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }
    }
}
