using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWeak : IWeakening
{
    public void Weakening(int figure)
    {
        PlayerStatsManager.Instance.PlayerDash -= figure;
    }
    public void Weakening(float figure)
    {
        PlayerStatsManager.Instance.PlayerDash -= (PlayerStatsManager.Instance.PlayerDash * (figure / 100));
    }
}
