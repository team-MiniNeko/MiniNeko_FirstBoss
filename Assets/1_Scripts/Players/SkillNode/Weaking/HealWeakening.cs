using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealWeaking : IWeakening
{
    public void Weakening(int figure)
    {
        PlayerStatsManager.Instance.PlayerHeal -= figure;
    }
    public string Weakening(){return "Heal per Tick";}
}
