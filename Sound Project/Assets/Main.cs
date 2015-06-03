using UnityEngine;
using System.Collections;

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

	// Update is called once per frame
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

				Debug.Log("Hit point: " + hit.point);
			}
		}
	}
}