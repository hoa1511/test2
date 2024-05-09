using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleSawBladeObstacle : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private Transform tripleSawBladeObstacleTransform;

    public void HandleRotate()
    {
        Vector3 rotateAxis = new Vector3(0,1,0);
        tripleSawBladeObstacleTransform.Rotate(rotateAxis * Time.unscaledDeltaTime * rotateSpeed);
    }

    void Start()
    {
        tripleSawBladeObstacleTransform = this.transform;
    }

    
    void Update()
    {
        HandleRotate();
    }
}
