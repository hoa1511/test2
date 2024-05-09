#if UNITY_EDITOR
namespace MyApp.DrawAndDrive
{
    using UnityEditor;
    using CanvasHaHa;
    [CustomEditor(typeof(CarObject))]
    public class CarObjectEditor : Editor
    {
#region variable
        private CarObject Target;
#endregion
#region Inspector
        public override void OnInspectorGUI()
        {
            if ((Target = (CarObject)target) == null) return;
            serializedObject.Update();
            Parameter();
            Control();
            Brush();
            Line();
            Info();
            serializedObject.ApplyModifiedProperties();
        }
        private void Parameter()
        {
            if (!EditorTools.Foldout(ref Target.parametersEnable, "Parameter(s)")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "speed", "Speed");
            EditorTools.PropertyField(serializedObject, "turnStrength", "Turn strengt");
            EditorTools.PropertyField(serializedObject, "height", "Height");
            EditorTools.PropertyField(serializedObject, "targetHitDistance", "Target hit distance");
            EditorTools.PropertyField(serializedObject, "obstacleSupport", "Obstacle support");
            EditorTools.PropertyField(serializedObject, "onIdleModeEnter", "on Idle mode enter");
            EditorTools.PropertyField(serializedObject, "onMovingModeEnter", "on Moving mode enter");
            EditorTools.Box_Close();
        }
        private void Control()
        {
            if (!EditorTools.Foldout(ref Target.controlEnable, "Control")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "fixPositionKey", "Fix position Key","Fix object position after moving.");
            EditorTools.Box_Close();
        }
        private void Brush()
        {
            if (!EditorTools.Foldout(ref Target.brushEnable, "Brush")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "showBrush", "Show brush");
            if (Target.showBrush)
            {
                EditorTools.PropertyField(serializedObject, "brush", "Brush");
                EditorTools.PropertyField(serializedObject, "brushSize", "Brush size");
                EditorTools.PropertyField(serializedObject, "brushExplanationDistance", "Brush explanation distance");
                EditorTools.PropertyField(serializedObject, "brushHeight", "Brush height");
            }
            EditorTools.Box_Close();
        }
        private void Line()
        {
            if (!EditorTools.Foldout(ref Target.lineEnable, "Line")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "showLine", "Show line");
            if (Target.showLine)
            {
                EditorTools.PropertyField(serializedObject, "lineWidth", "Line width");
                EditorTools.PropertyField(serializedObject, "lineHeight", "Line height");
                EditorTools.PropertyField(serializedObject, "material", "Material");
            }
            EditorTools.Box_Close();
        }
        private void Info()
        {
            if (!EditorTools.Foldout(ref Target.infoEnable, "Infomation")) return;
            EditorTools.Box_Open();
            EditorTools.Info("State", Target.playerState.ToString());
            EditorTools.Info("Bend angle", Target.bendAngle.ToString());
            EditorTools.Info("Path index", Target.currentIndex.ToString());
            EditorTools.Box_Close();
        }
        #endregion
    }
}
#endif