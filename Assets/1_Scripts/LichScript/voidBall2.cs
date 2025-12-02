using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VoidBall2 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject[] VoidBallCha;
    private voidBall2a[] voidBall2a;
    void Awake()
    {
        if(target == null)
            target = GameObject.FindWithTag("Player");
        voidBall2a = new voidBall2a[VoidBallCha.Length];
        voidBall2a[0] = VoidBallCha[0].GetComponent<voidBall2a>();
        voidBall2a[1] = VoidBallCha[1].GetComponent<voidBall2a>();
        voidBall2a[2] = VoidBallCha[2].GetComponent<voidBall2a>();
        voidBall2a[3] = VoidBallCha[3].GetComponent<voidBall2a>();
        voidBall2a[4] = VoidBallCha[4].GetComponent<voidBall2a>();
    }
    void OnEnable()
    {
        float[] angle = transform.position.x < target.transform.position.x
            ? new float[] { -30, -15, 0, 15, 30 }
            : new float[] { 150, 165, 180, 195, 210 };
        voidBall2a[0].transform.position = transform.position;
        voidBall2a[1].transform.position = transform.position;
        voidBall2a[2].transform.position = transform.position;
        voidBall2a[3].transform.position = transform.position;
        voidBall2a[4].transform.position = transform.position;
        Boss3Audiomanager.instance.PlayAudio(2);
        StartCoroutine(VoidMove(angle));
    }

    IEnumerator VoidMove(float[] angle)
    {
        for (int i = 0; i < VoidBallCha.Length; i++)
        {
            VoidBallCha[i].transform.rotation = Quaternion.Euler(0, 0, angle[i]);
            StartCoroutine(voidBall2a[i].MoveSingleVoid());
        }

        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
    }
}