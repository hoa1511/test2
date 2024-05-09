using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotation * speed * Time.unscaledDeltaTime);
    }
}
