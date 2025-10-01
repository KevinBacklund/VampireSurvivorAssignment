using UnityEngine;


public enum SoundType
{
    enemyDamaged,
    levelUp,
    playerDeath,
    enemyDeath,
    playerAttack,
    enemyAttack,
    expGain,
    playerDamaged,
}


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] soundlist;

    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void Playsound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundlist[(int)sound], volume);
    }

}
