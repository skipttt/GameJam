using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    [SerializeField] private AnimationCurve curvaEscala;

    private void Start()
    {
        escalaOriginal = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = escalaOriginal;
    }
}
