using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModuleSpawnerHitbox : MonoBehaviour
{
    LevelModule _module;
    bool _used = false;

    void Start()
    {
        _module = GetComponentInParent<LevelModule>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!_used)
            {
                _module.SpawnNext();
                _used = true;
            }
        }
    }
}
