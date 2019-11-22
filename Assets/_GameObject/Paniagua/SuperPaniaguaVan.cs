using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPaniaguaVan : SimpleVan
{
    public float timeBetweenChangeDirection = 1.5f;
    private Vector3 direction = Vector3.forward;
    private Vector3 rotation;
    //private int[][] areas;
    //SISTEMA DE DETECCION
    public float forwardDetectionDistance;
    //RaycastHit rch;
    //DELETE
    public GameObject sensor;
    protected override void Start()
    {
        base.Start();
        //CrearArea();
        rotation = new Vector3(0, angularSpeed, 0);
        InvokeRepeating("ChangeDirection", timeBetweenChangeDirection, timeBetweenChangeDirection);
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
        }
    }
    private void ChangeDirection()
    {
        if (speed > 0)
        {
            rotation = new Vector3(0, angularSpeed *= -1, 0);
        }
    }
    //private void CrearArea()
    //{
    //    GameObject p1 = GameObject.Find("HigherLeftLimit");
    //    GameObject p2 = GameObject.Find("LowerRightLimit");
    //    int cols = Mathf.CeilToInt(p2.transform.position.x - p1.transform.position.x);
    //    int rows = Mathf.CeilToInt(p2.transform.position.z - p1.transform.position.z);
    //    for(int i = 0; i < cols; i++)
    //    {
    //        for(int j = 0; j < rows; j++)
    //        {
    //            Instantiate(sensor, new Vector3(p1.transform.position.x+i, 0, p1.transform.position.z+j), Quaternion.identity);
    //        }
    //    }
    //}
}
