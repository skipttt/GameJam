using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class WallShuffler : MonoBehaviour
{
    [Header("Referencias padres")]
    [SerializeField] private Transform padreObjetosAMover;     // Padre que contiene las paredes/árboles
    [SerializeField] private Transform padrePosicionesDestino; // Padre que contiene los puntos vacíos

    [SerializeField] private float intervaloCambio = 5f;

    private List<GameObject> objetosAMover = new List<GameObject>();
    private List<Transform> posicionesDestino = new List<Transform>();

    private void Start()
    {
        // Agregar automáticamente todos los hijos a las listas
        foreach (Transform hijo in padreObjetosAMover)
        {
            objetosAMover.Add(hijo.gameObject);
        }

        foreach (Transform hijo in padrePosicionesDestino)
        {
            posicionesDestino.Add(hijo);
        }

        StartCoroutine(CambiarPosicionesPeriodicamente());
    }

    IEnumerator CambiarPosicionesPeriodicamente()
    {
        while (true)
        {
            CambiarPosiciones();
            yield return new WaitForSeconds(intervaloCambio);
        }
    }

    void CambiarPosiciones()
    {
        List<Transform> destinosDisponibles = new List<Transform>(posicionesDestino);

        foreach (GameObject obj in objetosAMover)
        {
            if (destinosDisponibles.Count == 0) break;

            int randomIndex = Random.Range(0, destinosDisponibles.Count);
            Transform destino = destinosDisponibles[randomIndex];
            destinosDisponibles.RemoveAt(randomIndex);

            StartCoroutine(MoverConCollider(obj, destino.position));
        }
    }

    IEnumerator MoverConCollider(GameObject obj, Vector3 nuevaPosicion)
    {
        Collider col = obj.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        obj.transform.position = nuevaPosicion;

        yield return new WaitForSeconds(0.1f);

        if (col != null) col.enabled = true;
    }

}
