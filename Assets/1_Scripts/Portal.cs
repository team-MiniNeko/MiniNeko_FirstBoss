using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class Portal : MonoBehaviour
{
    public string sceneName;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            SceneManager.LoadScene(sceneName);
    }
}
