using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightAttack : MonoBehaviour
{
    BoxCollider2D box;
    int y = 0;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        DOTween.To(() => box.size, x => box.size = x, new Vector2(17f, 30f), 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = 35;
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().Damage(damage);
        }
    }
}
