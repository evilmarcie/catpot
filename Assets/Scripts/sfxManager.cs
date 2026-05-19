using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager instance;

    [SerializeField] private AudioSource sfxPlayer;

    void Awake()
    {
        instance = this;
    }

    public void PlaySFX(AudioClip sfx, float volume)
    {
        AudioSource audioSource = Instantiate(sfxPlayer);
        audioSource.clip = sfx;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public AudioClip 
        cardFlip_sfx, 
        match_sfx, 
        incorrectMatch_sfx, 
        jackpot_sfx, 
        popclick_sfx, 
        click_sfx, 
        tutorialPageTurn_sfx, 
        yay_sfx, 
        crying_sfx;
}
