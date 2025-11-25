using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "5_ScriptableObject/Skill")]
public class SkillData : ScriptableObject
{
    public string skillName;
    public Sprite icon;
    public int maxRank = 1;
    public SkillData[] preRequisites;
    public List<SkillData> nextSkills;
    [TextArea] public string description;
}
