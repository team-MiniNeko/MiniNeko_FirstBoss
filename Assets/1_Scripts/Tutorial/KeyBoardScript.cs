using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardScript : MonoBehaviour
{
    public KeyCode Key;
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
        if (Input.GetKeyDown(Key))
        {   
            wasChanged = true;
            gameObject.GetComponent<Image>().sprite = changeSprite;
            return;
        } 
        if (Input.GetKeyUp(Key))
        {
            gameObject.GetComponent<Image>().sprite = OriginSprite;
            return;
        }
    }
}
