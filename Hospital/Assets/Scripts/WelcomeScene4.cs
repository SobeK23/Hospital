using UnityEngine;
using System.Collections;


/*This script show you a message of the doctor in the medical office.*/
public class WelcomeScene4 : MonoBehaviour {
	
	bool enter = false;
	string message = " Congratulation! The tests have proved that you still have your cognitives skills. \n Now we need to check that you are actually a doctor. \n A specialist is waiting for you in a room with an open door.";
	void OnTriggerEnter(Collider otherCollider)
	{
		
		enter = true;
	}
	
	
	void OnGUI() {
		
		if (enter) {
			GUI.skin.GetStyle("label").fontSize = 30;
			GUI.backgroundColor = Color.blue;
			GUI.Label (new Rect (Screen.width /4, Screen.height/4, Screen.width /2, Screen.height/2+200), message);
			if (Input.anyKeyDown){
				
				Destroy (gameObject);
			}
		}
		
		
	}
	
}
