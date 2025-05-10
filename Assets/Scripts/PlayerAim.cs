using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Camera cam;
    public LayerMask aimLayerMask; 

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, aimLayerMask))
        {
            Debug.Log("Apuntando a: " + hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
}
