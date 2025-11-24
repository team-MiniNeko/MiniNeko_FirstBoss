using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUI : MonoBehaviour
{
    public Image icon;
    public GameObject lockOverlay;
    public TextMeshProUGUI rankText;
    public Button button;

    public SkillData skillData;
    public int currentRank = 0;

    public event Action<SkillNodeUI> onClicked;
    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(() => onClicked?.Invoke(this));
            ReFresh();
        }
    }

    public void ReFresh()
    {
        if (skillData != null)
            icon.sprite = skillData.icon;
        lockOverlay.SetActive(!IsUnlocked());
        rankText.text = $"{currentRank}/{(skillData != null ? skillData.maxRank : 0)}";
    }

    public bool IsUnlocked()
    {
        return currentRank > 0;
    }

    public void SetRank(int r)
    {
        currentRank = Math.Clamp(r, 0, skillData.maxRank);
        ReFresh();
    }
}

