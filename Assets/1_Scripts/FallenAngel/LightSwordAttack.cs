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

        Vector3 startPos = transform.position + Vector3.right * 2f; 

        transform.position = startPos;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveX(transform.position.x - 2f, 1f)); // 오른쪽으로 이동
        seq.Join(sr.DOFade(1f, 1f)); // 동시에 투명도 증가
    }
}
