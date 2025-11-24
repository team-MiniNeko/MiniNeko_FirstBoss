using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardScript : MonoBehaviour
{
    public KeyCode Key;
    public Sprite changeSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            gameObject.GetComponent<Image>().sprite = changeSprite;
            return;
        } 
    }
}
