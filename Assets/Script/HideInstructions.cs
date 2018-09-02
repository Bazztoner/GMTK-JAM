using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HideInstructions : MonoBehaviour
{
    public GameObject inst;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            inst.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
