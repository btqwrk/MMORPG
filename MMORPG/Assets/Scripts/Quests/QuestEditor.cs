using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty questType;
    SerializedProperty turnInNPC;
    SerializedProperty currencyReward;
    SerializedProperty itemRewardType;
    SerializedProperty predefinedItemRewards;
    SerializedProperty itemRewardCount; // Add itemRewardCount
    SerializedProperty objectives;

    private void OnEnable()
    {
        questType = serializedObject.FindProperty("questType");
        turnInNPC = serializedObject.FindProperty("turnInNPC");
        currencyReward = serializedObject.FindProperty("currencyReward");
        itemRewardType = serializedObject.FindProperty("itemRewardType");
        predefinedItemRewards = serializedObject.FindProperty("predefinedItemRewards"); // Updated property name
        itemRewardCount = serializedObject.FindProperty("itemRewardCount"); // Add itemRewardCount
        objectives = serializedObject.FindProperty("objectives");
    }

    public override void OnInspectorGUI()
    {
        if (serializedObject != null)
        {
            // Access serialized properties here
            // ...

            serializedObject.Update();

            EditorGUILayout.PropertyField(questType);
            EditorGUILayout.PropertyField(turnInNPC);
            EditorGUILayout.PropertyField(currencyReward);

            // Allow switching the item reward type
            EditorGUILayout.PropertyField(itemRewardType);

            if (itemRewardType.enumValueIndex == 0) // Predefined Items
            {
                EditorGUILayout.PropertyField(predefinedItemRewards, true);
            }
            else if (itemRewardType.enumValueIndex == 1) // Choose Items
            {
                EditorGUILayout.PropertyField(itemRewardCount, new GUIContent("Item Reward Count"));
                EditorGUILayout.PropertyField(predefinedItemRewards, true);
                // Additional UI for choosing items (dropdown, etc.) can be added here.
            }

            EditorGUILayout.PropertyField(objectives, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif