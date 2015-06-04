using UnityEngine;

/// <summary>
/// Checks for sound emmision by pressing the 'E' key from the keyboard and raycasting it to find a collision from where to "spawn" it.
/// The after the first time the sound is fired the time offset is calculated, to calculate the direction of the soundwave.
/// </summary>
/// <author>Konstantinos Egkarchos</author>
/// <contact>konsnosl@gmail.com</contact>
/// <date>4/6/2015</date>
public class Main : MonoBehaviour 
{
	private static float soundTs = 0f;
	bool fired = false;

	void Awake()
	{
		Shader.SetGlobalFloat ("_SoundTimeOffset", 0f);
	}

	void updateSoundOffset()
	{
		if (fired) {
			float nowTS = Time.time;
			float sndOffset = nowTS - soundTs;
			Shader.SetGlobalFloat ("_SoundTimeOffset", sndOffset);
		}
	}

	void Update () {
		updateSoundOffset ();

		if (Input.GetKeyUp (KeyCode.E)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)){
				fired = true;
				Shader.SetGlobalVector("_SoundPos", hit.point);
				Shader.SetGlobalFloat ("_SoundTimeOffset", 0);
				soundTs = Time.time;
			}
		}
	}
}