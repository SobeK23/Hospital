using UnityEngine;
using System.Collections;

public class StartGame2 : MonoBehaviour {
	
	public Texture2D orbital;
	string text;
	
	// Use this for initialization
	void Start () {
		text = "You just waking up in the hospital and you don't know why you're there.";	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Confirm"))
		{
			Application.LoadLevel("hospital1");
		}
	}
	
	void OnGUI()
	{
		
		GUI.skin.box.fontSize = 30;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		GUI.skin.box.wordWrap = true;
		
		Rect labelSize = GUILayoutUtility.GetRect (new GUIContent (text), "box");
		labelSize.center = new Vector2 (Screen.width / 2f, Screen.height / 3f);
		GUI.Label (labelSize, text, "box");
		
		GUI.DrawTexture(new Rect(20,Screen.height-256-20,256,256), orbital);
	
		
	}		
}
