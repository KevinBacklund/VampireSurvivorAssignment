using UnityEngine;


public enum SfxType
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

public enum Bgm
{
    gameplay,
    mainMenu,
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] sfxList, bgmList;

    private static SoundManager instance;
    private AudioSource[] audioSources;
    private AudioSource sfxSource, bgmSource;

    private void Awake()
    {
            instance = this;
            audioSources = GetComponents<AudioSource>();
            sfxSource = audioSources[0];
            bgmSource = audioSources[1];
    }

    public static void PlaySfx(SfxType sound, float volume = 1)
    {
        instance.sfxSource.PlayOneShot(instance.sfxList[(int)sound], volume);
    }

    public static void PlayBgm (Bgm music, float volume = 1)
    {
        if (instance.bgmSource.clip != instance.bgmList[(int)music]) 
        {
            instance.bgmSource.clip = instance.bgmList[(int)music];
            instance.bgmSource.volume = volume;
            instance.bgmSource.loop = true;
            instance.bgmSource.Play();
        }
    }
}
