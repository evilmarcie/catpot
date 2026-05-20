using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int round;
    public float roundTime;

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
        sfxManager.instance.PlaySFX(sfxManager.instance.click_sfx, 0.8f);
        ProgressIntro();
    }

    public void StartGame()
    {
        sfxManager.instance.PlaySFX(sfxManager.instance.click_sfx, 0.8f);
        GameScreen.SetActive(true);
        tutorial.SetActive(false);
        RoundOne();
    }

    void RoundOne()
    {
        adoptionFund.sprite = AdoptionFundSprites[fundSpriteInt];
        CardsController.instance.PrepareSprites(round);
        CardsController.instance.CreateCards();

        Timer.instance.remainingTime = 15;
        roundTime = 15;
        Timer.instance.timerActive = true;
    }

    public void RoundComplete()
    {
        StartCoroutine(BeatRound());
    }
    private int maxRounds = 7;
    [SerializeField] Transform gameCanvas;

    public void FundBar()
    {
        fundSpriteInt ++;
        if (fundSpriteInt < AdoptionFundSprites.Length)
        {
            adoptionFund.sprite = AdoptionFundSprites[fundSpriteInt];   
        }
    }

    [SerializeField] Animator fundBarAnim;

    IEnumerator BeatRound()
    {
        Timer.instance.timerActive = false;

        JackpotPopup();
        
        CardsController.instance.matchCount = 0;
        round++;

        if (round <= maxRounds)
        {
            CardsController.instance.ClearCards();
            CardsController.instance.PrepareSprites(round);
            CardsController.instance.CreateCards();
            
            Timer timerCont = Timer.instance;

            yield return new WaitForSecondsRealtime(1.5f);
            jackpotPopup.SetActive(false);
            fundBarAnim.SetTrigger("BarPopup");
            yield return new WaitForSecondsRealtime(1.5f);
            
            if(timerCont.isPulsing == true)
            {
                timerCont.DestroyTimer();
                GameObject newTimer = Instantiate(timer, gameCanvas);
                newTimer.transform.SetSiblingIndex(4);
            }

            do
            {
                new WaitForEndOfFrame();
            }
            while(Timer.instance == null);

            timerCont = Timer.instance;
            roundTime += 10;
            timerCont.remainingTime = roundTime;
            timerCont.timerActive = true;
        }
        else
        {
            StartCoroutine(EndGame());
            yield break;
        }
        yield return null;
    }

    [SerializeField] public GameObject WinScreen;

    IEnumerator EndGame()
    {
        JackpotPopup();
        yield return new WaitForSeconds(2);

        sfxManager.instance.PlaySFX(sfxManager.instance.yay_sfx, 0.8f);
        WinScreen.SetActive(true);
        jackpotPopup.SetActive(false);
        Timer.instance.gameObject.SetActive(false);

        yield return null;
    }

    [SerializeField] GameObject LoseScreen;

    public void GameOver()
    {
        sfxManager.instance.PlaySFX(sfxManager.instance.crying_sfx, 1f);
        LoseScreen.SetActive(true);
        Timer.instance.gameObject.SetActive(false);
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
            intro.SetActive(false);
            tutorial.SetActive(true);
        }

        if (currentSlide > 1)
        {
            sfxManager.instance.PlaySFX(sfxManager.instance.tutorialPageTurn_sfx, 0.9f);
        }
    }

    void JackpotPopup()
    {
        jackpotPopup.SetActive(true);
        sfxManager.instance.PlaySFX(sfxManager.instance.jackpot_sfx, 1f);
    }
}
