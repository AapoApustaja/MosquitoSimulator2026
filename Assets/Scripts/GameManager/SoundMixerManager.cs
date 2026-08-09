using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
	[SerializeField] private AudioMixer audioMixer;

	public void SetMasterVolume(float volume)
	{
		audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20f);

	}

	public void SetSoundFxVolume(float volume)
	{
		audioMixer.SetFloat("SoundFxVolume", Mathf.Log10(volume) * 20f);

	}

	public void SetMusicVolume(float volume)
	{
		audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20f);
	}
}
