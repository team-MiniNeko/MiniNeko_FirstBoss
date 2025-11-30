using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenAngelPattern : MonoBehaviour
{
    public FallenAngelAttack fallenAngel;
    public GameObject player;
    private int curPhase = 0;
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
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Debug.LogError("...?!?!?");
                    break;
            }
            curPhase = fallenAngel.phase;
        }
    }
    //public IEnumerator PhaseOne()
    //{

    //}
    //public IEnumerator PhaseTwo()
    //{

    //}
    //public IEnumerator PhseThree()
    //{

    //}
}
