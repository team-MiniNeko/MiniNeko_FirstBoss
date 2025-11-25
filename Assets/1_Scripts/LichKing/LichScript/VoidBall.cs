using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoidBall : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject target;
    public Material material;
    void Awake()
    {
        if(target == null)
            target = GameObject.FindWithTag("Player");
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
        while (Mathf.Abs(transform.position.x) < 200)
        {
            transform.position += transform.right * Time.deltaTime * 20;        
            yield return null;
        }
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>().Debuff(3f);
        }
    }
}
