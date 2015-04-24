using UnityEngine;
using System.Collections;


/*Script that shows a message when you enter in the computer room*/
public class WelcomeScene2 : MonoBehaviour {

	bool enter = false;
	string message = "Look, we have to test your logic and memory now with some exercises. Please click on the first computer.";
	void OnTriggerEnter(Collider otherCollider)
	{
		
		enter = true;
	}
	
	void OnTriggerExit(Collider otherCollider)
	{
		
		enter = false;
	}

	void OnGUI() {
		
		if (enter) {
			GUI.skin.GetStyle("label").fontSize = 30;
			GUI.backgroundColor = Color.blue;
			GUI.Label (new Rect (Screen.width /4, Screen.height/4, Screen.width /2, Screen.height/2+200), message);
		
		}
		
		
	}

}
