using UnityEngine;

public class ExpGem : MonoBehaviour, Icollectible
{
    public int expAmount;

    public void Collect()
    {
        PlayerStats player = FindAnyObjectByType<PlayerStats>();
        player.IncreaseExperience(expAmount);
        Destroy(gameObject);
    }   
}
