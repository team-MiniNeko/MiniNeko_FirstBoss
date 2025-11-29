using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightSwordAttack : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    public int isFlip = 1;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        sr.color = new Color(1,1,1,0);
        sr.sortingOrder = -1;

        if (isFlip == -1)
        {
            Vector3 curPos = transform.parent.position + new Vector3 (2,0,0);
            Vector3 startPos = transform.parent.position + Vector3.right * 2f * isFlip + new Vector3(2, 0, 0);
            transform.position = startPos;

            StartCoroutine(Attack());
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMove(curPos, 1f));
            seq.Join(sr.DOFade(1f, 1f));
        }
        else
        {
            Vector3 curPos = transform.parent.position;
            Vector3 startPos = transform.parent.position + Vector3.right * 2f * isFlip;
            transform.position = startPos;

            StartCoroutine(Attack());
            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMove(curPos, 1f));
            seq.Join(sr.DOFade(1f, 1f));

        }

    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        sr.sortingOrder = 5;
        Vector3 startPos = transform.parent.position + Vector3.right * 2f * isFlip;
        yield return new WaitForSeconds(0.2f);
        //rb.AddForce(Vector2.left * 4444 * isFlip);
        transform.parent.DOMove(this.transform.position + new Vector3(-200 * isFlip,0 , 0), 5);
    }
    
}