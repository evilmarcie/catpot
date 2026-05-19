using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    private List<Sprite> spritePairs;

    public static CardsController instance;

    void Awake()
    {
        instance = this;
    }

    public void PrepareSprites(int round)
    {
        int cardCount = 0;

        if (round == 1)
        {
            cardCount = 4;
        }
        else if (round == 2)
        {
            cardCount = 6;
        }
        else if (round == 3)
        {
            cardCount = 8;
        }

        if (spritePairs != null){spritePairs.Clear();}
        spritePairs = new List<Sprite>();
        for (int i = 0; i < cardCount/2; i++)
        {
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        ShuffleSprites(spritePairs);
    }

    void ShuffleSprites(List<Sprite> spriteList)
    {
        for (int i = spriteList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Sprite temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
            spriteList[randomIndex] = temp;
        }
    }

    [SerializeField] Card cardPrefab;
    [SerializeField] public Transform cardSpace; 

    public void CreateCards()
    {  
        for(int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, cardSpace);
            card.SetSprite(spritePairs[i]);
            card.controller = this;
        }
    }

    void Update()
    {
        GridLayoutGroup grid = cardSpace.GetComponent<GridLayoutGroup>(); 
        if (cardSpace.childCount == 6)
        {
            grid.constraintCount = 3;
        }
        else
        {
            grid.constraintCount = 4;
        }
    }

    Card firstSelected;
    Card secondSelected;

    public void SetSelected(Card card)
    {
        if (card.isSelected == false)
        {
            card.TriggerFlip();
            //card.ShowCard();

            if (firstSelected == null)
            {
                firstSelected = card;
                return;
            }

            if (secondSelected == null)
            {
                secondSelected = card;
                StartCoroutine(CheckMatch(firstSelected, secondSelected));
                firstSelected = null;
                secondSelected = null;
            }
        }
    }

    public int matchCount = 0;
    [SerializeField] Animator catExpressions;

    IEnumerator CheckMatch(Card a, Card b)
    {
        yield return new WaitForSeconds(0.3f);
        if(a.cardFront == b.cardFront)
        {
            catExpressions.SetTrigger("Happy");
            matchCount++;
            if (matchCount >= spritePairs.Count / 2)
            {
                foreach(Transform child in cardSpace)
                {
                    Destroy(child.gameObject);
                }
                
                GameManager.instance.RoundComplete();
            }
            
        }
        else
        {
            catExpressions.SetTrigger("Sad");
            a.TriggerFlip();
            b.TriggerFlip();
            //a.HideCard();
            //b.HideCard();
        }

    }
}
