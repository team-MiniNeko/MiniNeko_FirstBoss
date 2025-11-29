using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Net;

public class SwordGate : MonoBehaviour
{
    private SpriteRenderer sr;

    public GameObject sword;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1,1,1,0);
    }
    void Start()
    {
        float startAlpha = sr.color.a;
        DOTween.To(
            () => startAlpha,
            x =>
            {
                startAlpha = x;
                Color c = sr.color;
                c.a = startAlpha;
                sr.color = c;
            },
            1f,
            1f
        ).SetEase(Ease.OutQuad);
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        var swordAttack = Instantiate(sword, this.transform.position + new Vector3(-2,0,0), Quaternion.identity);
        if (sr.flipX)
        {
            swordAttack.transform.Find("LightSwordSource").GetComponent<LightSwordAttack>().isFlip = -1;
            swordAttack.transform.rotation = Quaternion.Euler(180, 180, 0);
        }
        swordAttack.transform.SetParent(this.transform);

        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
