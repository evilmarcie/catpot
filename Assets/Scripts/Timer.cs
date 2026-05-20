using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance; void Awake(){instance =  this;}

    public TextMeshProUGUI timer;
    public float remainingTime;

    public bool timerActive;

    [SerializeField] Animator textAnimator;
    public bool isPulsing = false;
    bool hasPlayedDing = false;
    Color32 warningColor = new Color32(167, 52, 82, 255);

    void Update()
    {
        if (timerActive == true)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                // Ensure game hasn't already been won
                if (!GameManager.instance.WinScreen.activeSelf)
                {
                    GameManager.instance.GameOver();
                }
            }

            if (remainingTime <= 1)
            {
                if(!hasPlayedDing)
                {
                    hasPlayedDing = true;
                    sfxManager.instance.PlaySFX(sfxManager.instance.bell, 0.8f);
                }
            }

            if (remainingTime <= 6)
            {
                if (!isPulsing)
                {
                    timer.color = warningColor;
                    textAnimator.SetTrigger("Pulse");
                    sfxManager.instance.PlaySFX(sfxManager.instance.clock_tick, 0.6f);
                    isPulsing = true;
                }
            }
        }    

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }

    public void DestroyTimer()
    {
        Destroy(gameObject);
    } 
}
