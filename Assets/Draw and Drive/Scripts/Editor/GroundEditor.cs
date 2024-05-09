#if UNITY_EDITOR
namespace MyApp.DrawAndDrive 
{
    using UnityEditor;
    using CanvasHaHa;
    [CustomEditor(typeof(Ground))]
    public class GroundEditor : Editor
    {
        #region variable
        private Ground Target;
        #endregion
        #region Inspector
        public override void OnInspectorGUI()
        {
            if ((Target = (Ground)target) == null) return;
            serializedObject.Update();
            Nodes();
           Settings();
            serializedObject.ApplyModifiedProperties();
        }
        private void Nodes()
        {
            if (!EditorTools.Foldout(ref Target.nodesEnable, "Nodes")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "carObjects", "Car objects");
            EditorTools.PropertyField(serializedObject, "syncMode", "Sync mode");
            EditorTools.Box_Close();
        }
        private void Settings()
        {
            if (!EditorTools.Foldout(ref Target.settingsEnable, "Settings")) return;
            EditorTools.PropertyField(serializedObject, "gizmoEnable", "Gizmo enable");
            if (Target.gizmoEnable)
            {
                EditorTools.PropertyField(serializedObject, "gizmo", "Gizmo");
            }
        }
        #endregion
    }
}
#endif