﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelModule : MonoBehaviour
{
    void SpawnNext()
    {
        LevelSpawner.instance.SpawnNext(1, transform.localPosition.x);
    }
}
