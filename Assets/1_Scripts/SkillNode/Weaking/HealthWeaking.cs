using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthWeaking : IWeakening
{
    public void Weakening(int figure)
    {
        PlayerStatsManager.Instance.PlayerHp -= figure;
    }
    public void Weakening(float figure)
    {
        PlayerStatsManager.Instance.PlayerHp -= (PlayerStatsManager.Instance.PlayerHp * (figure / 100));
    }
}
