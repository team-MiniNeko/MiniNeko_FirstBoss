using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightSwordAttack : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Color c = sr.color;
        c.a = 0;
        sr.color = c;

        Vector3 curPos = transform.position;
        Vector3 startPos = transform.position + Vector3.right * 2f; 

        transform.position = startPos;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(curPos, 1f));
        seq.Join(sr.DOFade(1f, 1f)); 
    }
}