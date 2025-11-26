using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MouseScript : MonoBehaviour
{
    public int Mouse;//0 LMB
    public Sprite changeSprite;
    Sprite OriginSprite;
    public bool wasChanged = false;
    // Start is called before the first frame update
    void Start()
    {
        OriginSprite = GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(Mouse))
        {   
            gameObject.GetComponent<Image>().sprite = changeSprite;
            return;
        } 
        if (Input.GetMouseButtonUp(Mouse))
        {   
            gameObject.GetComponent<Image>().sprite = OriginSprite;
            return;
        } 
    }
}
