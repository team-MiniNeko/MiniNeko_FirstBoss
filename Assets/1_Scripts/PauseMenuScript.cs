using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool ispaused;
    public GameObject UIImage;
    void Start()
    {
        ispaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                UIImage.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                UIImage.SetActive(false);
            }
        }
    }
}
