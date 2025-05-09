using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float Velocidad = 100f;
    public Transform Jugador;
    public float RotacionX = 0f;

    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void Update()
    {
        // Movimiento de la camara
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        // Rotacion de la camara y tambien el limite de la rotacion :>
        RotacionX -= MouseY;
        RotacionX = Mathf.Clamp(RotacionX, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(RotacionX, 0f, 0f);
        Jugador.Rotate(Vector3.up * MouseX);

        

    }
}
