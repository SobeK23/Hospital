using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

	
/*This script it the inventory script reduced*/
public class InteractionScene5 : MonoBehaviour {
		
		
		public bool lockMouseLook=false;
		
		public bool introduceScores=false;
		
		
		GameObject player;
		GameObject head;
		Camera mainCamera;
		public string seenObject = "";
		
		Dictionary<string, int> backupInventory;
		
		int toolSizeX = 6;
		int toolSizeY = 4;
		
		int buttonSize = 80;
		int spacing = 3;	  
		
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
		
		
		
		void Selection()
		{  
			
			if (Screen.lockCursor){ 
				
				Ray ray = mainCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.0f));
				RaycastHit hit;
				if ((Physics.Raycast (ray, out hit)) && (hit.transform.tag == "Seleccionable")) //If the object is seleccionable (tag)
				{
					seenObject = hit.transform.name;                                            //you save the name
					
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
			
			if (seenObject.Equals ("patient_female" )) {
				Application.LoadLevel("Find diagnosis");
				
			} 
			
			seenObject = "";
			
		}
		

		void Update () {
			
			Screen.lockCursor = true;	
			Selection ();
			
		}
		
		
		void OnGUI()
		{		
			
			GUI.skin.box.fontSize = 50;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
			GUI.skin.box.wordWrap = false;
			
			GUI.skin.button.stretchWidth = false;
			GUI.skin.button.fontSize = 50;
			
			
			if (seenObject != "") 
			{
				Rect labelSize = GUILayoutUtility.GetRect (new GUIContent (seenObject), "box");
				labelSize.center = new Vector2 (Screen.width / 2f, Screen.height / 2 * 0.8f);
				GUI.Label (labelSize, seenObject, "box");
			}
			
			
		}
		
		
		
	}
	
	

