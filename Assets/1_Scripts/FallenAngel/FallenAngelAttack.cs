using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallenAngelAttack : MonoBehaviour
{
    private GameObject player;
    [SerializeField]

    public GameObject chain;
    public GameObject chainSkillRange;

    public GameObject lightSword;
    public GameObject lightSwordGate;
    public GameObject darkSword;
    public GameObject darkSwordGate;
    public GameObject swordAttackRange;

    public GameObject lightAttack;
    public GameObject lightSkillRange;

    public GameObject lightDarkLight;
    public GameObject lightDarkLightSkillRange;

    public EnemyHealthScript bossHp;
    public int phase;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        Phase();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (phase == 3)
            {
                phase = 1;
            }
            else
            {
                phase++;
            }
        }
    }
    void Phase()
    {
        if (bossHp.Health > bossHp.StartHealth/2)
        {
            phase = 1;
        }
        else if (bossHp.Health < bossHp.StartHealth / 2)
        {
            phase = 2;
        }
    }
    public IEnumerator ChainAttack()
    {
        switch (phase)
        {
            case 1:
                Vector3 coo = player.transform.position;
                var chainAttackRange = Instantiate(chainSkillRange, coo, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Destroy(chainAttackRange);
                yield return new WaitForSeconds(0.5f);
                var chainLeft = Instantiate(chain, (coo - new Vector3(24, 24, 0)), Quaternion.identity);
                var chainRight = Instantiate(chain, (coo + new Vector3(24, -24, 0)), Quaternion.identity);
                chainRight.GetComponent<Transform>().Rotate(0, 180, 0);
                chainLeft.GetComponent<ChainAttack>().isFlip = 1;
                chainRight.GetComponent<ChainAttack>().isFlip = -1;
                chainLeft.GetComponent<ChainAttack>().coo = coo;
                chainRight.GetComponent<ChainAttack>().coo = coo;
                yield return new WaitForSeconds(5f);
                //Destroy(chainLeft);
                //Destroy(chainRight);
                break;
            case 2:
                Vector3 coo2 = player.transform.position;
                var chainAttackRange2 = Instantiate(chainSkillRange, coo2, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Destroy(chainAttackRange2);
                yield return new WaitForSeconds(0.5f);
                var chainLeft2 = Instantiate(chain, (coo2 - new Vector3(24, 24, 0)), Quaternion.identity);
                var chainRight2 = Instantiate(chain, (coo2 + new Vector3(24, -24, 0)), Quaternion.identity);
                chainRight2.GetComponent<Transform>().Rotate(0, 180, 0);
                chainLeft2.GetComponent<ChainAttack>().isFlip = 1;
                chainRight2.GetComponent<ChainAttack>().isFlip = -1;
                chainLeft2.GetComponent<ChainAttack>().coo = coo2;
                chainRight2.GetComponent<ChainAttack>().coo = coo2;
                yield return new WaitForSeconds(5f);
                //Destroy(chainLeft2);
                //Destroy(chainRight2);
                break;
            case 3:
                break;
        }
        
    }

    public IEnumerator LightSwordAttack()
    {
        var swordGateOneRight = Instantiate(lightSwordGate, new Vector3(50, player.transform.position.y, 0), Quaternion.identity);
        var swordGateTwoLeft = Instantiate(lightSwordGate, new Vector3(-50, player.transform.position.y + 6, 0), Quaternion.identity);
        var sworGateThreeLeft = Instantiate(lightSwordGate, new Vector3(-50, player.transform.position.y - 6, 0), Quaternion.identity);
        swordGateTwoLeft.GetComponent<SpriteRenderer>().flipX = true;
        sworGateThreeLeft.GetComponent<SpriteRenderer>().flipX = true;
        var swordAttackRangeOne = Instantiate(swordAttackRange, new Vector3(0, player.transform.position.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var swordAttackRangeTwo = Instantiate(swordAttackRange, new Vector3(0, player.transform.position.y + 6, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var swordAttackRangeThree = Instantiate(swordAttackRange, new Vector3(0, player.transform.position.y - 6, 0), Quaternion.identity);
        Destroy(swordAttackRangeOne, 1f);
        Destroy(swordAttackRangeTwo, 1f);
        Destroy(swordAttackRangeThree, 1f);
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator DarkSwordAttack()
    {
        Vector3 coo = player.transform.position;
        var swordGateOneLeft = Instantiate(darkSwordGate, new Vector3(-50, coo.y, 0), Quaternion.identity);
        var swordGateTwoRight = Instantiate(darkSwordGate, new Vector3(50, coo.y + 6, 0), Quaternion.identity);
        var sworGateThreeRight = Instantiate(darkSwordGate, new Vector3(50, coo.y - 6, 0), Quaternion.identity);
        swordGateOneLeft.GetComponent<SpriteRenderer>().flipX = true;
        var swordAttackRangeOne = Instantiate(swordAttackRange, new Vector3(0, coo.y, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var swordAttackRangeTwo = Instantiate(swordAttackRange, new Vector3(0, coo.y + 6, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        var swordAttackRangeThree = Instantiate(swordAttackRange, new Vector3(0, coo.y - 6, 0), Quaternion.identity);
        Destroy(swordAttackRangeOne, 1f);
        Destroy(swordAttackRangeTwo, 1f);
        Destroy(swordAttackRangeThree, 1f);
        yield return new WaitForSeconds(0.5f);
    }
    public IEnumerator LightAttack(Vector3 loc)
    {
        Vector3 coo = loc;
        var lightAttackRange = Instantiate(this.lightSkillRange,coo, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(lightAttackRange);
        coo = new Vector3(coo.x,coo.y, -0.1f);
        var lightAttack = Instantiate(this.lightAttack, coo, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(lightAttack);
    }
    public IEnumerator LightDarkLightAttack()
    {
        Vector3 coo = new Vector3(player.transform.position.x, 0, 0);
        var leftLightAttackRange = Instantiate(this.lightDarkLightSkillRange, coo + new Vector3(-7,0,0), Quaternion.identity);
        var rightLightAttackRange = Instantiate(this.lightDarkLightSkillRange, coo + new Vector3(7, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(leftLightAttackRange);
        Destroy(rightLightAttackRange);
        coo = new Vector3(coo.x, -3.5f, -0.1f);
        var leftLightAttack = Instantiate(this.lightDarkLight, coo + new Vector3(-7, 0, 0), Quaternion.identity);
        var rightLightAttack = Instantiate(this.lightDarkLight, coo + new Vector3(7, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Destroy(leftLightAttack);
        Destroy(rightLightAttack);
    }
}
