using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager: MonoBehaviour
{
	static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<GameManager>();
				if(_instance == null)
				{
					_instance = new GameObject("GameManager Singleton").AddComponent<GameManager>().GetComponent<GameManager>();
				}
			}
			return _instance;
		}
	}

    public void Endgame()
    {

    }
}
