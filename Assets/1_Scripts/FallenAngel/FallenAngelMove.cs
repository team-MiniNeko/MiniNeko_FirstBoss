using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngelMove : MonoBehaviour
{
    public FallenAngelAttack fallenAngelAttack;
    public int curPhase = 0;
    public GameObject fallenAngel;
    public SpriteRenderer sr;
    public bool isRight = true;
    public IEnumerator PhaseOne()
    {
        while (curPhase == fallenAngelAttack.phase)
        {
            yield return new WaitForSeconds(7f);
            if (isRight)
            {
                fallenAngel.transform.DOMove(new Vector3(-40, -5.8f, -1), 3f);
                sr.flipX = true;
            }
            else
            {
                fallenAngel.transform.DOMove(new Vector3(40, -5.8f, -1), 3f);
                sr.flipX = false;
            }
        }
        
    }
    public IEnumerator PhaseTwo()
    {
        while (curPhase == fallenAngelAttack.phase)
        {
            yield return new WaitForSeconds(7f);
            if (isRight)
            {
                yield return fallenAngel.transform.DOMove(new Vector3(-40, -5.8f, -1), 3f);
                sr.flipX = true;
            }
            else
            {
                yield return fallenAngel.transform.DOMove(new Vector3(40, -5.8f, -1), 3f);
                sr.flipX = false;
            }
        }
        
    }
    //public IEnumerator PhaseThree()
    //{

    //}
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
                    curPhase++;
                    StartCoroutine(PhaseTwo());
                    break;
                case 3:
                    curPhase++;
                    //StartCoroutine(PhaseThree());
                    break;
                default:
                    Debug.LogError("...?!?!?");
                    break;
            }
        }
    }
}
