using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FallenAngelMove : MonoBehaviour
{
    public FallenAngelAttack fallenAngelAttack;
    public int curPhase = 0;
    public GameObject fallenAngel;
    public SpriteRenderer sr;
    private bool isRight = true;

    public GameObject shield;

    public float speed = 1f;     // 움직임 속도
    public float sizeX = 3f;     // 무한대 가로 크기
    public float sizeY = 2f;     // 무한대 세로 크기
    private float t = 0f;
    private Vector3 origin;
    public IEnumerator PhaseOne()
    {
        while (curPhase == fallenAngelAttack.phase && fallenAngelAttack.phase == 1)
        {
            if (isRight)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    yield return fallenAngel.transform.DOMove(new Vector3(-40, -5.8f, -1), 5f);
                    yield return new WaitForSeconds(5f);
                    yield return sr.flipX = true;
                    isRight = false;

                }
                else
                {
                    yield return shield.GetComponent<ShieldHp>().curHp += shield.GetComponent<ShieldHp>().maxHp / 2;
                    shield.gameObject.SetActive(true);
                    yield return new WaitForSeconds(3f);
                }
            }
            else
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    yield return fallenAngel.transform.DOMove(new Vector3(40, -5.8f, -1), 5f);
                    yield return new WaitForSeconds(5f);
                    yield return sr.flipX = false;
                    isRight = true;

                }
                else
                {
                    yield return shield.GetComponent<ShieldHp>().curHp += shield.GetComponent<ShieldHp>().maxHp / 2;
                    shield.gameObject.SetActive(true);
                    yield return new WaitForSeconds(3f);
                }
            }
        }
        
    }
    public IEnumerator PhaseTwo()
    {
        while (curPhase == fallenAngelAttack.phase && fallenAngelAttack.phase == 2)
        {
            if (isRight)
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    yield return fallenAngel.transform.DOMove(new Vector3(-40, -5.8f, -1), 3f);
                    yield return new WaitForSeconds(5f);
                    yield return sr.flipX = true;
                    isRight = false;
                }
                else
                {
                    yield return shield.GetComponent<ShieldHp>().curHp += shield.GetComponent<ShieldHp>().maxHp / 2;
                    shield.gameObject.SetActive(true);
                    yield return new WaitForSeconds(2f);
                }
            }
            else
            {
                int rand = Random.Range(0, 2);
                if (rand == 0)
                {
                    yield return fallenAngel.transform.DOMove(new Vector3(40, -5.8f, -1), 3f);
                    yield return new WaitForSeconds(5f);
                    yield return sr.flipX = false;
                    isRight = true;
                }
                else
                {
                    yield return shield.GetComponent<ShieldHp>().curHp += shield.GetComponent<ShieldHp>().maxHp / 2;
                    shield.gameObject.SetActive(true);
                    yield return new WaitForSeconds(2f);
                }
            }
        }
        
    }
    public IEnumerator PhaseThree()
    {
        while(curPhase == fallenAngelAttack.phase && fallenAngelAttack.phase == 3)
        {
            yield return new WaitForSeconds(5f);
            t += Time.deltaTime * speed;

            float x = sizeX * Mathf.Sin(t);
            float y = sizeY * Mathf.Sin(t * 2);

            transform.position = origin + new Vector3(x, y, 0f);
        }
        yield return new WaitForSeconds(0f);
    }
    private void Start()
    {
        origin = transform.position;
    }
    private void Update()
    {
        if (curPhase != fallenAngelAttack.phase)
        {
            switch (fallenAngelAttack.phase)
            {
                case 1:
                    Debug.LogError("1페이지 움직임");
                    curPhase++;
                    StartCoroutine(PhaseOne());
                    break;
                case 2:
                    Debug.LogError("2페이지 움직임");
                    curPhase++;
                    StartCoroutine(PhaseTwo());
                    break;
                case 3:
                    Debug.LogError("3페이지 움직임");
                    curPhase++;
                    StartCoroutine(PhaseThree());
                    break;
                default:
                    Debug.LogError("...?!?!?");
                    break;
            }
        }
    }
}
