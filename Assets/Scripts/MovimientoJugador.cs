using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovimientoJugador : MonoBehaviour
{
    public CharacterController Controlador;
    public float Velocidad = 15f;
    public float Gravedad = -10;
    public float salto = 2f;
    public Transform EnElPiso;
    public float DistanciaDelPiso;
    public LayerMask MascaraDePiso;

    public Camera camaraPrimeraPersona;
    public Camera camaraTerceraPersona;

    Vector3 VelocidadAbajo;
    bool EstaEnElPiso;
    bool primeraPersonaActiva = true;

    void Start()
    {
        ActivarPrimeraPersona(); // Comienza con la cámara en primera persona
    }

    void Update()
    {
        // Movimiento
        EstaEnElPiso = Physics.CheckSphere(EnElPiso.position, DistanciaDelPiso, MascaraDePiso);
        if (EstaEnElPiso && VelocidadAbajo.y < 0)
        {
            VelocidadAbajo.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 mover = transform.right * x + transform.forward * z;
        Controlador.Move(mover * Velocidad * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && EstaEnElPiso)
        {
            VelocidadAbajo.y = Mathf.Sqrt(salto * -2f * Gravedad);
        }

        VelocidadAbajo.y += Gravedad * Time.deltaTime;
        Controlador.Move(VelocidadAbajo * Time.deltaTime);

        // Cambio de cámara con la tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            primeraPersonaActiva = !primeraPersonaActiva;

            if (primeraPersonaActiva)
                ActivarPrimeraPersona();
            else
                ActivarTerceraPersona();
        }
    }

    void ActivarPrimeraPersona()
    {
        camaraPrimeraPersona.enabled = true;
        camaraTerceraPersona.enabled = false;
    }

    void ActivarTerceraPersona()
    {
        camaraPrimeraPersona.enabled = false;
        camaraTerceraPersona.enabled = true;
    }
}
