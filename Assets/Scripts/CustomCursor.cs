using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    RectTransform cursorTransform;

    Canvas canvas;
    RectTransform canvasRect;

    void Awake()
    {
        cursorTransform = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        var image = GetComponent<Image>();
        if (image != null) {image.raycastTarget = false;}
    }

    void OnEnable()
    {
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            canvasRect,
            mousePos,
            null,
            out Vector2 localPoint
        );

        cursorTransform.anchoredPosition = localPoint;
    }
}
