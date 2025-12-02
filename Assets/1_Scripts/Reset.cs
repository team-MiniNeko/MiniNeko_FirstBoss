using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public SkillData[] skillData;
    private void Start()
    {
        PlayerPrefs.DeleteKey("FirstTime");
        PlayerPrefs.DeleteKey("SceneCount");
        PlayerPrefs.DeleteKey("FallenAngelDied");
        PlayerPrefs.DeleteKey("isSKillFirst");
        foreach (var kv in skillData)
        {
            string key = "skill" + kv.name;
            PlayerPrefs.SetInt(key, kv.maxRank);
            PlayerPrefs.Save();
        }
        PlayerStatsManager.Instance.SetStats();
    }
}
