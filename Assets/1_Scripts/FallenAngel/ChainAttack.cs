using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class ChainAttack : MonoBehaviour
{
    private GameObject player;
    public int isFlip = 1;
    public Vector3 coo;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        StartCoroutine(Appear());
    }
    IEnumerator Appear()
    {
        Vector3 targetPos = coo - new Vector3(13 * isFlip, 13, 0);

        DOTween.To(
                () => transform.position,
                x => transform.position = x,
                targetPos,
                0.5f
        );
        yield return new WaitForSeconds( 2.0f );
        StartCoroutine(DisAppear());
    }
    public IEnumerator DisAppear()
    {
        Vector3 targetPos = coo - new Vector3(24 * isFlip, 24, 0);

        DOTween.To(
                () => transform.position,
                x => transform.position = x,
                targetPos,
                0.5f
        ); 
        yield return new WaitForSeconds(0.5f);
        this.transform.rotation = quaternion.Euler(90, 0, 0);
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
