using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    private List<Sprite> spritePairs;

    int cardCount;

    public static CardsController instance; void Awake(){instance = this;}

    public void PrepareSprites(int round)
    {
        cardCount = 0;

        cardCount = 2 + (round * 2);

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

    [SerializeField] GameObject cardPrefab;
    [SerializeField] public Transform cardSpace; 

    public void CreateCards()
    {  
        RefreshGrid();

        for (int i = 0; i < spritePairs.Count; i++)
        {
            GameObject cardParent = Instantiate(cardPrefab, cardSpace);
            cardParent.GetComponent<CardSizeController>().ResizeCards();
            Card card = cardParent.GetComponentInChildren<Card>();
            card.SetSprite(spritePairs[i]);
            card.controller = this;
        }
    }

    void RefreshGrid()
    {
        FlexibleLayoutGroup grid = cardSpace.GetComponent<FlexibleLayoutGroup>(); 
        int round = GameManager.instance.round;


        if(cardCount % 4 == 0)
        {
            grid.fitType = FlexibleLayoutGroup.FitType.FIXEDCOLUMNS;
            grid.columns = 4;
        }
        else
        {
            grid.fitType = FlexibleLayoutGroup.FitType.FIXEDROWS;
            grid.rows = 2; 
        }

        // if (round == 1)
        // {
        //     grid.fitType = FlexibleLayoutGroup.FitType.FIXEDROWS;
        //     grid.rows = 1; 
        // }
        // if (round == 2 || round == 5 || round == 7)
        // {
        //     grid.columns = 2;   
        // }
        // if (round == 3)
        // {
        //     grid.columns = 4;
        // }
        // if (round == 4)
        // {
        //     grid.columns = 5;
        // }
        // if (round == 6)
        // {
        //     grid.columns = 7;
        // }


    }

    Card firstSelected;
    Card secondSelected;

    public void SetSelected(Card card)
    {
        if (card.isSelected == false)
        {

            sfxManager.instance.PlaySFX(sfxManager.instance.popclick_sfx, 0.5f);

            card.TriggerFlip();

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
            sfxManager.instance.PlaySFX(sfxManager.instance.match_sfx, 0.5f);
            catExpressions.SetTrigger("Happy");
            matchCount++;
            if (matchCount >= cardCount / 2)
            {
                GameManager.instance.RoundComplete();
            }
            
        }
        else
        {
            sfxManager.instance.PlaySFX(sfxManager.instance.incorrectMatch_sfx, 0.5f);
            catExpressions.SetTrigger("Sad");
            a.TriggerFlip();
            b.TriggerFlip();
        }

    }

    public void ClearCards()
    {
        if(cardSpace.childCount > 0)
        {
            foreach(Transform child in cardSpace)
            {
                Destroy(child.gameObject);
            }
        }
        
    }
}
