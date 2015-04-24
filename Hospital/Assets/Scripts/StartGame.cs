using UnityEngine;
using System.Collections;


/*Menu with options: start game or quit it.*/
public class StartGame : MonoBehaviour {


	int groupWidth =  Screen.width/2;
	int groupHeight = Screen.height / 2;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		
		int groupX = Screen.width / 4;
		int groupY = Screen.height/4;
		
		GUI.BeginGroup( new Rect( groupX, groupY, groupWidth, groupHeight ) );
		//GUI.Box( new Rect( 0, 0, groupWidth, groupHeight ), "Level Select" );
		
		if ( GUI.Button( new Rect( 10, 0, groupWidth-100, groupHeight/2 -20 ), "Start Game" ) )
		{
			Application.LoadLevel("Hospital1");
		}
		if ( GUI.Button( new Rect( 10, groupHeight/2, groupWidth-100, groupHeight/2 -20), "Exit" ) )
		{
			Application.Quit();
		}

		
		GUI.EndGroup();
	}


}
