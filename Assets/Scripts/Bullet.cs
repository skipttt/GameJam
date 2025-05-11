using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;
    private Vector3 moveDirection;

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        Destroy(gameObject, lifeTime); // Autodestrucción de la bala
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Aquí haces daño al enemigo si tienes un sistema de vida
            // Ejemplo: other.GetComponent<EnemyHealth>()?.TakeDamage(10);

            Destroy(gameObject); // Destruye la bala al impactar
        }
    }
}
