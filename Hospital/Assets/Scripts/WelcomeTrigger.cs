using UnityEngine;
using System.Collections;

/*First message showed, explaining your situation in the hospital*/
public class WelcomeTrigger : MonoBehaviour {

	bool enter = false;
	string context = "Context:";
	string objective = "Objective 1:";
	string message = "Context: \nYou were taken to the hospital for unknown reasons and you have been diagnosed with amnesia.\n\nTo prove that this is not the case , you should show to the doctors that you still have your mental abilities. \n\nThe doctors developed various cognitive exercises that you have to solve as you will move througt the differents rooms the hospital. \n\n" ;
	string message2 = "Objective1: \nThe first step is to discover the door's combination in order to get out of this room where you are trapped. \n\n";


	void OnTriggerEnter(Collider otherCollider)
	{

		enter = true;
	}
	

	void OnGUI() {
		
		if (enter) {
			GUI.skin.GetStyle ("label").fontSize = 30;
			GUI.backgroundColor = Color.blue;
			GUI.Label (new Rect (Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2 + 200), message + message2);
			if (Input.anyKeyDown) {

				message ="\n\n\n\nPress E to open doors and left click to interact with the objects";
				message2= "";

			}
		}



	}

	void OnTriggerExit(Collider otherCollider){

		Destroy (gameObject);
	}

	IEnumerator wait5seconds(){
		yield return new WaitForSeconds(5);
	}
	
}

