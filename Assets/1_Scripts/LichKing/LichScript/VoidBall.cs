using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoidBall : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public Material material;
    public GameObject Light;
    void OnEnable()
    {
        float angle = transform.position.x < target.transform.position.x
            ? Random.Range(-30f, 30f)
            : Random.Range(150f, 210f);
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
            material.color = new Color(0.2392f, 0.2392f, 0.2392f, 1f);
            Light.SetActive(true);
            StartCoroutine(LightUp());
        }
    }

    IEnumerator LightUp()
    {
        yield return new WaitForSeconds(3);
        Light.SetActive(false);
        material.color = new Color(1, 1, 1, 1f);
    }
}
