using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image cardImage;
     [SerializeField] Animator cardAnimator;

    public Sprite cardBack;
    public Sprite cardFront;

    public bool isSelected;

    public CardsController controller;

    public void OnCardClick()
    {
        controller.SetSelected(this);
    }

    public void SetSprite(Sprite sprite)
    {
        cardFront = sprite;
    }

    public void ChangeSprite()
    {
        if (isSelected == true)
        {
            HideCard();
        }
        else
        {
            ShowCard();
        }
    }

    public void TriggerFlip()
    {
        cardAnimator.SetTrigger("Flip");
        sfxManager.instance.PlaySFX(sfxManager.instance.cardFlip_sfx, 1f);
    }

    public void ShowCard()
    {
        cardImage.sprite = cardFront;
        isSelected = true;
    }

    public void HideCard()
    {
        cardImage.sprite = cardBack;
        isSelected = false;
    }

    public void FadeCard()
    {
        // Image img = GetComponent<Image>();
        // Color colour = img.color;
        // colour.a = 0.8f;
        // img.color = colour;

        Button button = GetComponent<Button>();
        button.interactable = false;
    }

}
