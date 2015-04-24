using UnityEngine;
using System.Collections;

/*Script for load the Scene2 when you enter in the trigger*/
public class loadScene2 : MonoBehaviour {


	GameObject player;

	void OnTriggerEnter(Collider otherCollider)
	{

		Application.LoadLevel ("Hospital2");
	}
	
}
