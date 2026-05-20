using UnityEngine;

public class CardSizeController : MonoBehaviour
{
    [SerializeField] float padding = 20;

    void Update()
    {
        ResizeCards();
    }

    public void ResizeCards()
    {
        RectTransform parentRT = GetComponent<RectTransform>();

        GameObject card = transform.GetChild(0).gameObject;
        RectTransform cardRT = card.GetComponent<RectTransform>();

        // original sprite/card ratio
        float aspect = cardRT.sizeDelta.x / cardRT.sizeDelta.y;

        // available space inside parent
        float maxWidth = parentRT.rect.width - padding;
        float maxHeight = parentRT.rect.height - padding;

        // fit by height first
        float height = maxHeight;
        float width = height * aspect;

        // if too wide, fit by width instead
        if (width > maxWidth)
        {
            width = maxWidth;
            height = width / aspect;
        }

        cardRT.sizeDelta = new Vector2(width, height);
    }
}
