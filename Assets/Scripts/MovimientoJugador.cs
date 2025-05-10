using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public CharacterController Controlador;
    public float Velocidad = 15f;
    public float Gravedad = -10;
    public float salto = 2f;
    public Transform EnElPiso;
    public float DistanciaDelPiso = 0.4f;
    public LayerMask MascaraDePiso;

    public Camera camaraPrimeraPersona;
    public Camera camaraTerceraPersona;

    public Animator animator;

    Vector3 VelocidadAbajo;
    bool EstaEnElPiso;
    bool primeraPersonaActiva = true;

    void Start()
    {
        ActivarPrimeraPersona();
    }

    void Update()
    {
        Debug.Log("EstaEnElPiso: " + EstaEnElPiso);

        EstaEnElPiso = Physics.CheckSphere(EnElPiso.position, DistanciaDelPiso, MascaraDePiso);
        animator.SetBool("IsJumping", !EstaEnElPiso);

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

        // Calcula la velocidad directamente del input de movimiento
        float movementSpeed = new Vector2(x, z).magnitude;
        animator.SetFloat("Speed", movementSpeed);

        if (Input.GetKeyDown(KeyCode.C))
        {
            primeraPersonaActiva = !primeraPersonaActiva;

            if (primeraPersonaActiva)
                ActivarPrimeraPersona();
            else
                ActivarTerceraPersona();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
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
