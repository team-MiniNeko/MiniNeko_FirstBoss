using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{

    readonly float[] difficultyMultiple = {2f,1.5f,1f,0.75f};
    public Slider statam;
    public void sceneChange(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    public void GameStart()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("PlayerAttack", 20 * difficultyMultiple[Convert.ToInt32(statam.value-1)]);
        PlayerPrefs.SetFloat("PlayerHp", 1000 * difficultyMultiple[Convert.ToInt32(statam.value-1)]);
        Debug.Log(PlayerPrefs.GetFloat("PlayerHp"));
        PlayerPrefs.SetFloat("PlayerDash", 100);
        if(Convert.ToInt32(statam.value-1) == 3){
            PlayerPrefs.SetFloat("PlayerHeal",0f);
        }
        PlayerPrefs.Save();
        PlayerStatsManager.Instance.LoadStats();
        if(Convert.ToInt32(statam.value) <= 2)
            SceneManager.LoadScene("Tutorial");
        else
            SceneManager.LoadScene("SkillMenu");
    }
    public void scenereChange()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
