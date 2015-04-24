using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*This is the logical game*/
public class GameManagerLogicalTest : MonoBehaviour {
	
	public static GameManagerLogicalTest SP;

	int currentTestNumber = 1;
	string currentTest;
	string testinfo_filename;
	int lastTest = 4; //Number of test we have
	
	string question;
	string question2;
	string question3;
	string answer1;
	string answer2;
	string answer3;
	string answer4;
	string correctAnswer;
	string explication1;
	string explication2;
	string explication3;
	string endTest =  "Congratulations! \n You finished all tests. Your score is " ;

	
	Dictionary<string, string> testInfo; 
	string [] testText;
	int stringStart = 10;
	double scoreTotal = 0;
	double scoreCorrectAnswer = 1; //The score of each correct answer
	double score;

	
	GUIStyle buttonAnalysisStyle;
	GUIStyle buttonStyle1;
	GUIStyle buttonStyle2;
	GUIStyle buttonStyle3;
	GUIStyle buttonStyle4;
	GUIStyle wrongStyle;

	GUIStyle buttonReadyStyle;
	GUIStyle titleStyle;
	GUIStyle nextCaseStyle;
	
	
	public Texture2D buttonBackground;
	public Texture2D informationBackground;
	public Texture2D buttonReadyBackground;
	
	bool showNextCase = false;
	bool answered = false;
	bool  alreadyCalculated = false;
	bool wrong = false;

	/*We set up all the styles and load first case*/
	void Start(){

		titleStyle = new GUIStyle ();
		titleStyle.normal.textColor = Color.white;
		titleStyle.fontSize = 40;
		titleStyle.alignment = TextAnchor.UpperCenter;

		buttonAnalysisStyle = new GUIStyle ();
		buttonAnalysisStyle.fontSize = 30;
		buttonAnalysisStyle.alignment = TextAnchor.MiddleCenter;
		buttonAnalysisStyle.normal.background = buttonBackground;

		buttonReadyStyle = new GUIStyle ();
		buttonReadyStyle.fontSize = 30;
		buttonReadyStyle.alignment = TextAnchor.MiddleCenter;
		buttonReadyStyle.normal.background = buttonReadyBackground;
			
		nextCaseStyle = new GUIStyle ();
		nextCaseStyle.fontSize = 50;
		nextCaseStyle.normal.textColor = Color.white;
		nextCaseStyle.alignment = TextAnchor.MiddleCenter;
			
		buttonStyle1 = new GUIStyle ();
		buttonStyle1.fontSize = 35;
		buttonStyle1.alignment = TextAnchor.MiddleCenter;
		buttonStyle1.normal.background = buttonBackground;

		buttonStyle2 = new GUIStyle ();
		buttonStyle2.fontSize = 35;
		buttonStyle2.alignment = TextAnchor.MiddleCenter;
		buttonStyle2.normal.background = buttonBackground;

		buttonStyle3 = new GUIStyle ();
		buttonStyle3.fontSize = 35;
		buttonStyle3.alignment = TextAnchor.MiddleCenter;
		buttonStyle3.normal.background = buttonBackground;

		buttonStyle4 = new GUIStyle ();
		buttonStyle4.fontSize = 35;
		buttonStyle4.alignment = TextAnchor.MiddleCenter;
		buttonStyle4.normal.background = buttonBackground;

		buttonReadyStyle = new GUIStyle ();
		buttonReadyStyle.fontSize = 30;
		buttonReadyStyle.alignment = TextAnchor.MiddleCenter;
		buttonReadyStyle.normal.background = buttonReadyBackground;

		wrongStyle = new GUIStyle ();
		wrongStyle.fontSize = 50;
		wrongStyle.alignment = TextAnchor.MiddleCenter;
		wrongStyle.normal.textColor = Color.red;

		loadCase (1);
		
		
	}
	

	/*If we finished the tests, you calculate the score*/
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E)){
			Application.LoadLevel("Hospital1");
		}

		if (currentTestNumber == lastTest+1 && !alreadyCalculated){
			score = (scoreTotal / lastTest)*100 ;
			endTest = endTest + score + "%";
			alreadyCalculated = true;

			}
	}
	

	/*Load the case "curTest"*/
	void loadCase (int curTest){

		resetStyles ();
		answered = false;
		wrong = false;
		testInfo = new Dictionary<string, string>(); //Dictionary where we save the .xml information
		currentTestNumber = curTest;
		currentTest = "test"+currentTestNumber.ToString();
		testinfo_filename = @"Assets/Scripts/" + currentTest + ".xml";
		
		testText = File.ReadAllText (testinfo_filename).Split (new string[] {"</test>"}, System.StringSplitOptions.None);
		
		foreach (string info in testText) {
			int start = info.IndexOf("\"") + 1;
			int end = info.IndexOf("\"", start);
			string key = info.Substring(start, end - start);
			string value = info.Substring(end+2);
			testInfo.Add(key,value);
			
		}

		question = testInfo ["Question"];
		question2 = testInfo ["Question2"];
		question3 = testInfo ["Question3"];
		answer1 = testInfo ["Answer1"];
		answer2 = testInfo ["Answer2"];
		answer3 = testInfo ["Answer3"];
		answer4 = testInfo ["Answer4"];
		explication1 = testInfo ["Explication1"];
		explication2 = testInfo ["Explication2"];
		explication3 = testInfo ["Explication3"];
		correctAnswer = testInfo ["CorrectAnswer"];
		
		
	}

	
	

	
	void OnGUI(){
		
		
		if (currentTestNumber != lastTest+1) { //If is not the last one
				
		  	
			GUI.Label (new Rect (Screen.width / 4, 20, Screen.width / 2,Screen.height/4), question+"\n"+question2+"\n"+question3, titleStyle);
				
			//BUTTON LEFT UP	
			if (GUI.Button (new Rect (Screen.width / 24, Screen.height/3, Screen.width / 6, Screen.width / 8), answer1, buttonStyle1)) {
					
					if (answer1.Equals (correctAnswer)) { //If is correct you add the score of the answer
					scoreTotal = scoreTotal +  scoreCorrectAnswer;	 
					buttonStyle1.normal.textColor=Color.green;
						
					} 
				else{
					buttonStyle1.normal.textColor=Color.red;
					wrong= true;
				}
				answered=true;
		    }
				
			//BUTTON RIGHT UP
			if (GUI.Button (new Rect (7*Screen.width / 24, Screen.height/3, Screen.width / 6, Screen.width / 8), answer2, buttonStyle2)) {
				
				if (answer2.Equals (correctAnswer)) {
					scoreTotal = scoreTotal +scoreCorrectAnswer;
					buttonStyle2.normal.textColor=Color.green;
					
				} 
				else{
					buttonStyle2.normal.textColor=Color.red;
					wrong= true;
				}
				answered=true;
		}


			//BUTTON LEFT DOWN	
			if (GUI.Button (new Rect (13*Screen.width / 24, Screen.height/3, Screen.width / 6, Screen.width / 8), answer3, buttonStyle3)) {
				
				if (answer3.Equals (correctAnswer)) {
					scoreTotal = scoreTotal + scoreCorrectAnswer;	
					buttonStyle3.normal.textColor=Color.green;
					
				} 
				else{
					buttonStyle3.normal.textColor=Color.red;
					wrong= true;
				}
				answered=true;
		     }
		
	
	
			//BUTTON RIGHT DOWN
			if (GUI.Button (new Rect (19*Screen.width /24, Screen.height/3, Screen.width / 6, Screen.width / 8), answer4, buttonStyle4)) {
				
				if (answer4.Equals (correctAnswer)) {
					scoreTotal = scoreTotal +  scoreCorrectAnswer;	 
					buttonStyle4.normal.textColor=Color.green;
					
				} 
				else{
					buttonStyle4.normal.textColor=Color.red;
					wrong= true;

				}

				answered=true;
		
       	}
	
			if (answered){

				GUI.Label(new Rect (Screen.width / 4,5* Screen.height/6, Screen.width / 2,Screen.height/6), "Explanation: "+explication1 + "\n" + explication2+ "\n" +explication3, titleStyle);
				if (GUI.Button (new Rect (5 * Screen.width / 6, 5*Screen.height / 6 , Screen.width / 8, Screen.height / 8), "Next question", buttonReadyStyle)){

					
					currentTestNumber ++;
					if (currentTestNumber != lastTest+1) loadCase(currentTestNumber);

				}



			}

			if (wrong){
				GUI.Label (new Rect(Screen.width / 4,4* Screen.height/6, Screen.width / 2,Screen.height/6),"WRONG",wrongStyle);
			}

	
		}else { //If is the last test

			    
				GUI.Label (new Rect (Screen.width / 6, 2* Screen.height/3, 2 *Screen.width / 3, Screen.height / 3), endTest, nextCaseStyle); 
				
				
				if (GUI.Button (new Rect (5 * Screen.width / 6, 5*Screen.height / 6 , Screen.width / 8, Screen.height / 8), "Exit", buttonReadyStyle)){
					//We exit of the cases
					Application.LoadLevel("Hospital3");
				}
				
				
	
		}
		
	} 
	

	/*We reset the styles, in this way, each time you load a new case, the content of each button is black and not in colors*/
	void resetStyles(){
		
		buttonStyle1.normal.textColor = Color.black;
		buttonStyle2.normal.textColor = Color.black;
		buttonStyle3.normal.textColor = Color.black;
		buttonStyle4.normal.textColor = Color.black;
	
	}

	
}
