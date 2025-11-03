using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngelAttack : MonoBehaviour
{
    private GameObject player;
    [SerializeField]

    public GameObject chain;
    public GameObject ChainSkillRange;

    public GameObject sword;
    public GameObject swordGate;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        ChainAttack();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChainAttack();
        }
    }

    void ChainAttack()
    {
        var chainLeft = Instantiate(chain,(player.transform.position - new Vector3(24,24,0)), Quaternion.identity);
        var chainRight = Instantiate(chain, (player.transform.position + new Vector3(24, -24, 0)), Quaternion.identity);
        chainRight.GetComponent<Transform>().Rotate(0,180,0);
        chainLeft.GetComponent<ChainAttack>().isFilp = 1;
        chainRight.GetComponent<ChainAttack>().isFilp = -1;
    }

    void SwordAttack()
    {
        var firstGate = Instantiate(swordGate, player.transform.position + new Vector3(9, -1, 0), Quaternion.identity);
        var secondGate = Instantiate(swordGate, player.transform.position + new Vector3(6, 9, 0), Quaternion.identity);
        var thirdGate = Instantiate(swordGate, player.transform.position + new Vector3(0, 12, 0), Quaternion.identity);
        var fourthGate = Instantiate(swordGate, player.transform.position + new Vector3(-6, 9, 0), Quaternion.identity);
        var fifthGate = Instantiate(swordGate, player.transform.position + new Vector3(-9, -1, 0), Quaternion.identity);

    }
}
