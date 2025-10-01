using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName ="ScriptableObjects/Character")]
public class CharacterSriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float dmgMultiplier;
    public float DmgMultiplier { get => dmgMultiplier; private set => dmgMultiplier = value; }  
}
