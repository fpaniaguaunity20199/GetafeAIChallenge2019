using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeenIdol : SimpleVan
{
    private Vector3 direction = Vector3.forward;
    private Vector3 rotation = Vector3.zero;
    public int contador = 0;
    private bool salir = false;
    private float distance2 = 60f;
    private bool unavez = true;

    protected override void Start()
    {
        base.Start();
    }


    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.Rotate(rotation * Time.deltaTime);
        
        LanzarRayo();
    }

    private void LanzarRayo()
    {
        
        RaycastHit hit;
        //Debug.DrawRay(puntoVista.position, puntoDetectarPlayer.position - puntoVista.position, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.distance < 30 && contador < 3 && !salir &&  hit.transform.CompareTag("Pared"))
            {
                rotation = new Vector3(0, angularSpeed, 0);
                Invoke("CancelRotation", 3.6f);
                contador++;
                salir = true;
            }
            else if (hit.distance < distance2 && contador >= 3 && !salir && hit.transform.CompareTag("Pared"))
            {

                rotation = new Vector3(0, angularSpeed, 0);
                Invoke("CancelRotation", 2f);
                contador++;
                if (unavez)
                {
                    distance2 = distance2 - 10f;
                    unavez = false;
                }
                salir = true;
               
            }
        }
    }

    private void CancelRotation()
    {
        rotation = Vector3.zero;
        salir = false;
    }

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //    if (other.gameObject.CompareTag(TAG_PARED))
    //    {
    //        speed = -speed;
    //        rotation = new Vector3(0,angularSpeed,0);
    //    }
    //}
}
