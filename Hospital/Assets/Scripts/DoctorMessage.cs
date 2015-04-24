using UnityEngine;
using System.Collections;


/*This script show a message and play an audio when you approach to the doctor*/
public class DoctorMessage : MonoBehaviour {

	string cong = "Congratulations!";
	string message = "You completed two challenges. Now go downstairs, find the computer room and continue the rest of the challenges.";
	bool inside = false;
	private AudioSource source;
	//public AudioClip speech;

	void Start(){

		source = GetComponent<AudioSource> ();

	}

	void OnTriggerEnter(Collider otherCollider){

		source.Play();

	}

	void OnTriggerStay(Collider otherCollider)
	{
		inside = true;

	}

	void OnTriggerExit(Collider otherCollider){

		inside = false;
	}

	void OnGUI(){

		if (inside) {
			GUI.skin.GetStyle("label").fontSize = 50;
			GUI.Label (new Rect (Screen.width /2-200, 100, Screen.width /2, 100), cong);

			GUI.skin.GetStyle("label").fontSize = 35;
			GUI.Label (new Rect (Screen.width /4, 300, Screen.width /2, 400), message);

		}

	}



}
