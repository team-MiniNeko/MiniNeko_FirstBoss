using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlockScript : MonoBehaviour
{
    public FallenAngelAttack fallenAngel;
    private Vector3 coo;
    private void Awake()
    {
        coo = transform.position;
        if (fallenAngel == null)
        {
            fallenAngel = GameObject.Find("FallenAngel(Sprite)").GetComponent<FallenAngelAttack>();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Sequence seq = DOTween.Sequence();
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(this.transform);
        }
        if (fallenAngel.phase == 2 && collision.gameObject.CompareTag("Player"))
        {
            transform.DOMove(coo - new Vector3(0, 30, 0), 3);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
        if (fallenAngel.phase == 2 && collision.gameObject.CompareTag("Player"))
        {
            transform.DOMove(coo, 3);
        }
    }
}
