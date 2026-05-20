using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scale = 1.5f;
    private Vector3 originalScale;

    public float rotate = 3f;
    private Quaternion originalRotation;

    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();

        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        originalScale = rt.localScale;
        originalRotation = rt.localRotation;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.localScale = originalScale * scale;
        rt.localRotation = Quaternion.Euler(0, 0, rotate);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.localScale = originalScale;
        rt.localRotation = originalRotation;
    }
}