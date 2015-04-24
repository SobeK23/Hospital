using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;


/*The most important script, interaction of the player with objects*/
public class Inventory : MonoBehaviour {

	public bool displayReport = false;
	bool tableOpen = false;
	private bool hasSaw=false;
	private bool openFirstComb=false;
	private bool openSecondComb=false;
	private int selected=0;
	private string[] buttons; 
	private string[] cbuttons;

	public bool firstDoorLocked= true;
	public bool secondDoorLocked = true;
	public bool lockMouseLook=false;

	public bool introduceScores=false;


	GameObject player;
	GameObject head;
	Camera mainCamera;
	public string seenObject = "";
	
	public bool journalShown;
	public string journal = "";
	public Vector2 scrollPosition;
	GUIStyle journalStyle;
	GUIStyle reportStyle;
	GUIStyle buttonRedStyle;
	GUIStyle buttonYellowStyle;
	GUIStyle buttonBlueStyle;
	GUIStyle buttonsSubmitStyle;

	public Texture2D red;
	public Texture2D yellow;
	public Texture2D blue;
	public Texture2D redFocus;
	public Texture2D yellowFocus;
	public Texture2D blueFocus;

	int offsetX = 10;
	int offsetY = 10;

	public Dictionary<string, int> inventory = new Dictionary<string, int>();
	string[] inventoryArray;

	CharacterMotor cmotor;
	
	int firstScore;
	int secondScore;
	int thirdScore;

	bool nextTest = false;
	bool firstPc = true;
	bool secondPc = false;
	bool thirdPc = false;

	bool showMessageChain=false;

	void Start () {

		/*player = GameObject.Find("Player");
		head = GameObject.Find ("head");
		mainCamera = head.GetComponent<Camera>(); */

		mainCamera = Camera.allCameras[1];


		//this.transform.localPosition = GetComponent<relevantInformation> ().position.localPosition;
	
		Screen.lockCursor = true;
		//DontDestroyOnLoad(transform.gameObject);
	    
		buttons = new string[4]{"0","0","0","0"};
		cbuttons = new string[3]{"0","0","0"};
		
		journalShown = false;
		journalStyle = new GUIStyle ();
		journalStyle.fontSize = 22;
		journalStyle.wordWrap = true;
		journalStyle.normal.textColor = Color.white;
		cmotor = GetComponent<CharacterMotor> (); //This component moves the player, we will use to block the player in determinated situations.

		//We set up all the styles
		reportStyle = new GUIStyle ();
		reportStyle.fontSize = 30;
		reportStyle.normal.background = Texture2D.blackTexture;
		reportStyle.normal.textColor = Color.white;

		buttonRedStyle = new GUIStyle ();
		buttonRedStyle.fontSize = 50;
		buttonRedStyle.alignment = TextAnchor.MiddleCenter;
		buttonRedStyle.focused.background = redFocus;
		buttonRedStyle.normal.background= red;

		buttonYellowStyle = new GUIStyle ();
		buttonYellowStyle.fontSize = 50;
		buttonYellowStyle.alignment = TextAnchor.MiddleCenter;
		buttonYellowStyle.focused.background = yellowFocus;
		buttonYellowStyle.normal.background= yellow;

		buttonBlueStyle = new GUIStyle ();
		buttonBlueStyle.fontSize = 50;
		buttonBlueStyle.alignment = TextAnchor.MiddleCenter;
		buttonBlueStyle.focused.background = blueFocus;
		buttonBlueStyle.normal.background= blue;

		buttonsSubmitStyle = new GUIStyle ();
		buttonsSubmitStyle.fontSize = 40;
		buttonsSubmitStyle.alignment = TextAnchor.MiddleCenter;
		buttonsSubmitStyle.normal.textColor = Color.white;
			
	}
	
	void OnLevelWasLoaded()
	{
		mainCamera = Camera.main;
	}
	
	void showJournal()
	{
		GUILayout.BeginArea (new Rect (60, 5, 1400, 200));
		scrollPosition = GUILayout.BeginScrollView (scrollPosition, GUILayout.Width(1400), GUILayout.Height(200));
		GUILayout.Label (journal,journalStyle);
		GUILayout.EndScrollView ();
		GUILayout.EndArea ();
	}

	/*Adds the object to the inventory depending on the selected object */	
	void InventoryFill(string name)

	{

		//We check if the character has taken before a part of the chain, for complete the saw if necessary
		if (name.Equals ("saw1") && inventory.ContainsKey ("saw2")) {
			hasSaw = true;
			inventory.Add ("saw", 1);
			inventory.Remove ("saw1");
		
		} else if (name.Equals ("saw2") && inventory.ContainsKey ("saw1")) {
			hasSaw = true;
			inventory.Add ("saw", 1);
			inventory.Remove ("saw2");
		
		} else { //If not, we add just the object to the dictionary.
			     if (inventory.ContainsKey (name)) {
				   inventory [name] ++;
			     } else {
			       inventory.Add (name, 1); 		
			     }

			     Debug.Log ("add object " + name);
		 }
		}

	/* Finds the object under the cursor (at the center of the screen)*/
	void Selection()


	{  

		if (Screen.lockCursor){ 

			Ray ray = mainCamera.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0.0f));
			RaycastHit hit;
			if ((Physics.Raycast (ray, out hit)) && (hit.transform.tag == "Seleccionable")) //If the object is seleccionable (tag)
			{
				seenObject = hit.transform.name;                                            //you save the name of the object 

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



	/*The interaction with the "seleccionable" objects if you click on them*/
	void objectInteraction(RaycastHit hit, String seenObject){

		if (seenObject.Equals ("monitor1") && firstPc) { //If is the first time you click on the first computer
			Application.OpenURL ("https://www.lumosity.com/app/v5/personalization/account/new");
			introduceScores = true;
			secondPc = true;
			firstPc = false;

		} else if (seenObject.Equals ("monitor2") && secondPc) { //If you click, after the first computer.
			Application.LoadLevel ("LogicalTests");
		}
		  else if (seenObject.Equals ("monitor2") && !secondPc) { //You don't click on the first computer before.
				//You have to use the first computer
			    //We check that for not add this item to the inventory.	

		} else if (seenObject.Equals ("monitor1") && !firstPc) { //If you click again, after complete the first challenge
				//You have to use the second computer


		}  else if (seenObject.Equals ("sheet")) { //The sheet with the medical report
			displayReport = true;

		} else if (seenObject.Equals ("chain")) {  

			if (hasSaw) { //if you have both pieces of the saw
				//Break chain and open cabinet
				GameObject chain = GameObject.Find("chain");
				chain.rigidbody.useGravity = true;
				chain.rigidbody.isKinematic = false;
				GameObject doorBlocked = GameObject.Find ("door_cabinet_blocked");
				doorBlocked.animation.Play ();
			}
			else {
				showMessageChain = true;  //If not, you show a message explaining you have to find the 2 parts of the saw
			}



		} else if (seenObject.Equals ("towel")) { //We throw down the towel fot show the number.
			GameObject towel = GameObject.Find ("towel");
			towel.rigidbody.useGravity = true;
			towel.rigidbody.isKinematic=false;



		} else if (seenObject.Equals ("FirstDoorLocked")) { //We display the boxes for the first combination
			openFirstComb=true;
		
		} else if (seenObject.Equals ("SecondDoorLocked")) {//We display the boxes for the second combination
			openSecondComb=true;
		
   
		}else{ //And the last one is take the object and add it to the inventory.
			Debug.Log("take object");
		        InventoryFill (seenObject);
		        Destroy (hit.transform.gameObject);
			            
		  }

		seenObject = "";

	}

	/*We show the hint, the buttons with numbers and their behaviour of the first door */
	void openFirstCombination(){
		//This method pops 4 button interactive for introduce the code to open the first door

		cmotor.setCanControl(false); //We lock the movement of the player

		lockMouseLook=true; //We lock the mouse look for interact with the buttons


		//HINT
		GUI.skin.GetStyle("label").fontSize = 35;

		GUI.Label (new Rect (Screen.width / 3,   Screen.height / 8, Screen.width / 3, Screen.height / 6), "To find the door's combination, you have to search for two hidden numbers in the room!");


		//INDICATIONS
		GUI.skin.GetStyle("label").fontSize = 60;

		GUI.Label (new Rect (3*Screen.width /12-20, Screen.height / 3, Screen.width /6, 200), "*");

		GUI.Label (new Rect (5*Screen.width /12-20, Screen.height / 3, Screen.width /6, 200), "+");

		GUI.Label (new Rect (7*Screen.width /12-20, Screen.height / 3, Screen.width /6, 200), "-");

		GUI.Label (new Rect (9*Screen.width /12-20, Screen.height / 3, Screen.width /6, 200), "/");


		//BUTTONS
		GUI.SetNextControlName ("0");
		if (GUI.Button (new Rect (Screen.width / 6, Screen.height / 2, Screen.width / 6 - 20, Screen.height / 6), buttons[0])) {

		}

		GUI.SetNextControlName ("1");
		if (GUI.Button (new Rect (2*Screen.width / 6, Screen.height / 2, Screen.width / 6 - 20, Screen.height / 6), buttons[1])) {
		
		}

		GUI.SetNextControlName ("2");
		if (GUI.Button (new Rect (3*Screen.width / 6, Screen.height / 2, Screen.width / 6 - 20, Screen.height / 6), buttons[2])) {

		}

		GUI.SetNextControlName ("3");
		if (GUI.Button (new Rect (4*Screen.width / 6, Screen.height / 2, Screen.width / 6 - 20, Screen.height / 6), buttons[3])) {

		}

		GUI.FocusControl (selected.ToString());


		//BEHAVIOUR
		GUI.skin.GetStyle("label").fontSize = 30;
		GUI.Label (new Rect (Screen.width / 3 +30, 6 * Screen.height / 8, Screen.width / 3, Screen.height / 4), "\u2190 \u2192 To move through boxes.\n  \u2191  \u2193  To increase/decrease the numbers in each box  \n\nPress Esc if you want cancel.");

		if (Input.GetKeyDown(KeyCode.Escape)) {

			openFirstComb=false;
			lockMouseLook=false;
			cmotor.setCanControl(true);
		}

	}

	/*We show the hint, the buttons with numbers and their behaviour of the second door */
	void openSecondCombination(){

		lockMouseLook=true; //We lock the mouse look for interact with the buttons

		cmotor.setCanControl(false); //We lock the movement of the player
		GUI.skin.GetStyle("label").fontSize = 35;

		//HINT
		GUI.Label (new Rect (Screen.width / 3,   Screen.height / 8, Screen.width / 3, Screen.height / 8), "Do you remember how many colored objects exist at this floor ?");

		//BUTTONS
		GUI.FocusControl (selected.ToString());
		GUI.SetNextControlName ("0");
		if (GUI.Button (new Rect (Screen.width / 5, Screen.height / 2, Screen.width / 5 - 20, Screen.height / 6), cbuttons[0], buttonRedStyle)) {

		}

		GUI.SetNextControlName ("1");
		if (GUI.Button (new Rect (2*Screen.width / 5, Screen.height / 2, Screen.width / 5 - 20, Screen.height / 6), cbuttons[1],buttonYellowStyle)) {

		}

		GUI.SetNextControlName ("2");
		if (GUI.Button (new Rect (3*Screen.width / 5, Screen.height / 2, Screen.width / 5 - 20, Screen.height / 6), cbuttons[2], buttonBlueStyle)) {
		
		}

		//BEHAVIOUR
		GUI.skin.GetStyle("label").fontSize = 30;
		GUI.Label (new Rect (Screen.width / 3, 6 * Screen.height / 8, Screen.width / 3, Screen.height / 4), "\u2190 \u2192 To move through boxes.\n  \u2191  \u2193  To increase/decrease the numbers in each box  \n\nPress Esc if you want cancel.");
		
		if (Input.GetKeyDown(KeyCode.Escape)) {
			
			openSecondComb=false;
			lockMouseLook=false;
			Screen.lockCursor = true;
			cmotor.setCanControl(true);

		}

		
	}

	/* Interface showed after click on the first computer, to introduce the scores of the website.*/
	void introduceScoresGUI(){
 

		GUI.Label (new Rect (Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 12), "Speed");
		GUI.Label (new Rect (Screen.width / 3, Screen.height / 3 + Screen.height / 12 + 20, Screen.width / 3, Screen.height / 12), "Logic");
		GUI.Label (new Rect (Screen.width / 3, Screen.height / 3 + 2 * Screen.height / 12 + 40, Screen.width / 3, Screen.height / 12), "Memory");
		                               
		string score1 = GUI.TextField (new Rect (2*Screen.width/3-100, Screen.height/3, Screen.width/12, Screen.height/12), firstScore.ToString(),buttonsSubmitStyle);
		string score2 = GUI.TextField (new Rect (2*Screen.width/3-100, Screen.height/3 + Screen.height/12 +20, Screen.width/12, Screen.height/12), secondScore.ToString(),buttonsSubmitStyle);
		string score3 = GUI.TextField (new Rect (2*Screen.width/3-100, Screen.height/3 + 2*Screen.height/12+40, Screen.width/12, Screen.height/12), thirdScore.ToString(),buttonsSubmitStyle);

		GUI.Label (new Rect (Screen.width/3 , 2*Screen.height/3, Screen.width/3, Screen.height/6),"To introduce the results, use TAB. Once done, click right and after click left outside the boxes.");

		firstScore = int.Parse (score1); //Parse string to int
		secondScore = int.Parse (score2);
		thirdScore = int.Parse (score3);

		
	}



	void Update () {


		//Pressing R you display the medical report
		if (Input.GetKeyDown(KeyCode.R)){
			
			if (displayReport) 
			      {displayReport=false;}
				else displayReport = true;
		}

		Screen.lockCursor = true;	
		Selection ();

		//If the combination of the first door is 8622, we open the door.
		if (buttons[0].Equals("8") & buttons[1].Equals("6") & buttons[2].Equals("2") & buttons[3].Equals("2") ) {
			firstDoorLocked = false;
			openFirstComb=false;
			lockMouseLook=false;
			buttons[0]="0";
			selected=0;
			Screen.lockCursor = true;
			cmotor.setCanControl(true);


		}

		//If the combination of the first door is 463, we open the door.
		if (cbuttons[0].Equals("4") & cbuttons[1].Equals("6") & cbuttons[2].Equals("3")) {
			secondDoorLocked = false;
			openSecondComb=false;
			lockMouseLook=false;
			cbuttons[0]="0";
			Screen.lockCursor = true;
			cmotor.setCanControl(true);
			
		} 


		//We use the arrows to move trough the boxes, we have 2 arrays, so we have to do the difference
		if(Input.GetKeyDown(KeyCode.LeftArrow) && openFirstComb){
			
			selected = menuSelection(buttons, selected, "left");
			
		}
		
		if(Input.GetKeyDown(KeyCode.RightArrow) && openFirstComb){
			
			selected = menuSelection(buttons, selected, "right");
			
		}

		if(Input.GetKeyDown(KeyCode.UpArrow) && openFirstComb){
			
			buttons[selected]=((int.Parse(buttons[selected])+1)%10).ToString();
		}

		if(Input.GetKeyDown(KeyCode.DownArrow) && openFirstComb){
			
			buttons[selected]=((int.Parse(buttons[selected])+9)%10).ToString();
		}


		//We use the arrows to move trough the boxes
		if(Input.GetKeyDown(KeyCode.LeftArrow) && openSecondComb){
			
			selected = menuSelection(cbuttons, selected, "left");
			
		}
		
		if(Input.GetKeyDown(KeyCode.RightArrow) && openSecondComb){
			
			selected = menuSelection(cbuttons, selected, "right");
			
		}
		
		if(Input.GetKeyDown(KeyCode.UpArrow) && openSecondComb){
			
			cbuttons[selected]=((int.Parse(cbuttons[selected])+1)%10).ToString();
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow) && openSecondComb){
			
			cbuttons[selected]=((int.Parse(cbuttons[selected])+9)%10).ToString();
		}


		//When the fields to introduce the score are showed, 
		if (Input.GetMouseButtonDown(1) && introduceScores) {
			introduceScores = false;
			//If the average is more than 70 (or the total 210) you don't need to continue the tests
			if (firstScore+secondScore+thirdScore>=210){
				
				//Finish all tests
				//finishTests=true;
				Application.LoadLevel("Hospital4");
				
			}
			//if not, you have to do the other 2
			else {
				nextTest = true;
				firstPc = false;
				secondPc = true;
			}

		}


	}


	/*Message showed when you get a average score under 70*/
	void nextTestGUI(){
		GUI.Label (new Rect(Screen.width/3, Screen.height/2, Screen.width/3, Screen.height/6),"You don't get a good score. You need to continue the rest of tests. Click on the second computer.");

		if (Input.anyKeyDown) {
			nextTest=false;
		}

	}


	/* We display a medical report pressing R, or the first time you click on this report*/
	void displayReportGUI()

	{


		GUILayout.BeginArea (new Rect(Screen.width/4,Screen.height/3.4f,Screen.width/2,500.0f), reportStyle);
		GUILayout.TextField ("Medical Report",reportStyle, GUILayout.MinWidth(200));
		GUILayout.TextArea ("Name: John\nSurname:Smith\nComments: Trauma in a car accident.\n",reportStyle);


		GUILayout.EndArea ();
	}



	/*Principal funcction we use for call others and display the information */
	void OnGUI()
	{		
	 
		GUI.skin.box.fontSize = 50;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		GUI.skin.box.wordWrap = false;
		
		GUI.skin.button.stretchWidth = false;
		GUI.skin.button.fontSize = 50;

		
	/*	if (seenObject != "") 
		{
			Rect labelSize = GUILayoutUtility.GetRect (new GUIContent (seenObject), "box");
			labelSize.center = new Vector2 (Screen.width / 2f, Screen.height / 2 * 0.8f);
			GUI.Label (labelSize, seenObject, "box");
		}*/
		if (displayReport) {
			displayReportGUI();


		}

		if (openFirstComb){
			seenObject="";
			openFirstCombination();

		}

		if (openSecondComb){
			seenObject="";
			openSecondCombination();
			
		}

		if (introduceScores) {

			introduceScoresGUI();

		}

		if (showMessageChain) {
			showMessageChainGUI();

			
		}

		if (nextTest) {

			nextTestGUI();

		}

		//The code is correct
		if (firstDoorLocked==false) {
			String success = "Success!";
			GUI.Label (new Rect (Screen.width /3, 100, Screen.width /3, 300), success);
			GameObject door1 = GameObject.Find("door1");

			door1.animation.wrapMode=WrapMode.Once; //We play animation and we open the door when the code is correct.
			door1.animation.Play("OpenDoorAnim");

			firstDoorLocked = true;

		}

		if (secondDoorLocked==false) {
			String success = "Success!";
			GUI.Label (new Rect (Screen.width /3, 100, Screen.width /3, 300), success);
			GameObject door2 = GameObject.Find("door2");

			door2.animation.wrapMode=WrapMode.Once;
			door2.animation.Play("OpenDoorAnim2");

			secondDoorLocked = true;
			
		}


	}
	/* Message showed when you click on the chain and you don't have take the 2 objects. */
	void showMessageChainGUI(){

		GUI.skin.box.fontSize = 40;
		GUI.Label (new Rect (5*Screen.width /12, Screen.height/4, Screen.width /3, 300), "You need to find the 2 parts of the saw to cut the chain.");
		if (Input.anyKeyDown) {
			showMessageChain=false;
		}
	}


	/* Function who gives you the index of the button to you move */
	int menuSelection (string[] buttonsArray, int selectedItem, string direction) {
		
		if (direction == "left") {
			
			if (selectedItem == 0) selectedItem =buttonsArray.Length-1;
			else selectedItem = (selectedItem- 1) % buttonsArray.Length;
			
		}
		
		if (direction == "right") {
			
			selectedItem = (selectedItem + 1) % buttonsArray.Length;
		}
		return selectedItem;
		
	}

}

