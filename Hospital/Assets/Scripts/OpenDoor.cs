using UnityEngine;
using System.Collections;

/*Script for open doors that have this.*/
public class OpenDoor : MonoBehaviour {
	void OnTriggerStay(Collider otherCollider)
	{
		if (Input.GetKeyDown(KeyCode.E)) animation.Play ();
	}
	
}
