using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        Appear();
    }
    void Appear()
    {
        Vector3 targetPos = coo - new Vector3(13 * isFlip, 13, 0);

        DOTween.To(
                () => transform.position,
                x => transform.position = x,
                targetPos,
                0.5f
        );
    }
    public void DisAppear()
    {
        Vector3 targetPos = coo - new Vector3(24 * isFlip, 24, 0);

        DOTween.To(
                () => transform.position,
                x => transform.position = x,
                targetPos,
                0.5f
        );
    }
}
