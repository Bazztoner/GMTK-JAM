using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyEvents
{
    //-----GNOLL------//

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// 1 - WeaponType (enum)
    /// </summary>
    public const string GnollWeaponStart = "GnollWeaponStart";

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// 1 - WeaponType (enum)
    /// </summary>
    public const string GnollWeaponEnd = "GnollWeaponEnd";

    /// <summary>
    /// 0 - Dier? (GameObject)
    /// </summary>
    public const string GnollDeathStart = "GnollDeathStart";

    /// <summary>
    /// 0 - Dier? (GameObject)
    /// </summary>
    public const string GnollDeathEnd = "GnollDeathEnd";

    //-----TROGLODYTE------//

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// 1 - WeaponType (enum)
    /// </summary>
    public const string TroglodyteWeaponStart = "TroglodyteWeaponStart";

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// 1 - WeaponType (enum)
    /// </summary>
    public const string TroglodyteWeaponEnd = "TroglodyteWeaponEnd";

    /// <summary>
    /// 0 - Dier? (GameObject)
    /// </summary>
    public const string TroglodyteDeathStart = "TroglodyteDeathStart";

    /// <summary>
    /// 0 - Dier? (GameObject)
    /// </summary>
    public const string TroglodyteDeathEnd = "TroglodyteDeathEnd";

    /// <summary>
    /// 0 - Attacker (GameObject)
    /// </summary>
    public const string TroglodyteSpawnProyectile = "TroglodyteSpawnProyectile";
}

public enum WeaponType
{
    Short,
    Long,
    Proyectile,
    COUNT
}
