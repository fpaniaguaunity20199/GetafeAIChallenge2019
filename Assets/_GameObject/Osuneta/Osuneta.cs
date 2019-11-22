using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osuneta : SimpleVan
{
    [SerializeField] float angularSpeedMin;
    [SerializeField] float angularSpeedMax;
    [SerializeField] float tiempoHastaRectoMin;
    [SerializeField] float tiempoHastaRectoMax;
    [SerializeField] float tiempoActivarGirar;

    private Vector3 direction = Vector3.forward;
    private Vector3 rotation = Vector3.zero;
    private bool giro = true;
    private float tiempoHastaRecto;

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

            if (giro)            {

                angularSpeed = Random.Range(angularSpeedMin, angularSpeedMax);
                rotation = new Vector3(0, angularSpeed, 0);
                tiempoHastaRecto = Random.Range(tiempoHastaRectoMin, tiempoHastaRectoMax);
                CancelInvoke();
                Invoke("ConducirRecto", tiempoHastaRecto);
                Invoke("ActivarGirar", tiempoActivarGirar);
            }

            giro = !giro;
        }
    }

    private void ConducirRecto()
    {
        rotation = new Vector3(0, 0, 0);
    }

    private void ActivarGirar()
    {
        giro = true;
    }
}
