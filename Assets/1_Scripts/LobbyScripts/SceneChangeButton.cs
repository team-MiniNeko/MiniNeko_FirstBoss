using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public void sceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void sceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }
}
