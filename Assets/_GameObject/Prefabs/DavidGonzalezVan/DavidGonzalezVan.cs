using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DavidGonzalezVan : SimpleVan
{
    private Vector3 direction = Vector3.forward;
    private Vector3 rotation = Vector3.zero;

    [SerializeField] GameObject[] puntos;

    [SerializeField] float rango;
    private Transform puntoActual;

    private bool seguir = true;
    private bool noRepetirMas = false;
    private float r;
    private int p;

    private float angularSpeedOriginal;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(47.3f, -3.748332e-19f, -47.75735f);
        rotation = new Vector3(0, angularSpeed, 0);
        angularSpeedOriginal = angularSpeed;

        for (int i = 0; i < puntos.Length; i++)
        {
            Instantiate(puntos[i]);
        }
        p = 0;
        CambioPunto();

        seguir = true;

        Time.timeScale = 1;
    }
    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 target = new Vector3(puntoActual.transform.position.x, transform.position.y, puntoActual.transform.position.z);

        if(seguir)
        {
            Quaternion q = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * angularSpeed);
        }
        else
        {
            transform.Rotate(rotation * Time.deltaTime);
        }
        if(!noRepetirMas && !seguir)
        {
            InvokeRepeating("GirarRandom", 0, 3);
            noRepetirMas = true;
        }

        if (Vector3.Distance(transform.position, target) < rango)
        {
            p++;
            if (p < puntos.Length)
            {
                CambioPunto();
            }
            else
            {
                seguir = false;
            }
        }

    }

    private void CambioPunto()
    {
        puntoActual = puntos[p].transform;
    }

    private void GirarRandom()
    {
        angularSpeed = Random.Range(-45, 46);
        if(angularSpeed > 15)
        {
            angularSpeed = 45;
        }
        else if(angularSpeed < -15)
        {
            angularSpeed = -45;
        }
        else
        {
            angularSpeed = 1;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(TAG_PARED))
        {
            if (seguir)
            {
                speed = -speed;
                Invoke("Revelocitar", 2);
            }
            else speed = -speed;
        }
    }

    private void Revelocitar()
    {
        speed = -speed;
    }
}
