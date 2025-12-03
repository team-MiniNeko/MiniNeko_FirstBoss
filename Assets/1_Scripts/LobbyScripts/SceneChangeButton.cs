using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public float statam = 1f;
    public void sceneChange(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("PlayerAttack", 20 * 0.5f * (5 - statam));
        PlayerPrefs.SetFloat("PlayerHp", 1000 * 0.5f * (5 - statam));
        Debug.Log(PlayerPrefs.GetFloat("PlayerHp"));
        PlayerPrefs.SetFloat("PlayerDash", 100);
        PlayerPrefs.Save();
        PlayerStatsManager.Instance.LoadStats();
        SceneManager.LoadScene("Tutorial");
    }
    public void scenereChange()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
