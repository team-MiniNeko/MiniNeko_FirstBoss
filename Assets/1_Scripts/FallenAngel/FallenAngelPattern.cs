using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngelPattern : MonoBehaviour
{
    public FallenAngelAttack fallenAngel;
    public GameObject player;
    public int curPhase = 0;
    public IEnumerator PatternOne()
    {
        for (int i = -50; i <= 50; i += 10)
        {
            StartCoroutine(fallenAngel.LightAttack(new Vector3(i, 0, 0)));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 45; i >= -50; i -= 10)
        {
            StartCoroutine(fallenAngel.LightAttack(new Vector3(i, 0, 0)));
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator PatternTwo()
    {
        StartCoroutine(fallenAngel.LightSwordAttack());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(fallenAngel.DarkSwordAttack());
    }
    public IEnumerator PatternThree()
    {
        StartCoroutine(fallenAngel.LightAttack(player.transform.position));
        StartCoroutine(fallenAngel.LightSwordAttack());
        yield return new WaitForSeconds(0f);
    }
    public IEnumerator PatternFour()
    {
        StartCoroutine(fallenAngel.LightDarkLightAttack());
        StartCoroutine(fallenAngel.DarkSwordAttack());
        yield return new WaitForSeconds(0f);
    }
    public void Update()
    {
        if (curPhase != fallenAngel.phase)
        {
            switch (fallenAngel.phase)
            {
                case 1:
                    Debug.LogError($"{curPhase}, {fallenAngel.phase}");
                    curPhase = fallenAngel.phase;
                    StartCoroutine(PhaseOne());
                    break;
                case 2:
                    curPhase = fallenAngel.phase;
                    StartCoroutine(PhaseTwo());
                    break;
                case 3:
                    curPhase = fallenAngel.phase;
                    StartCoroutine(PhseThree()); 
                    break;
                default:
                    Debug.LogError("...?!?!?");
                    break;
            }
        }
    }
    public IEnumerator PhaseOne()
    {
        Debug.LogError($"{curPhase}, {fallenAngel.phase}");
        while (curPhase == fallenAngel.phase && fallenAngel.phase == 1)
        {
            Debug.LogError("1페이이지");
            int value = Random.Range(0, 2);
            if (value == 0)
            {
                yield return StartCoroutine(PatternOne());
            }
            else
            {
                yield return StartCoroutine(PatternThree());
            }
            yield return StartCoroutine(fallenAngel.ChainAttack());
        }
    }
    public IEnumerator PhaseTwo()
    {
        while (curPhase == fallenAngel.phase && fallenAngel.phase == 2)
        {
            int value = Random.Range(0, 4);
            if (value == 0)
            {
                yield return StartCoroutine(PatternOne());
            }
            else if (value == 1)
            {
                yield return StartCoroutine(PatternThree());
            }
            else if (value == 2)
            {
                yield return StartCoroutine(PatternFour());
            }
            else if (value == 3)
            {
                yield return StartCoroutine(PatternTwo());
            }
            yield return new WaitForSeconds(1.5f);
            yield return StartCoroutine(fallenAngel.ChainAttack());
            yield return new WaitForSeconds(1.5f);
        }
    }
    public IEnumerator PhseThree()
    {
        while (curPhase == fallenAngel.phase && fallenAngel.phase == 3)
        {
            int value = Random.Range(0, 4);
            if (value == 0)
            {
                yield return StartCoroutine(fallenAngel.ChainAttack());
                yield return StartCoroutine(PatternOne());
            }
            else if (value == 1)
            {
                yield return StartCoroutine(fallenAngel.ChainAttack());
                yield return StartCoroutine(PatternThree());
            }
            else if (value == 2)
            {
                yield return StartCoroutine(fallenAngel.ChainAttack());
                yield return StartCoroutine(PatternFour());
            }
            else if (value == 3)
            {
                yield return StartCoroutine(fallenAngel.ChainAttack());
                yield return StartCoroutine(PatternTwo());
            }
            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(fallenAngel.ChainAttack());
            yield return new WaitForSeconds(0.3f);
        }
    }
}
