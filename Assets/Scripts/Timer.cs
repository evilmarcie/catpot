using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float remainingTime = 15;

    public bool timerActive;

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
                GameManager.instance.GameOver();
            }
        }    

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }   
}
