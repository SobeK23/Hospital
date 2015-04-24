using UnityEngine;
using System.Collections;

public class FootStepSound : MonoBehaviour {

	public AudioClip footstepSound;
	AudioSource audio;

	// Use this for initialization
	void Start () {
	
		audio = GetComponent<AudioSource> ();
		audio.clip = footstepSound;

	}
	
	// Update is called once per frame
	void Update () {


		if ((Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyDown(KeyCode.S)&& !Input.GetKeyDown(KeyCode.W))  ||
		    (Input.GetKeyDown(KeyCode.UpArrow)&& !Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyDown(KeyCode.DownArrow)&& !Input.GetKeyDown(KeyCode.UpArrow)))
		{

			audio.Play();
			
		}

		if ((Input.GetKeyUp(KeyCode.W) && !Input.GetKeyDown(KeyCode.S)) || (Input.GetKeyUp(KeyCode.S)&& !Input.GetKeyDown(KeyCode.W))  ||
		    (Input.GetKeyUp(KeyCode.UpArrow)&& !Input.GetKeyDown(KeyCode.DownArrow)) || (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKeyDown(KeyCode.UpArrow))
		    )
		{

			audio.Stop ();
			
		}

	
	
	}
}
