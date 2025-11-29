using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    private List<Type> skillTypes;
    private string[] skillNames;
    private int selectedIndex = -1;

    private void OnEnable()
    {
        // ISkill 구현체 찾기
        skillTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IWeakening).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        skillNames = skillTypes.Select(t => t.Name).ToArray();

        // 현재 할당된 타입의 인덱스 찾기
        var skillSO = (SkillData)target;
        if (skillSO.skillName != null)
        {
            selectedIndex = skillTypes.IndexOf(skillSO.skillName.GetType());
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty skillProp = serializedObject.FindProperty("weakening");

        if (skillProp == null)
        {
            EditorGUILayout.HelpBox("'skill' 프로퍼티를 찾을 수 없습니다.", MessageType.Error);
            return;
        }

        // 드롭다운
        int typeIndex = EditorGUILayout.Popup("Skill Type", selectedIndex, skillNames);

        if (typeIndex != selectedIndex)
        {
            selectedIndex = typeIndex;

            var newType = skillTypes[selectedIndex];
            skillProp.managedReferenceValue = Activator.CreateInstance(newType);
        }

        // 내부 필드 표시
        if (skillProp.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;

            // foldout 포함 전체 필드 표시
            EditorGUILayout.PropertyField(skillProp, true);

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }



}
