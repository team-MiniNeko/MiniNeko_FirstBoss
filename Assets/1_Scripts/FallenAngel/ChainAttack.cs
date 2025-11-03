using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChainAttack : MonoBehaviour
{
    private GameObject player;
    public int isFilp = 1;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        Vector3 targetPos = player.transform.position - new Vector3(13*isFilp,13,0);

        DOTween.To(
                () => transform.position,         // getter
                x => transform.position = x,      // setter
                targetPos,                        // target
                0.5f                              // duration
        );
        Invoke("Destroy", 1f);
    }
    
}
