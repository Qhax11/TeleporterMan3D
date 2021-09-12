#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace _Scripts.EditorScripts
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.HelpBox("Choose *Random* to have the levels come randomly."
                                    + "Choose *Serial* to have the levels come in order.", MessageType.Info);
        }
    }

    [CustomEditor(typeof(CODE_DragAndDrop))]
    public class CodeDragAndDropEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUILayout.HelpBox("Open GameObject and Start Game", MessageType.Info);
            EditorGUILayout.HelpBox("Delete *Test_Mechanics* Parent", MessageType.Warning);
        }
    }
    
    [CustomEditor(typeof(CODE_RotatePlayer))]
    public class CodeRotatePlayerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUILayout.HelpBox("Open GameObject and Start Game", MessageType.Info);
            EditorGUILayout.HelpBox("Delete *Test_Mechanics* Parent", MessageType.Warning);
        }
    }
    [CustomEditor(typeof(CODE_Swerve_L_R))]
    public class CodeSwerveLrEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUILayout.HelpBox("Open GameObject and Start Game", MessageType.Info);
            EditorGUILayout.HelpBox("Delete *Test_Mechanics* Parent", MessageType.Warning);
        }
    }
    [CustomEditor(typeof(CODE_Swerve_LR_UD))]
    public class CodeSwerveLrUdEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUILayout.HelpBox("Open GameObject and Start Game", MessageType.Info);
            EditorGUILayout.HelpBox("Delete *Test_Mechanics* Parent", MessageType.Warning);
        }
    }
}
#endif