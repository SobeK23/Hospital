using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

/*Script added to the player, he can  just move and click on the third computer*/
public class InteractionScene3 : MonoBehaviour {
	

		public bool lockMouseLook=false;
		
		public bool introduceScores=false;
		
		
		GameObject player;
		GameObject head;
		Camera mainCamera;
		public string seenObject = "";
  

		int offsetX = 10;
		int offsetY = 10;
		
		CharacterMotor cmotor;


		void Start () {
			

			mainCamera = Camera.allCameras[1];
            Screen.lockCursor = true;
			cmotor = GetComponent<CharacterMotor> ();
			

		}
		
		void OnLevelWasLoaded()
		{
			mainCamera = Camera.main;
		}
		
	
		
		void Selection(){
			
			if (Screen.lockCursor){ 
				
				Ray ray = mainCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.0f));
				RaycastHit hit;
				if ((Physics.Raycast (ray, out hit)) && (hit.transform.tag == "Seleccionable")) //If the object is seleccionable (tag)
				{
					seenObject = hit.transform.name;                                            
					
					if ( Input.GetMouseButtonDown(0) && (hit.distance < 3.0f)) //if you are close and you click left
					{
						objectInteraction(hit, seenObject);
						
					}
				}
				else 
				{
					seenObject = "";
				}
			}
		}
		
		void objectInteraction(RaycastHit hit, String seenObject){
			
			if (seenObject.Equals ("monitor3" )) {
				Application.LoadLevel("MemoryGame");
				
			} 
			
			seenObject = "";
			
		}
		
		
		
		
		void Update () {
			
			Screen.lockCursor = true;	
			Selection ();
			
		}

		
		void OnGUI()
		{		
			


		}
		

		
	}
	

