using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaniaguaVan : SimpleVan
{
    private Vector3 direction = Vector3.forward;
    private Vector3 rotation = Vector3.zero;
    protected override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.Rotate(rotation * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag(TAG_PARED))
        {
            speed = -speed;
            rotation = new Vector3(0,angularSpeed,0);
        }
    }
}
