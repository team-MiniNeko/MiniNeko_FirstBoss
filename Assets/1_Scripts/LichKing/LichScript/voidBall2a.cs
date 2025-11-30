using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidBall2a : MonoBehaviour
{
    private CameraScripts camera;
    public GameObject ball;
    public Vector3 startLocalPos;
    void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>();
        startLocalPos = ball.transform.localPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camera.Debuff(3f);
        }
    }
    public IEnumerator MoveSingleVoid()
    {
        while (ball.activeInHierarchy)
        {
            transform.position += transform.right * Time.deltaTime * 20;
            yield return null;
        }
    }
}