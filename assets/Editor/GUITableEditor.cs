using UnityEditor;
using UnityEngine;
using EditorGUITable;

[CustomEditor(typeof(GUITableExample))]
[CanEditMultipleObjects]
public class GUITableExampleEditor : Editor {

    GUITableState vegetationTable;
    SerializedProperty vegetation;
    bool showVeg = false;

    void OnEnable()
    {
        vegetationTable = new GUITableState("vegetationTable");
        vegetation = serializedObject.FindProperty("vegetation");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUITableExample tableScript = (GUITableExample) target;
        EditorGUI.indentLevel++;
        showVeg = EditorGUILayout.Foldout(showVeg, "Vegetation");
        if (showVeg)
        {
            vegetationTable = GUITableLayout.DrawTable(vegetationTable,
                                        serializedObject.FindProperty("vegetation"));
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+"))
            {
                tableScript.AddRow();
            }
            if (GUILayout.Button("-"))
            {
                tableScript.RemoveRow();
            }

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
