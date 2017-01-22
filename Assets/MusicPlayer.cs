using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	static AudioSource instance;

	void Awake () 
	{
		instance = GetComponent<AudioSource> ();
	}

	static AudioSource Instance
	{
		get
		{
			if (instance == null)
			{
				throw new System.NullReferenceException("There is no instance of music player in the scene");
			}

			return instance;
		}
	}

	public static void PauseMusic () 
	{
		Instance.Pause ();
	}

	public static void PlayMusic () 
	{
		Instance.Play ();
	}
}
