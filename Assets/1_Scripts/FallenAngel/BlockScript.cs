using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BlockScript : MonoBehaviour
{
    public FallenAngelAttack fallenAngel;
    public EnemyHealthScript fallenAngelHp;
    private Vector3 coo;
    private Vector3 origin;
    private void Awake()
    {
        origin = transform.position;
        coo = origin - new Vector3(0, 5, 0);
        if (fallenAngel == null)
        {
            fallenAngel = GameObject.Find("FallenAngel(Sprite)").GetComponent<FallenAngelAttack>();
            fallenAngelHp = GameObject.Find("FallenAngel(Sprite)").GetComponent<EnemyHealthScript>();
        }
        if (fallenAngelHp.Health <= 0)
        {
            coo = origin;
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
            transform.DOMove(coo, 1);
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
            transform.DOMove(origin, 5);
        }
    }
}
