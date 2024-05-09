using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateSawBlade : Obstacle, IDamageable
{
    [SerializeField] private float rotateSpeed;
    private int damage = 100;
    private Transform rotateSawBladeTransform;
    public void HandleRotate()
    {
        Vector3 rotateAxis = new Vector3(0,0,-1);
        rotateSawBladeTransform.Rotate(rotateAxis * Time.unscaledDeltaTime * rotateSpeed);

    }

    protected override void Update()
    {
        HandleRotate();
    }

    protected override void Start()
    {
        rotateSawBladeTransform = this.transform;
    }

    public void HandleDamage(int damage, ICanTakeDamage client)
    {
        client.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ICanTakeDamage client;
            client = other.GetComponent<ICanTakeDamage>();
            HandleDamage(damage,client);
        }
    }
}
