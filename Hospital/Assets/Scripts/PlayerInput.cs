using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {


    public AudioClip canHitSound;

	void Update () {	
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100)) {

                Debug.DrawLine (ray.origin, hit.point);

             

            }
        }
	}
}
