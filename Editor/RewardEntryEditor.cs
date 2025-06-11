using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RewardEntry))]
public class RewardEntryEditor : Editor
{
    
    SerializedProperty rarityType;
    SerializedProperty stars;
    SerializedProperty letterRanking;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        RewardEntry rewardEntry = (RewardEntry)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("rewardName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rewardSprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rewardType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rarityType"));
        rarityType = serializedObject.FindProperty("rarityType");
        stars = serializedObject.FindProperty("stars");
        letterRanking = serializedObject.FindProperty("letterRanking");
        RarityType selectedRarity = (RarityType)rarityType.enumValueIndex;
        if (selectedRarity == RarityType.StarNumber)
        {
            EditorGUILayout.PropertyField(stars);
        }
        else if (selectedRarity == RarityType.Letter)
        {
            EditorGUILayout.PropertyField(letterRanking);
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("probability"));
        EditorGUILayout.Space();
        DrawPreview(rewardEntry);


        serializedObject.ApplyModifiedProperties();
    }

    private void DrawPreview(RewardEntry entry)
    {

        Color previewColor = entry.GetColor();
        string label = entry.GetLabel();

        GUIStyle previewStyle = new GUIStyle(GUI.skin.box)
        {
            normal = { background = Texture2D.whiteTexture },
            padding = new RectOffset(10, 10, 5, 5)
        };

        Color previousColor = GUI.color;
        GUI.color = previewColor * new Color(1, 1, 1, 0.2f);

        EditorGUILayout.BeginVertical(previewStyle);
        GUI.color = previousColor;

        GUILayout.Label($"🎁 Reward: {entry.RewardName}", EditorStyles.boldLabel);
        GUILayout.Label($"⭐ Rarity: {label}", new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Italic });

        EditorGUILayout.EndVertical();
    }
}
