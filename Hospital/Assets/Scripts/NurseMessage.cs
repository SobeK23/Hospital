using UnityEngine;
using System.Collections;

/*This script show a message and play an audio when you approach to the nurse*/
public class NurseMessage : MonoBehaviour {
	
	string message = "Hello! Go straight, you will find the computer room at the end of the corridor.";
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
			
			GUI.skin.GetStyle("label").fontSize = 35;
			GUI.Label (new Rect (Screen.width /4, 100, Screen.width /2, 400), message);
			
		}
		
	}
	

}
