using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LevelSpawner : MonoBehaviour
{
    public static LevelSpawner instance;

    [Serializable]
    public struct LevelData
    {
        public string modulePathPrefix;
        public string[] ModuleDifficultyPaths;
    }

    [SerializeField] public LevelData levelData;
    public float levelUnit = 32f;
    public AnimationCurve difficultyCurve;

    Dictionary<int, LevelModule[]> _diffModules = new Dictionary<int, LevelModule[]>();

    void Awake()
    {
        if (instance) Destroy(this);
        else instance = this;
    }

    void Start()
    {
        for (int i = 0; i < levelData.ModuleDifficultyPaths.Length; i++)
        {
            _diffModules[i] = LoadDifficultyMode(i);
        }
    }

    LevelModule[] LoadDifficultyMode(int index)
    {
        var path = levelData.modulePathPrefix + levelData.ModuleDifficultyPaths[index];
        return Resources.LoadAll(path).Select(x => x as GameObject)
                                      .Select(x => x.GetComponent<LevelModule>())
                                      .Where(x => x != null)
                                      .ToArray();
    }

    //todo checkear la curva de dificultad y mandarle un int para que elija el coso
    public void SpawnNext(int index, float xCoord)
    {
        var list = _diffModules[index];
        int rnd = UnityEngine.Random.Range(0, list.Length);
        var obj = GameObject.Instantiate(list[rnd], Vector3.zero, Quaternion.identity, transform);
        obj.transform.localPosition = new Vector3(xCoord + levelUnit, 0, 0);
    }
}