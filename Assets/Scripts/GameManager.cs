using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int round;

    [SerializeField] GameObject jackpotPopup;

    public static GameManager instance;

    public Sprite[] AdoptionFundSprites;

    [SerializeField] Image adoptionFund;
    int fundSpriteInt = 0;

    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject HomeMenu;

    [SerializeField] GameObject timer;

    void Awake()
    {
        instance = this;
        HomeMenu.SetActive(true);
        Setup();
        round = 1;
    }

    void Setup()
    {
        jackpotPopup.SetActive(false);
        GameScreen.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        timer.SetActive(false);
        intro.SetActive(false);
        tutorial.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartIntro()
    {
        HomeMenu.SetActive(false);
        intro.SetActive(true);
        ProgressIntro();
    }

    public void StartGame()
    {

        GameScreen.SetActive(true);
        tutorial.SetActive(false);
        RoundOne();
    }

    void RoundOne()
    {
        adoptionFund.sprite = AdoptionFundSprites[fundSpriteInt];
        CardsController.instance.PrepareSprites(round);
        CardsController.instance.CreateCards();
        timer.SetActive(true);
    }

    public void RoundComplete()
    {
        StartCoroutine(BeatRound());
    }

    IEnumerator BeatRound()
    {
        fundSpriteInt ++;
        if (fundSpriteInt < AdoptionFundSprites.Length)
        {
            adoptionFund.sprite = AdoptionFundSprites[fundSpriteInt];   
        }

        jackpotPopup.SetActive(true);
        
        CardsController.instance.matchCount = 0;
        round++;

        if (round <= 3)
        {
            CardsController.instance.PrepareSprites(round);
            CardsController.instance.CreateCards();
            timer.GetComponent<Timer>().remainingTime = 20;
        
        }
        else
        {
            StartCoroutine(EndGame());
            yield break;
        }

        yield return new WaitForSeconds(1);
        jackpotPopup.SetActive(false);

        yield return null;
    }

    [SerializeField] GameObject WinScreen;

    IEnumerator EndGame()
    {
        jackpotPopup.SetActive(true);
        yield return new WaitForSeconds(2);

        WinScreen.SetActive(true);
        jackpotPopup.SetActive(false);
        timer.SetActive(false);

        yield return null;
    }

    [SerializeField] GameObject LoseScreen;

    public void GameOver()
    {
        LoseScreen.SetActive(true);
        timer.SetActive(false);
    }

    [SerializeField] GameObject intro;
    [SerializeField] Sprite[] introSprites;
    [SerializeField] Image introComic;
    int currentSlide = 0;
    [SerializeField] GameObject tutorial;

    public void ProgressIntro()
    {
        if (currentSlide < introSprites.Length)
        {
            introComic.sprite = introSprites[currentSlide];
            currentSlide++;
        }
        else
        {
            tutorial.SetActive(true);
        }
    }
}
