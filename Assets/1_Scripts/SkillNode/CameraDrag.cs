using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    public Vector2 defaultPos;
    public RectTransform skillTree;
    private void Start()
    {
        var par = GameObject.Find("SkillTree");
        skillTree = par.transform.Find("SkillTreee").GetComponent<RectTransform>();
        if (skillTree == null)
        {
            Debug.LogError("오브젝트 연결 x");
        }
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(skillTree.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out defaultPos);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 currentPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(skillTree.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out currentPos);
        skillTree.anchoredPosition -= defaultPos - currentPos;
        defaultPos = currentPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        //defaultPos = eventData.position;
    }
}