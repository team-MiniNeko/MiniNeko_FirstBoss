using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillData))]
public class SkillDataEditor : Editor
{
    private List<Type> skillTypes;
    private string[] skillNames;
    private int selectedIndex = -1;

    private void OnEnable()
    {
        skillTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IWeakening).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        skillNames = skillTypes.Select(t => t.Name).ToArray();

        var skillSO = (SkillData)target;
        if (skillSO.skillName != null)
        {
            selectedIndex = skillTypes.IndexOf(skillSO.skillName.GetType());
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        SerializedProperty skillProp = serializedObject.FindProperty("weakening");

        if (skillProp == null)
        {
            EditorGUILayout.HelpBox("'skill' ������Ƽ�� ã�� �� �����ϴ�.", MessageType.Error);
            return;
        }

        // ��Ӵٿ�
        int typeIndex = EditorGUILayout.Popup("Skill Type", selectedIndex, skillNames);

        if (typeIndex != selectedIndex)
        {
            selectedIndex = typeIndex;

            var newType = skillTypes[selectedIndex];
            skillProp.managedReferenceValue = Activator.CreateInstance(newType);
        }

        // ���� �ʵ� ǥ��
        if (skillProp.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;

            // foldout ���� ��ü �ʵ� ǥ��
            EditorGUILayout.PropertyField(skillProp, true);

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }



}
