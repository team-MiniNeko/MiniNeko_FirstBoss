using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject button;
    private void Start()
    {
        //StartCoroutine(TypeEffect(text, "'너'와 함께한 '나'의 이야기는 이걸로 끝이야"));
        //StartCoroutine(TypeEffect(text, "하지만 '우리'의 이야기는 영원히 끝나지 않을거야"));
        StartCoroutine(start());
    }
    IEnumerator start()
    {
        StartCoroutine(TypeEffect(text, "Thank You For Playing This Game"));
        yield return new WaitForSeconds(5f);
        StartCoroutine(TypeEffect(text, "WANT:Oblivion"));
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
    }
    public IEnumerator TypeEffect(TextMeshProUGUI text, string fullText, float speed = 0.1f)
    {
        text.text = "";

        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

}
