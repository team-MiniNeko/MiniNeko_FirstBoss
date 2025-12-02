using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraLobbyMoveScript : MonoBehaviour
{
    public Transform target;
    public void sceneChange()
    {
        Debug.Log("why not move wtf");
        GameObject.FindWithTag("MainCamera").GetComponent<CameraScripts>().Target = target;
    }
}