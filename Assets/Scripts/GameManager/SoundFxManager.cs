using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
	public static SoundFxManager instance;

	[SerializeField] private AudioSource soundFxObject;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	public void PlaySoundFxClip(AudioClip audioClip, Transform spawnTransfrom, float volume)
	{
		//Spawnaa obejcti
		AudioSource audioSource = Instantiate(soundFxObject, spawnTransfrom.position, Quaternion.identity	);

		//anna audioclip
		audioSource.clip = audioClip;

		//anna volume
		audioSource.volume = volume;

		//soita ‰‰ni
		audioSource.Play();


		//‰‰ni clipin pituus
		float clipLength = audioSource.clip.length;

		Destroy(audioSource.gameObject, clipLength);
	}


	public void PlayRandomSoundFxClip(AudioClip[] audioClip, Transform spawnTransfrom, float volume)
	{
		//Otetaan random index
		int rand = Random.Range(0, audioClip.Length);

		//Spawnaa obejcti
		AudioSource audioSource = Instantiate(soundFxObject, spawnTransfrom.position, Quaternion.identity);

		//anna audioclip
		audioSource.clip = audioClip[rand];

		//anna volume
		audioSource.volume = volume;

		//soita ‰‰ni
		audioSource.Play();


		//‰‰ni clipin pituus
		float clipLength = audioSource.clip.length;

		Destroy(audioSource.gameObject, clipLength);
	}
}
