using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Util : MonoBehaviour
{
    [ExecuteInEditMode]
    void Update()
    {
        var obj = ScriptableObject.CreateInstance<CollideableTile>();
        AssetDatabase.CreateAsset(obj, "Assets/CollideableTile.asset");
        DestroyImmediate(gameObject);
    }
}
