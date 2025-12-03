using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    public string sceneName;
    public void sceneChange()
    {
        SceneManager.LoadScene(sceneName);
    }
}
