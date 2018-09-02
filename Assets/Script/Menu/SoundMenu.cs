using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundMenu: MonoBehaviour
{
	static SoundMenu instance;
	public static SoundMenu Instance
	{
		get
		{
			if(instance == null)
			{
				instance = FindObjectOfType<SoundMenu>();
				if(instance == null)
				{
					instance = new GameObject("SoundMenu Singleton").AddComponent<SoundMenu>().GetComponent<SoundMenu>();
				}
			}
			return instance;
		}
	}

    public AudioSource audioSource;

    public AudioClip[] clips;
    public enum Audios { MENUBUTTON, COCONUT, DEATH };

    public void PlayClip(Audios aud)
    {
        audioSource.PlayOneShot(clips[(int)aud]);
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

