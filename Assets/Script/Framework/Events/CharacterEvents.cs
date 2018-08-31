using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEvents
{
    /// <summary>
    /// 0 - Target (Character)
    /// 1 - Buff
    /// </summary>
    public const string CharacterBuffed = "CharacterBuffed";
    /// <summary>
    /// 0 - Target (Character)
    /// 1 - Amount (int)
    /// </summary>
    public const string CharacterHealed = "CharacterHealed";
    /// <summary>
    /// 0 - Target (Character)
    /// 1 - Amount (int)
    /// </summary>
    public const string CharacterDamaged = "CharacterDamaged";

    /// <summary>
    /// 0 - Character (PlayerMovement)
    /// 1 - Where did it land? (GameObject)
    /// </summary>
    public const string CharacterLanded = "CharacterLanded";

    public const string CharacterFalling = "CharacterFalling";

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// </summary>
    public const string CharacterEndAttack = "CharacterEndAttack";

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// </summary>
    public const string CharacterStartAttack = "CharacterStartAttack";

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// </summary>
    public const string CharacterEndUsingSubweapon = "CharacterEndUsingSubweapon";

    /// <summary>
    /// 0 - HealAmount (int)
    /// </summary>
    public const string CharacterPickedHeal = "CharacterPickedHeal";

    /// <summary>
    /// 0 - ManaAmount (int)
    /// </summary>
    public const string CharacterPickedMana = "CharacterPickedMana";

    /// <summary>
    /// 0 - Subweapon
    /// 1 - ManaAmount (int)
    /// </summary>
    public const string CharacterPickedSubweapon = "CharacterPickedSubweapon";
    
}
