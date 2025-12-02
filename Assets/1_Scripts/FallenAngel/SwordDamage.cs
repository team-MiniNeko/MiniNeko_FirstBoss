using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int swordDamage = 15;
    private CameraScripts camera;
    public string type;
    private void Awake()
    {
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(swordDamage);
            camera.deBuffType = type;
            camera.Debuff(1f);
        }
    }
}
