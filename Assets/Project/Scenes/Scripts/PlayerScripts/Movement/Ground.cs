using System;
using System.Collections.Generic;
using UnityEngine;
using SceneManagement.MyCamera;

namespace CanvasHaHa
{
    public class Ground : MonoBehaviour
    {
        #region Variables
        #region Public
        [Header("Node(s)")]
        public List<CarObject> carObjects;
        public bool syncMode = false;
        [Header("Settings")]
        public bool gizmoEnable = true;
        public GameObject gizmo;
        //[NonSerialized]
        [HideInInspector] public bool isLock = false;
        #endregion
        #region private
        private float deltaTime;
        private CameraOrbit cameraOrbit;
        private bool lastCameraOrbitEnable;
        #endregion
#if UNITY_EDITOR
        [HideInInspector] public bool nodesEnable = true;
        [HideInInspector] public bool settingsEnable = true;
#endif
        #endregion
        #region Functions
        private void Awake()
        {
            init();
        }
        private void Start()
        {
            if (!gizmoEnable)
            {
                gizmo = new GameObject();
            }
            foreach (CarObject node in carObjects)
            {
                node.setCanvasLayerMask(LayerMask.LayerToName(gameObject.layer));
                node.setCanvas(this);
                node.playerState = State.Start;
            }
        }
        private void Update()
        {
            updateCarState();
            updateMousePosition();
            setCameraOrbit_enable(!isLock);
        }
        #endregion
        #region functions
        private void updateCarState()
        {
            if (syncMode)
            {
                changeStateInSyncMode();
            }
            else
            {
                changeStateInAsyncMode();
            }
        }
        private void changeStateInAsyncMode()
        {
            foreach (CarObject node in carObjects)
            {
                if (node.playerState == State.Ready)
                {
                    node.playerState = State.Movement;
                }
            }
        }
        private void changeStateInSyncMode()
        {
            foreach (CarObject node in carObjects)
            {
                if (node.playerState != State.Ready)
                {
                    return;
                }
            }
            changeStateInAsyncMode();
        }
        private void updateMousePosition()
        {
            Vector3 mouse = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (gizmoEnable)
                    gizmo.SetActive(true);
                gizmo.transform.position = hit.point;
            }
            else
            {
                if (gizmoEnable)
                    gizmo.SetActive(false);
            }
        }
        public Vector3 getGizmoPosition()
        {
            if (gizmo.activeInHierarchy) return gizmo.transform.position;
            return Vector3.zero;
        }
        private void initBoxCollider()
        {
            GameObject invisible = new GameObject("invisible");
            BoxCollider _c = this.GetComponent<BoxCollider>();
            BoxCollider c = invisible.AddComponent<BoxCollider>();
            c.center = _c.center;
            c.size = _c.size;
            c.transform.position = _c.transform.position;
            c.transform.rotation = _c.transform.rotation;
            c.transform.localScale = new Vector3(_c.transform.localScale.x * 2,
                _c.transform.localScale.y,
                _c.transform.localScale.z * 2);
        }
        private void init()
        {
            if (carObjects == null)
            {
                carObjects = new List<CarObject>();
                var list = FindObjectsOfType<CarObject>();
                for (int i = 0; i < list.Length; i++)
                {
                    carObjects.Add(list[i]);
                }
            }
            if (carObjects.Count < 1)
            {
                Debug.LogError("No car object...");
                Extensions.Quit();
            }
            deltaTime = Time.unscaledDeltaTime;
            init_cameraOrbit();
        }
        #endregion
        #region cameraOrbit
        private void init_cameraOrbit()
        {
            if ((cameraOrbit = FindObjectOfType<CameraOrbit>()) != null)
            {
                lastCameraOrbitEnable = isLock;
            }
        }
        public void setCameraOrbit_enable(bool enable)
        {
            if (cameraOrbit == null || lastCameraOrbitEnable == enable) return;
            lastCameraOrbitEnable = cameraOrbit.Enable = enable;
        }
        #endregion
    }
}