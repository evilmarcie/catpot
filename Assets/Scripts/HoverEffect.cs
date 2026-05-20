using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scale = 1f;

    private Vector2 originalSize;
    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        originalSize = rt.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.localScale = originalSize * scale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rt.localScale = originalSize;
    }
}
