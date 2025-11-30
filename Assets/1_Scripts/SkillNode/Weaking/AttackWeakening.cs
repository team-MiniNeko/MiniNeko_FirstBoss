using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeakening : IWeakening
{
    public void Weakening(int figure)
    {
        PlayerStatsManager.Instance.PlayerAttack -= figure;
    }
    public void Weakening(float figure)
    {
        PlayerStatsManager.Instance.PlayerAttack -= (PlayerStatsManager.Instance.PlayerAttack * (figure/100));
    }
    public string Weakening(){return "Attack";}
}
