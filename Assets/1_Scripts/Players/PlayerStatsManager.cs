using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;
    public float playerAttack;
    public float playerHp;
    public float playerDefence;
    public float playerSpeed;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            SetStats();
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
        }
        else
        {
            LoadStats();
        }
    }
    void SetStats()
    {
        PlayerPrefs.SetFloat("PlayerAttack", 550);
        PlayerPrefs.SetFloat("PlayerHp", 1000);
        PlayerPrefs.SetFloat("PlayerDefence", 300);
        PlayerPrefs.SetFloat("PlayerSpeed", 14);
        LoadStats();
    }
    void LoadStats()
    {
        playerAttack = PlayerPrefs.GetFloat("PlayerAttack");
        playerHp = PlayerPrefs.GetFloat("PlayerHp");
        playerDefence = PlayerPrefs.GetFloat("PlayerDefence");
        playerSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
    }
}
