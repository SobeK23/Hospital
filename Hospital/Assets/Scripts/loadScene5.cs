using UnityEngine;
using System.Collections;

public class loadScene5 : MonoBehaviour {

	void OnTriggerEnter(Collider otherCollider)
	{

		Application.LoadLevel ("Hospital5");
	}
}
