using UnityEngine;

namespace CanvasHaHa
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    #region enums
    public enum State { Null, Start, Draw, Ready, Movement, Finished }
    #endregion
    public class CarObject : MonoBehaviour
    {
        #region variable
        #region public
        //[Header("Parameter(s)")]
        public float speed = 1f;
        public float turnStrength = 40f;
        public float height = 1f;
        public float targetHitDistance = 0.001f;
        public bool obstacleSupport = true;
        public Player player;


        #region Events
        public UnityEvent onIdleModeEnter;
        public UnityEvent onMovingModeEnter;
        #endregion
        //[Header("Control")]
        public KeyCode fixPositionKey = KeyCode.Space;
        //[Header("Brush")]
        public bool showBrush = true;
        public GameObject brush;
        public float brushSize = 0.01f;
        public float brushExplanationDistance = 0.1f;
        public float brushHeight = 0.1f;
        //[Header("Line")]
        public bool showLine = true;
        public float lineWidth = 0.1f;
        public float lineHeight = 0.01f;
        public Material material;
        //[Header("Info")]
        //public State currentState = State.Start;
        public float bendAngle;

        public bool hittedEnd = false;

        public int currentIndex = 0;
        #endregion
#if UNITY_EDITOR
        [HideInInspector] public bool parametersEnable = true;
        [HideInInspector] public bool controlEnable = true;
        [HideInInspector] public bool brushEnable = true;
        [HideInInspector] public bool lineEnable = true;
        [HideInInspector] public bool infoEnable = true;
#endif
        #region private
        private LineRenderer helperLine;
        private string canvasLayerMask;

        private List<PathNode> nodes = new List<PathNode>();
        private List<GameObject> lines = new List<GameObject>();
        private bool isMoving = false;
        private Vector3 targetPosition = Vector3.zero;
        private GameObject garbage;
        private Collider _collider;
        private Ground canvas;
        private bool lockCanvas = false;
        private CarObjectSituation startSituatuin;
        private int curLine = 0;

        

        
        #endregion
        #endregion
        #region geter and setter
        private State _playerState = State.Null;
        public State playerState
        {
            get { return _playerState; }
            set
            {
                switch (value)
                {
                    case State.Start:
                        if (_playerState != State.Start)
                        {
                            runIdleEvent();
                        }
                        break;
                    case State.Movement:
                        if (_playerState != State.Movement)
                        {
                            runMoveEvent();
                        }
                        break;
                    case State.Finished:
                        if (_playerState != State.Finished)
                        {
                            runIdleEvent();
                        }
                        break;
                    default:
                        if (onIdleModeEnter != null) onIdleModeEnter.RemoveAllListeners();
                        if (onMovingModeEnter != null) onIdleModeEnter.RemoveAllListeners();
                        break;
                }
                _playerState = value;
            }
        }
        public void setCanvas(Ground node)
        {
            canvas = node;
        }

        public void setCanvasLayerMask(string val)
        {
            canvasLayerMask = val;
        }

        #endregion
        #region Functions

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();

            player = transform.GetComponent<Player>();
        }

        void Start()
        {
            Init();
        }
        private void OnDrawGizmos()
        {
            if (nodes.Count > 0)
            {
                Vector3 p1 = nodes[0].getPoint();
                Vector3 p2 = nodes[nodes.Count - 1].getPoint();
                Gizmos.color = Color.red;
                Gizmos.DrawLine(p1, p2);
            }
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                switch (playerState)
                {
                    case State.Draw:
                        draw();
                        break;
                    case State.Start:
                        {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (_collider.Raycast(ray, out hit, int.MaxValue))
                            {
                                playerState = State.Draw;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (lockCanvas)
                {
                    if (canvas.isLock)
                    {
                        lockCanvas = canvas.isLock = false;
                        helperLine.enabled = false;
                    }
                }
                if (playerState == State.Draw && nodes.Count > 0)
                {
                    playerState = State.Ready;
                }
            }

            if (playerState == State.Finished)
            {
                if (Input.GetKeyDown(fixPositionKey))
                {
                    FixeCurrentPosition();
                    Clear();
                }
                else if (Input.GetMouseButton(1))
                {
                    Clear();
                }
            }
        }

        private void FixedUpdate()
        {
            if (playerState == State.Movement)
            {
                if(hittedEnd == true)
                {
                    player.RunPlayer();
                    Moving();
                    move(targetPosition, targetHitDistance);
                }
                else
                {
                    Clear();
                    playerState = State.Start;
                }
            }
        }
        
        #endregion
        #region draw
        private void draw()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (nodes.Count < 1)
            {
                RaycastHit hit1;
                if (_collider.Raycast(ray, out hit1, Mathf.Infinity))
                {
                    if (!lockCanvas)
                    {
                        if (canvas.isLock) return;
                        lockCanvas = canvas.isLock = true;
                    }
                }
                else
                {
                    return;
                }
            }
            updateHelperLine();
            bool hitValid = false;
            RaycastHit hit;
            if (obstacleSupport)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (!(hitValid = LayerMask.LayerToName(hit.collider.gameObject.layer) == canvasLayerMask)
                        && hit.collider.gameObject != this.gameObject)
                    {
                        return;
                    }
                }
                if (!hitValid && !Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask(canvasLayerMask)))
                {
                    return;
                }
            }
            else
            {
                if (!Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask(canvasLayerMask)))
                {
                    return;
                }
            }

            if (!hitPointIsValid(hit.point)) return;

            if (hit.collider.gameObject.CompareTag("Door"))
            {
                if(hittedEnd == false)
                {
                    hittedEnd = true;
                    playerState = State.Movement;
                }
            }

            var node = Instantiate(brush, hit.point, transform.rotation);
            node.SetActive(showBrush);
            node.name = nodes.Count.ToString();
            node.transform.SetParent(garbage.transform);
            node.transform.localScale = new Vector3(brushSize, brushSize, brushSize);
            var nodeClass = new PathNode(node);
            if (nodes.Count > 0)
            {
                Vector3 angle = (node.transform.position - nodes[nodes.Count - 1].gameObject.transform.position);
                Quaternion rotation = Quaternion.LookRotation(angle, Vector3.up);
                node.transform.rotation = rotation;
                drawSkid(nodes[nodes.Count - 1], nodeClass);
            }
            nodes.Add(nodeClass);
        }
        private bool hitPointIsValid(Vector3 point)
        {
            if (nodes.Count < 1) return true;

            bool result = true;

            Vector3 start = nodes[nodes.Count - 1].getPoint();
            float dist = Vector3.Distance(point, start);
            if (dist < brushExplanationDistance) return false;

            Vector3 direction = (point - start).normalized;

            IDictionary<Vector3, bool> candidates = new Dictionary<Vector3, bool>();
            Vector3 candidate = start;
            while (true)
            {
                candidate += (direction * brushExplanationDistance);
                if (Vector3.Distance(candidate, start) > dist) break;
                bool hitting = candidatePointIsValid(candidate);
                Color color;
                if (hitting)
                {
                    color = Color.green;
                }
                else
                {
                    result = false;
                    color = Color.red;
                }
                candidates.Add(candidate, hitting);
            }
            foreach (KeyValuePair<Vector3, bool> kvp in candidates)
            {
                Color c = kvp.Value ? Color.green : Color.red;
                Debug.DrawLine(Camera.main.transform.position, kvp.Key, c);
            }

            return result;
        }
        private bool candidatePointIsValid(Vector3 candidate)
        {
            float dist = Vector3.Distance(Camera.main.transform.position, candidate);
            RaycastHit hit;
            Vector3 v = candidate + transform.up * dist;
            if (obstacleSupport)
            {
                if (Physics.Raycast(v, -transform.up, out hit, Mathf.Infinity))
                {
                    if (LayerMask.LayerToName(hit.collider.gameObject.layer) == canvasLayerMask
                        || hit.collider.gameObject == this.gameObject)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return Physics.Raycast(v, -transform.up, out hit, Mathf.Infinity);
            }
            return false;
        }
        #region skid pattern
        private void drawSkid(PathNode start, PathNode end)
        {
            drawSkid(start.getPoint(), end.getPoint());
        }
        private Vector3[] lastSkidPoint = new Vector3[] { Vector3.zero, Vector3.zero };
        private void drawSkid(Vector3 start, Vector3 end)
        {
            if (!showLine) return;
            Vector3 p1 = new Vector3(start.x, start.y + lineHeight, start.z);
            Vector3 p2 = new Vector3(end.x, end.y + lineHeight, end.z);

            GameObject newLine = new GameObject("Line");
            newLine.transform.SetParent(garbage.transform);

            newLine.AddComponent<MeshRenderer>().sharedMaterial = material;
            MeshFilter meshFilter = newLine.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
            meshFilter.GetComponent<MeshRenderer>().sharedMaterial = material;

            //lineWidth
            Vector3 vector = p2 - p1;
            Vector3 dir = Vector3.Cross(vector, Vector3.up).normalized;

            float halfLineWidth = (lineWidth / 2);
            Vector3 a = p1 + dir * halfLineWidth;
            Vector3 b = p1 - dir * halfLineWidth;
            Vector3 c = p2 + dir * halfLineWidth;
            Vector3 d = p2 - dir * halfLineWidth;
            //
            if (lastSkidPoint[0] != Vector3.zero)
            {
                drawSoftenSkid(vector.normalized, new Vector3[] { lastSkidPoint[0], lastSkidPoint[1], a, b });
            }

            lastSkidPoint[0] = c;
            lastSkidPoint[1] = d;
            //
            Mesh mesh = meshFilter.sharedMesh;
            mesh.Clear();
            mesh.vertices = new Vector3[] { a, b, c, d };

            Vector2[] uvs = new Vector2[mesh.vertices.Length];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
            }
            mesh.uv = uvs;

            mesh.triangles = new int[] {
                0, 2, 1,
                2, 3, 1
            };
            mesh.RecalculateNormals();
            lines.Add(newLine);

        }
        private void drawSoftenSkid(Vector3 direction, Vector3[] vertices)
        {
            GameObject newLine = new GameObject("SoftenLine");
            newLine.transform.SetParent(garbage.transform);

            newLine.AddComponent<MeshRenderer>().sharedMaterial = material;
            MeshFilter meshFilter = newLine.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh();
            meshFilter.GetComponent<MeshRenderer>().sharedMaterial = material;
            //
            Mesh mesh = meshFilter.sharedMesh;
            mesh.Clear();
            mesh.vertices = vertices;

            Vector2[] uvs = new Vector2[mesh.vertices.Length];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(mesh.vertices[i].x, mesh.vertices[i].z);
            }
            mesh.uv = uvs;

            List<int> triangles = new List<int>();
            Vector3 dir02 = (vertices[2] - vertices[0]).normalized;

            if (Vector3.Dot(dir02, direction) > 0)
            {
                triangles.Add(0);
                triangles.Add(2);
                triangles.Add(1);
            }
            Vector3 dir13 = (vertices[3] - vertices[1]).normalized;
            if (Vector3.Dot(dir13, direction) > 0)
            {
                triangles.Add(3);
                triangles.Add(1);
                triangles.Add(2);
            }

            mesh.triangles = triangles.ToArray();

            mesh.RecalculateNormals();
            lines.Add(newLine);
        }
        #endregion
        private void updateHelperLine()
        {
            Vector3 gizmoPosition = canvas.getGizmoPosition();
            if (gizmoPosition == Vector3.zero || nodes.Count < 1)
            {
                helperLine.enabled = false;
                return;
            }
            helperLine.enabled = true;
            helperLine.startColor = Color.black;
            helperLine.endColor = Color.black;
            helperLine.startWidth = lineWidth;
            helperLine.endWidth = lineWidth;
            helperLine.positionCount = 2;
            helperLine.useWorldSpace = true;
            helperLine.material = material;

            helperLine.SetPosition(0, nodes[nodes.Count - 1].getPoint());
            helperLine.SetPosition(1, gizmoPosition);
        }

        public void Clear()
        {
            if (nodes.Count > 0)
            {
                startSituatuin.setValueFor(this.transform);
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].destroy();
                nodes[i] = null;
            }
            for (int i = 0; i < lines.Count; i++)
            {
                UnityEngine.Object.Destroy(lines[i]);
            }
            nodes.Clear();
            lines.Clear();
        }
        #endregion
        #region Movement
        private void Moving()
        {
            if (isMoving || nodes.Count < 1) return;
            if (currentIndex == 0)
            {
                setPosition(new Vector3(nodes[0].gameObject.transform.position.x, 0, nodes[0].gameObject.transform.position.z));
            }
            if (currentIndex < nodes.Count)
            {
                if (targetPosition == Vector3.zero)
                {
                    if(curLine + 2 <= lines.Count)
                    {
                        lines[curLine].SetActive(false);
                        lines[curLine + 1].SetActive(false);
                        curLine += 2;
                    }
                    else
                    {
                        lines[curLine].SetActive(false);
                    }

                    calculateBendAngle(currentIndex);

                    if(currentIndex != 1)
                        nodes[currentIndex].gameObject.SetActive(false);

                    targetPosition = new Vector3(nodes[currentIndex].gameObject.transform.position.x, 0, nodes[currentIndex].gameObject.transform.position.z) ;
                    currentIndex++;
                }
            }
            else
            {
                currentIndex = 0;
                playerState = State.Finished;
            }
        }
        private void move(Vector3 target, float targetRadios)
        {
            if (target == Vector3.zero) return;

            isMoving = true;
            {
                Vector3 relativePos = new Vector3(target.x, 0, target.z)
                    - transform.position;
                if (relativePos != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotation,
                        Time.deltaTime * speed * turnStrength);
                    
                    
                }
            }

            setPosition(Vector3.MoveTowards(transform.position,
                new Vector3(target.x, 0, target.z),
                speed * Time.deltaTime));

            if (Vector3.Distance(transform.position, new Vector3(target.x,
                0, target.z)) < targetHitDistance)
            {
                targetPosition = Vector3.zero;
                isMoving = false;
            }
        }
        private void calculateBendAngle(int index)
        {
            if (index == nodes.Count - 1)
            {
                bendAngle = 0;
            }
            else
            {
                Vector3 v1 = nodes[index].gameObject.transform.position - transform.position;
                Vector3 v2 = nodes[index + 1].gameObject.transform.position - nodes[index].gameObject.transform.position;
                bendAngle = Vector3.Angle(v1, v2);
            }
        }
        private void setPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public void FixeCurrentPosition()
        {
            Debug.Log("Current position is fixed");
            startSituatuin = new CarObjectSituation(this.transform);
        }
        #endregion
        #region Events
        private void runIdleEvent()
        {
            if (onIdleModeEnter != null)
            {
                onIdleModeEnter.Invoke();
            }
            if (onMovingModeEnter != null)
            {
                onMovingModeEnter.RemoveAllListeners();
            }
        }
        private void runMoveEvent()
        {
            if (onIdleModeEnter != null)
            {
                onIdleModeEnter.RemoveAllListeners();
            }
            if (onMovingModeEnter != null)
            {
                onMovingModeEnter.Invoke();
            }
        }
        #endregion
        public void Init()
        {
            FixeCurrentPosition();
            garbage = new GameObject("paths");
            if (brush == null)
            {
                brush = new GameObject("brush");
                brush.transform.SetParent(garbage.transform);
            }
            helperLine = new GameObject("Line").AddComponent<LineRenderer>();
            helperLine.transform.SetParent(garbage.transform);
            helperLine.enabled = false;
        }
    }
}
