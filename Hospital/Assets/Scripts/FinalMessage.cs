using UnityEngine;
using System.Collections;


/*This script show a final message when you are outside the hospital and you finish the game.*/
public class FinalMessage : MonoBehaviour {

	bool enter = false;
	string message = "Congratulations! You proved that you are a doctor! You are free now.";

	void OnTriggerEnter(Collider otherCollider)
	{
		
		enter = true;
	}
	
	
	void OnGUI() {
		
		if (enter) {
			GUI.skin.GetStyle ("label").fontSize = 30;
			GUI.backgroundColor = Color.blue;
			GUI.Label (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2 + 200), message);
	
		}
		
		
		
	}
	
	void OnTriggerExit(Collider otherCollider){
		
		Destroy (gameObject);
	}
}
