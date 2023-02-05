using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    public static float volume;
    void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

    void Start () {
        PlayerPrefsManager.SetSFXVolume(1);
        volume = PlayerPrefsManager.GetSFXVolume();
	}
    private void Update() {
//        print(volume);
    }

    public void PlaySound(AudioClip audioClip){
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
    }
}
