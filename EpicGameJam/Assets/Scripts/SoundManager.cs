using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioSource musicSource;
	public AudioSource efxSource;
	public AudioSource bulletEfx;
	public AudioSource bulletEffectsEfx;
	public AudioSource explosionEfx;
	public AudioSource enemyEfx;
	public AudioSource playerEfx;

	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingle (AudioClip clip) {
		efxSource.clip = clip;
		efxSource.Play ();
	}

	public void PlaySingleBullet (AudioClip clip) {
		bulletEfx.clip = clip;
		bulletEfx.Play ();
	}

	public void PlayBulletEffect (AudioClip clip) {
		bulletEffectsEfx.clip = clip;
		bulletEffectsEfx.Play ();
	}

	public void PlayExplosion (params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		explosionEfx.pitch = randomPitch;
		explosionEfx.clip = clips [randomIndex];
		explosionEfx.Play ();
	}

	public void PlayEnemySound (params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		enemyEfx.pitch = randomPitch;
		enemyEfx.clip = clips [randomIndex];
		enemyEfx.Play ();
	}

	public void PlayPlayerSound (params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		/*playerEfx.pitch = randomPitch;
		playerEfx.clip = clips [randomIndex];
		playerEfx.Play ();*/
	}

	public void RandomizeSfx (params AudioClip[] clips) {
		int randomIndex = Random.Range (0, clips.Length);
		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efxSource.pitch = randomPitch;
		efxSource.clip = clips [randomIndex];
		efxSource.Play ();
	}
}
