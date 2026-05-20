using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float remainingTime = 15;

    public bool timerActive;

    [SerializeField] Animator textAnimator;
    bool isPulsing = false;
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

            if (remainingTime <= 6)
            {
                if (!isPulsing)
                {
                    timer.color = warningColor;
                    textAnimator.SetTrigger("Pulse");
                    isPulsing = true;
                }
            }
        }    

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }   
}
