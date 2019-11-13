using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SillyVan : SimpleVan
{
    private Vector3 direction = Vector3.forward;
    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag(TAG_PARED))
        {
            speed = -speed;
        }
    }
}
