using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerVan : SimpleVan
{
    private Vector3 direction = Vector3.forward;
    private Vector3 rotation = Vector3.zero;
    public Transform inicial;
    protected const string TAG_OBSTACULO = "Obstaculo";
    private int ladosTocados, ladosCompletados,numPared,numObstaculo;
    public Transform detector;
    public float distanciaDeteccion;
    public LayerMask layerMuro;
    public Transform[] esquina;
    private int numesquina, numlateral, momento;

    protected override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        transform.Rotate(rotation * Time.deltaTime);
        bool pivotgolpe = Physics.Raycast(detector.transform.position, detector.transform.forward, distanciaDeteccion, layerMuro);
        RaycastHit[] pivote = Physics.RaycastAll(detector.transform.position, detector.transform.forward, distanciaDeteccion, layerMuro);
        
        if (pivote != null)
        {
            print("choque muro: " + pivotgolpe);
        }

        Puntodeejecucion(momento);
        HacerLateral(numlateral);
        HacerEsquina(numesquina);

    }

    private void Puntodeejecucion(int momento)
    {
        int i,j;
        if (momento == 0) 
        {
            for (i=0;i<360;i++) 
            {
                detector.transform.rotation = Quaternion.Euler(0, 1, 0);
                RaycastHit[] pivote = Physics.RaycastAll(detector.transform.position, (detector.transform.forward), distanciaDeteccion, layerMuro);
                
                if (pivote[i].transform.position.x > pivote[i - 1].transform.position.x) 
                {
                     esquina[i] = pivote[i].transform;

                }
                   

            }
            for (j = 0; j < 360; j++) 
                {
                detector.transform.rotation = Quaternion.Euler(0, 1, 0);
                detector.transform.rotation = Quaternion.Euler(1, 0, 0);
                RaycastHit[] pivote = Physics.RaycastAll(detector.transform.position, (detector.transform.forward), distanciaDeteccion, layerMuro);
                if (esquina[j].transform.position.x < esquina[j-1].transform.position.x) 
                    {
                    //esquina superior derecha
                }

                momento += 1;
                esquina[numesquina].transform.position = pivote[j].transform.position;
            }

            
            numesquina = numesquina + 1;


        }

        throw new NotImplementedException();
    }

    private void HacerEsquina(int numesquina) {
        throw new NotImplementedException();
    }

    private void HacerLateral(int numesquina)    
    {
        throw new NotImplementedException();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        int i, j;
        bool esEsquina;
        bool lado1, lado2, lado3, lado4;

        base.OnTriggerEnter(other);
        
        if (other.gameObject.CompareTag(TAG_PARED))
        {
            numPared += 1;

            speed = -speed;
            rotation = new Vector3(0, angularSpeed, 0);

        }
    
 
    }
    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(detector.transform.position, detector.transform.forward * distanciaDeteccion, Color.yellow);
    }
}
