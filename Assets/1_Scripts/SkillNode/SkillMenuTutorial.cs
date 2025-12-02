using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillMenuTutorial : MonoBehaviour
{
    public TextMeshProUGUI text;
    private void Start()
    {
        StartCoroutine(TextAnim());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive(false);
            PlayerPrefs.SetInt("isSKillFirst", 1);
        }
    }
    IEnumerator TextAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            text.gameObject.SetActive(false);
        }
    }
}
