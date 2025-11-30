using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;
    private float playerAttack = 20;
    private float playerHp = 1000;
    private float playerDefence = 300;
    private float playerDash = 100;
    public float PlayerAttack 
    {   
        get => playerAttack;
        set {
            playerAttack = value;
            PlayerPrefs.SetFloat("PlayerAttack", this.PlayerAttack);
            Debug.Log($"Attack : {playerAttack}");
        } 
    }
    public float PlayerHp 
    { 
        get => playerHp;
        set
        {
            playerHp = value;
            PlayerPrefs.SetFloat("PlayerHp", this.PlayerHp);
            Debug.Log($"Hp : {playerHp}");
        }
    }
    public float PlayerDefence
    { 
        get => playerDefence;
        set
        {
            playerDefence = value;
            PlayerPrefs.SetFloat("PlayerDefence", this.playerDefence);
            Debug.Log($"Defence : {playerDefence}");
        }
    }
    public float PlayerDash
    { 
        get => playerDash;
        set
        {
            playerDash = value;
            PlayerPrefs.SetFloat("PlayerDash", playerDash);
            Debug.Log($"Dash : {playerDash}");
        }
    }
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
        PlayerPrefs.DeleteKey("FirstTime");
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
        Debug.LogError("SEtSTats");
        PlayerPrefs.SetFloat("PlayerAttack", 20);
        PlayerPrefs.SetFloat("PlayerHp", 1000);
        PlayerPrefs.SetFloat("PlayerDefence", 300);
        PlayerPrefs.SetFloat("PlayerDash", 100);
        LoadStats();
    }
    void LoadStats()
    {
        Debug.LogError("LoadSTats");
        PlayerAttack = PlayerPrefs.GetFloat("PlayerAttack");
        PlayerHp = PlayerPrefs.GetFloat("PlayerHp");
        PlayerDefence = PlayerPrefs.GetFloat("PlayerDefence");
        PlayerDash = PlayerPrefs.GetFloat("PlayerDash");
    }
}
