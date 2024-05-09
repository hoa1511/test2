using UnityEngine;

namespace CanvasHaHa
{
    [System.Serializable]
    public class PathNode
    {
        public GameObject gameObject;
        public PathNode() { }
        public PathNode(GameObject go)
        {
            gameObject = go;
        }

        public Vector3 getPoint()
        {
            return gameObject.transform.position;
        }

        public void destroy()
        {
            Object.Destroy(gameObject);
        }

        ~PathNode()
        {
            destroy();
        }
    }
}