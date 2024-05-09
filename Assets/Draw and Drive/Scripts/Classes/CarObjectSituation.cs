using UnityEngine;

namespace CanvasHaHa
{
    public class CarObjectSituation
    {
        public Vector3 position;
        public Quaternion rotation;
        public CarObjectSituation(Transform obj)
        {
            if (obj == null) return;
            position = obj.position;
            rotation = obj.rotation;
        }
        public void setValueFor(Transform obj)
        {
            if (obj == null) return;
            obj.position = position;
            obj.rotation = rotation;
        }
    }
}