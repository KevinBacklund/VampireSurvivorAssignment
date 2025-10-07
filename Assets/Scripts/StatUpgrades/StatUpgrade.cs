using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    protected PlayerStats player;
    public StatUpgradeScriptableObject statUpgradeData;

    protected virtual void ApplyModifier()
    {

    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerStats>();
        ApplyModifier();
    }

    void Update()
    {
        
    }
}
