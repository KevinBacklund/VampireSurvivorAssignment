using UnityEngine;

public class RecoverUpgrade : StatUpgrade
{
    protected override void ApplyModifier()
    {
        player.CurrentRecovery *= 1 + statUpgradeData.Multiplier / 100f;
    }
}
