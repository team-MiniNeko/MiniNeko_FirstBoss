using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatDisplayScript : MonoBehaviour
{
    // Start is called before the first frame update
    float ATK,SPD,HP;
    private TextMeshProUGUI textUi;
    void Start()
    {
        textUi = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ATK = PlayerStatsManager.Instance.PlayerAttack;
        SPD = PlayerStatsManager.Instance.PlayerDash;
        HP = PlayerStatsManager.Instance.PlayerHp;
        textUi.text = $"ATK: {ATK}\nSPD: {SPD}\nmax HP:{HP}";
    }
}
