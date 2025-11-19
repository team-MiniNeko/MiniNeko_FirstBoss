using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    Vector3 targetScale;
    Vector3 originalScale;
    public Color changeColor;
    Color originColor;
    Color TargetColor;
    float Xmove = 0f;
    Object textColor;
    void Start()
    {
        targetScale = new Vector3(1.2f,1.2f,1.2f);
        originalScale = new Vector3(1f,1f,1f);
        originColor = gameObject.GetComponent<TextMeshProUGUI>().color;
        TargetColor = originColor;
    }

    // Update is called once per frame
    public void ChangeColor(Color Tcolor)
    {
        float r = gameObject.GetComponent<TextMeshProUGUI>().color.r;
        float g = gameObject.GetComponent<TextMeshProUGUI>().color.g;
        float b = gameObject.GetComponent<TextMeshProUGUI>().color.b;
        Color newColor = new Color(r+(Tcolor.r-r)*(Time.deltaTime*10f),g+(Tcolor.g-g)*(Time.deltaTime*10f),b+(Tcolor.b-b)*(Time.deltaTime*10f));
        gameObject.GetComponent<TextMeshProUGUI>().color = newColor;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 목표 크기로 설정
        transform.localScale = new Vector3(1.2f,1.2f,1.2f);
        Xmove += 0.5f;
        Debug.Log($"ENTER:{gameObject.name}");
        TargetColor = changeColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // 목표 크기로 설정
        Xmove -= 0.5f;
        transform.localScale = new Vector3(1f,1f,1f);
        Debug.Log($"EXIT:{gameObject.name}");
        TargetColor = originColor;
    }
    void Update(){
        if (transform.localScale != targetScale && transform.localScale != originalScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 10f);
        }
        if(Xmove != 0){
            Debug.Log($"Xmove{Xmove}");
            transform.position = new Vector3(transform.position.x+Xmove*Time.deltaTime*10,transform.position.y);
            Xmove = Xmove-(Xmove*Time.deltaTime*10);
            if(math.abs(Xmove) <= 0.001f){
                transform.position = new Vector3(transform.position.x+Xmove,transform.position.y);
                Xmove = 0;
            }
        }
        ChangeColor(TargetColor);
    }
}
