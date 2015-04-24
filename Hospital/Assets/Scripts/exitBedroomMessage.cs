using UnityEngine;
using System.Collections;


/*This script show a message when you cross the first door*/
public class exitBedroomMessage : MonoBehaviour {

	private bool enter = false;
	private string message = "Now you get out of the room. \n\n\nObjective 2: The second step is to go downstairs by EXIT door.";


	void OnTriggerEnter(Collider otherCollider)
	{
		
		enter = true;
	}
	
	
	void OnGUI() {
		
		if (enter) {
			GUI.skin.GetStyle("label").fontSize = 40;
			GUI.backgroundColor = Color.blue;
			GUI.Label (new Rect (Screen.width /4, Screen.height/4, Screen.width /2, Screen.height/2+200), message);

		}
		
		
	}

	void OnTriggerExit(Collider otherCollider)
	{
		
		DestroyObject (this);
	}

}
