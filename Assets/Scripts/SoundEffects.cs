using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public static SoundEffects instance;

    void Awake()
    {
        instance = this;
    }

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip cardFlip_sfx;

    public void CardFlipSFX()
    {
        audioSource.clip = cardFlip_sfx;
        audioSource.Play();
    }

    [SerializeField] AudioClip match_sfx;

    public void MatchSFX()
    {
        audioSource.clip = match_sfx;
        audioSource.Play();
    }

    [SerializeField] AudioClip incorrectMatch_sfx;

    public void IncorrectMatchSFX()
    {
        audioSource.clip = incorrectMatch_sfx;
        audioSource.Play();
    }

    [SerializeField] AudioClip jackpot_sfx;

    public void JackpotSFX()
    {
        audioSource.clip = jackpot_sfx;
        audioSource.Play();
    }
}
