#if UNITY_EDITOR
namespace MyApp.DrawAndDrive.Menu
{
    using UnityEditor;
    using UnityEngine;
    using CanvasHaHa;
    using System.Collections.Generic;

    public class MainMenu : MonoBehaviour
    {
        #region Component
        public static void CreateComponentIfNotExits<T>(string name) where T : Component
        {
            var node = FindObjectOfType<T>();
            if (node == null)
            {
                AddComponentToObject<T>(name);
            }
            else
            {
                Selection.objects = new Object[] { node.gameObject };
            }
        }
        public static void AddComponentToObject<T>(string name) where T : Component
        {
            GameObject node = new GameObject(name);
            node.transform.SetPositionAndRotation(getPosition(), Quaternion.identity);
            node.AddComponent<T>();
            Undo.RegisterCreatedObjectUndo(node, "Create object");
            Selection.objects = new Object[] { node };
        }
        public static void AttachComponentToSelection<T>() where T : Component
        {
            if (Selection.objects == null) return;
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                var node = (GameObject)Selection.objects[i];
                if (node == null || node.GetComponent<T>() != null) continue;
                Undo.AddComponent<T>(node);
            }
        }
        public static void RemoveComponentFromSelection<T>() where T : Component
        {
            if (Selection.objects == null) return;
            T function;
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                var node = (GameObject)Selection.objects[i];
                if (node == null || (function = node.GetComponent<T>()) == null) continue;
                Undo.DestroyObjectImmediate(function);
            }
        }
        public static void SelectAllObjectsByComponent<T>() where T : Component
        {
            var nodes = FindObjectsOfType<T>();
            List<Object> result = new List<Object>();
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (node == null) continue;
                    result.Add(node.gameObject);
                }
            }

            Selection.objects = result.ToArray();
        }
        #endregion
        #region Ground
        [MenuItem(Globals.RootName + "/" + Globals.PROJECT_NAME + "/Ground/Add (or) Select")]
        private static void AddOrSelectGround()
        {
            GameObject obj;
            var item = FindObjectOfType<Ground>();
            if (item == null)
            {
                obj = new GameObject("Ground");
                obj.AddComponent<Ground>();
                Undo.RegisterCreatedObjectUndo(obj, "Create Ground");
                obj.transform.position = getPosition();
            }
            else
            {
                obj = item.gameObject;
            }
            Selection.objects = new Object[] { obj };
        }
        #endregion
        #region CarObject
        [MenuItem(Globals.RootName + "/" + Globals.PROJECT_NAME + "/Car Object/Add script")]
        private static void AddCarObject()
        {
            AttachComponentToSelection<CarObject>();
        }
        [MenuItem(Globals.RootName + "/" + Globals.PROJECT_NAME + "/Car Object/Select all")]
        private static void SelectAllCarObject()
        {
            SelectAllObjectsByComponent<CarObject>();
        }

        [MenuItem(Globals.RootName + "/" + Globals.PROJECT_NAME + "/Car Object/Remove script")]
        private static void RemoveCarObject()
        {
            RemoveComponentFromSelection<CarObject>();
        }
        #endregion
        #region About us
        [MenuItem(Globals.RootName + "/Publisher Page")]
        public static void PublisherPage()
        {
            Application.OpenURL("https://assetstore.unity.com/publishers/48757");
        }
        #endregion
        #region Support
        [MenuItem(Globals.RootName + "/Support")]
        public static void Support()
        {
            TextWindow window = (TextWindow)EditorWindow.GetWindow(typeof(TextWindow), true, "Support");
            window.Descriptions = new string[] { "If you need any further assistance, please contact us", "unrealisticarts@gmail.com" };
        }
        #endregion
        private static Vector3 getPosition()
        {
            var item = FindObjectOfType<Ground>();
            if (item == null) return Vector3.zero;
            return item.transform.position;
        }
    }
}
#endif