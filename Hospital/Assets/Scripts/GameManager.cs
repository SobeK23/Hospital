using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*This is the diagnosis game*/
public class GameManager : MonoBehaviour {

    public static GameManager SP;

	bool showDiagnosis;
	bool showTreatment;

	int currentCaseNumber = 1;
	string currentCase;
	string caseinfo_filename;
	int lastCase = 6; //How many cases we will load


	//All the information of the .xml will be save in this strigs.

	string symptom1;
	string symptom2;
	string symptom3;
	string symptom4;
	string symptom5;
	string symptom6;
	string symptoms;

	string analysis1;
	string analysis2;
	string analysis3;
	string analysis4;
	string analysis5;
	string analysis6;

	string analysis1result;
	string analysis2result;
	string analysis3result;
	string analysis4result;
	string analysis5result;
	string analysis6result;
	
	string diagnosis1;
	string diagnosis2;
	string diagnosis3;
	string diagnosis4;
	string diagnosis5;
	string diagnosis6;
	string diagnosisCorrect;
	int diagnosisErrors;

	string treatment1;
	string treatment2;
	string treatment3;
	string treatment4;
	string treatment5;
	string treatment6;
	string treatmentCorrect1;
	string treatmentCorrect2;
	string treatmentCorrect3;
	int numberTreatmentCorrect;
	int treatmentErrors;

	string informationPatient; 
	string antecedentPatient;

	string diagnosis = "Diagnosis";
	string analysis = "Analysis";
	string symptom = "Symptoms";
	string information = "Information";
	string antecedent = "Antecedent";
	string treatment ="Treatment";

	string nextCase = "Excellent! \n Let's go for the next medical case!";
	string endCase = "Congratulations! \n You finished all the cases.";

	Dictionary<string, string> caseInfo; 
	string [] caseText;
	int stringStart = 10;
	int correctAnswersTreatment=0;

	/*All the style we will use. we have one for each diagnosis, to made teh difference when you click righ or wrong*/

	GUIStyle buttonAnalysisStyle;
	GUIStyle buttonStyle1;
	GUIStyle buttonStyle2;
	GUIStyle buttonStyle3;
	GUIStyle buttonStyle4;
	GUIStyle buttonStyle5;
	GUIStyle buttonStyle6;
	GUIStyle buttonStyle7;
	GUIStyle buttonStyle8;
	GUIStyle buttonStyle9;
	GUIStyle buttonStyle10;
	GUIStyle buttonStyle11;
	GUIStyle buttonStyle12;
	GUIStyle informationStyle;
	GUIStyle buttonReadyStyle;
	GUIStyle symptomStyle;
	GUIStyle nextCaseStyle;
	GUIStyle titleStyle;
	

	public Texture2D buttonBackground;
	public Texture2D informationBackground;
	public Texture2D buttonReadyBackground;

	bool showNextCase = false;

	/*We set up all the styles and we load the first case*/
	void Start(){


		titleStyle = new GUIStyle ();
		titleStyle.normal.textColor = Color.white;
		titleStyle.fontSize = 30;

		informationStyle = new GUIStyle ();
		informationStyle.fontSize = 25;
		informationStyle.alignment = TextAnchor.UpperLeft;
		informationStyle.normal.background = informationBackground;

		buttonAnalysisStyle = new GUIStyle ();
		buttonAnalysisStyle.fontSize = 30;
		buttonAnalysisStyle.alignment = TextAnchor.MiddleCenter;
		buttonAnalysisStyle.normal.background = buttonBackground;

		symptomStyle = new GUIStyle ();
		symptomStyle.fontSize = 30;
		symptomStyle.alignment = TextAnchor.UpperCenter;
		symptomStyle.normal.background = informationBackground;


		buttonStyle1 = new GUIStyle ();
		buttonStyle1.fontSize = 30;
		buttonStyle1.alignment = TextAnchor.MiddleCenter;
		buttonStyle1.normal.background = buttonBackground;


		buttonStyle2 = new GUIStyle ();
		buttonStyle2.fontSize = 30;
		buttonStyle2.alignment = TextAnchor.MiddleCenter;
		buttonStyle2.normal.background = buttonBackground;


		buttonStyle3 = new GUIStyle ();
		buttonStyle3.fontSize = 30;
		buttonStyle3.alignment = TextAnchor.MiddleCenter;
		buttonStyle3.normal.background = buttonBackground;

		buttonStyle4 = new GUIStyle ();
		buttonStyle4.fontSize = 30;
		buttonStyle4.alignment = TextAnchor.MiddleCenter;
		buttonStyle4.normal.background = buttonBackground;

		buttonStyle5 = new GUIStyle ();
		buttonStyle5.fontSize = 30;
		buttonStyle5.alignment = TextAnchor.MiddleCenter;
		buttonStyle5.normal.background = buttonBackground;

		buttonStyle6 = new GUIStyle ();
		buttonStyle6.fontSize = 30;
		buttonStyle6.alignment = TextAnchor.MiddleCenter;
		buttonStyle6.normal.background = buttonBackground;

		buttonStyle7 = new GUIStyle ();
		buttonStyle7.fontSize = 30;
		buttonStyle7.alignment = TextAnchor.MiddleCenter;
		buttonStyle7.normal.background = buttonBackground;

		buttonStyle8 = new GUIStyle ();
		buttonStyle8.fontSize = 30;
		buttonStyle8.alignment = TextAnchor.MiddleCenter;
		buttonStyle8.normal.background = buttonBackground;
		
		
		buttonStyle9 = new GUIStyle ();
		buttonStyle9.fontSize = 30;
		buttonStyle9.alignment = TextAnchor.MiddleCenter;
		buttonStyle9.normal.background = buttonBackground;
		
		
		buttonStyle10 = new GUIStyle ();
		buttonStyle10.fontSize = 30;
		buttonStyle10.alignment = TextAnchor.MiddleCenter;
		buttonStyle10.normal.background = buttonBackground;
		
		buttonStyle11 = new GUIStyle ();
		buttonStyle11.fontSize = 30;
		buttonStyle11.alignment = TextAnchor.MiddleCenter;
		buttonStyle11.normal.background = buttonBackground;
		
		buttonStyle12 = new GUIStyle ();
		buttonStyle12.fontSize = 30;
		buttonStyle12.alignment = TextAnchor.MiddleCenter;
		buttonStyle12.normal.background = buttonBackground;


		buttonReadyStyle = new GUIStyle ();
		buttonReadyStyle.fontSize = 30;
		buttonReadyStyle.alignment = TextAnchor.MiddleCenter;
		buttonReadyStyle.normal.background = buttonReadyBackground;


		nextCaseStyle = new GUIStyle ();
		nextCaseStyle.fontSize = 50;
		nextCaseStyle.normal.textColor = Color.white;
		nextCaseStyle.alignment = TextAnchor.MiddleCenter;


		loadCase (1);


	}


    void Update()
    {

	  if (diagnosisErrors == 3 || treatmentErrors == 3) { //More than 3 mistakes in the diagnosis or treatment, you lose

			Application.LoadLevel("Game over"); //We load a scene with this message
		}

	  
    }


	/*We load the case "curCase"*/
	void loadCase (int curCase){

		resetStyles ();
		treatmentErrors = 0;
	    diagnosisErrors = 0;
		correctAnswersTreatment = 0;
		showDiagnosis = false;
		showTreatment = false;
		caseInfo = new Dictionary<string, string>();
		currentCaseNumber = curCase;
		currentCase = "case"+currentCaseNumber.ToString();
		caseinfo_filename = @"Assets/Scripts/" + currentCase + ".xml";

		caseText = File.ReadAllText (caseinfo_filename).Split (new string[] {"</case>"}, System.StringSplitOptions.None); //we split the text in lines

		foreach (string info in caseText) {
			int start = info.IndexOf("\"") + 1;
			int end = info.IndexOf("\"", start);
			string key = info.Substring(start, end - start); //The key is the id of each line in the .xml
			string value = info.Substring(end+2); //The value is the informatiion between the <> in the .xml
			caseInfo.Add(key,value); //we add in the dictionary key and value of each line

		}


		//We set up all the strings with the values of the dictionary
		symptoms = "\n\n " + caseInfo["Symptom1"] +  "\n " + caseInfo["Symptom2"] + "\n " + caseInfo["Symptom3"] + "\n " + caseInfo["Symptom4"] + "\n " + caseInfo["Symptom5"] + "\n " + caseInfo["Symptom6"]; 

		informationPatient = "\n\n  Name: " + caseInfo["Name"] +  "\n  First name: " + caseInfo["FirstName"] + "\n  Age: " + caseInfo["Age"] + "\n  Country: " + caseInfo["Country"] + "\n  Weight: " + caseInfo["Weight"] + "\n  Height: " + caseInfo["Height"] ;
	
		antecedentPatient = "\n\n  " + caseInfo["Antecedent1"] +  "\n  " + caseInfo["Antecedent2"] + "\n  " + caseInfo["Antecedent3"] + "\n  " + caseInfo["Antecedent4"] + "\n  " + caseInfo["Antecedent5"] + "\n  " + caseInfo["Antecedent6"]; 
	
		analysis1 = caseInfo ["Analysis1"];
		analysis2 = caseInfo ["Analysis2"];
		analysis3 = caseInfo ["Analysis3"];
		analysis4 = caseInfo ["Analysis4"];
		analysis5 = caseInfo ["Analysis5"];
		analysis6 = caseInfo ["Analysis6"];

		analysis1result = analysis1 + ": \n" + caseInfo ["Analysis1Result"];
		analysis2result = analysis2 + ": \n" + caseInfo ["Analysis2Result"];
		analysis3result = analysis3 + ": \n" + caseInfo ["Analysis3Result"];
		analysis4result = analysis4 + ": \n" + caseInfo ["Analysis4Result"];
		analysis5result = analysis5 + ": \n" + caseInfo ["Analysis5Result"];
		analysis6result = analysis6 + ": \n" + caseInfo ["Analysis6Result"];

		diagnosis1 = caseInfo ["Diagnosis1"];
		diagnosis2 = caseInfo ["Diagnosis2"];
		diagnosis3 = caseInfo ["Diagnosis3"];
		diagnosis4 = caseInfo ["Diagnosis4"];
		diagnosis5 = caseInfo ["Diagnosis5"];
		diagnosis6 = caseInfo ["Diagnosis6"];
		diagnosisCorrect = caseInfo ["DiagnosisCorrect"];

		treatment1 = caseInfo ["Treatment1"];
		treatment2 = caseInfo ["Treatment2"];
		treatment3 = caseInfo ["Treatment3"];
		treatment4 = caseInfo ["Treatment4"];
		treatment5 = caseInfo ["Treatment5"];
		treatment6 = caseInfo ["Treatment6"];
		treatmentCorrect1 = caseInfo ["TreatmentCorrect1"];
		treatmentCorrect2 = caseInfo ["TreatmentCorrect2"];
		treatmentCorrect3 = caseInfo ["TreatmentCorrect3"];
		numberTreatmentCorrect = int.Parse (caseInfo ["NumberTreatmentCorrect"]);


	
	}


	/*We reset the styles, in this way, each time you load a new case, the content of each button is black and not in colors*/
	void resetStyles(){

		buttonStyle1.normal.textColor = Color.black;
		buttonStyle2.normal.textColor = Color.black;
		buttonStyle3.normal.textColor = Color.black;
		buttonStyle4.normal.textColor = Color.black;
		buttonStyle5.normal.textColor = Color.black;
		buttonStyle6.normal.textColor = Color.black;
		buttonStyle7.normal.textColor = Color.black;
		buttonStyle8.normal.textColor = Color.black;
		buttonStyle9.normal.textColor = Color.black;
		buttonStyle10.normal.textColor = Color.black;
		buttonStyle11.normal.textColor = Color.black;
		buttonStyle12.normal.textColor = Color.black;


	}





    void OnGUI(){


		if (!showNextCase) { //We show the minigame

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

				

			//Symptoms of the patient. No interaction, we don't need.

			GUI.Label (new Rect (Screen.width / 3 + 80, 60, Screen.width / 6, 4*Screen.height / 9-20), symptoms, symptomStyle);
			GUI.Label (new Rect (Screen.width / 2 - 140, 20, Screen.width / 6, 50), symptom, titleStyle);
        


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		

			//Information ot the patient
			GUI.Label (new Rect (60, 60, Screen.width / 6, 4*Screen.height / 9), informationPatient, informationStyle);
			GUI.Label (new Rect (Screen.width / 12 , 20, Screen.width / 6, 40), information, titleStyle); 



//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		

		    //Antecendet or other relevant information
			GUI.Label (new Rect (60, Screen.height / 2 +90, Screen.width / 6, 3*Screen.height / 9), antecedentPatient, informationStyle);
			GUI.Label (new Rect (Screen.width / 12 , Screen.height / 2 + 50, Screen.width / 6, 40), antecedent, titleStyle); 




//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

			//The 3 buttons with differents analyse we can do

			GUI.Label (new Rect (Screen.width / 2 -140, Screen.height / 2, Screen.width / 6, 50), analysis, titleStyle);

			if (GUI.Button (new Rect (Screen.width / 3 + 90 , Screen.height / 2 + 60, Screen.width / 6 - 20, Screen.height / 7 - 20), analysis2, buttonAnalysisStyle)) {

				//analysis2 = "Temperature: 39.3º";
				analysis2 = analysis2result;
			}
		

			if (GUI.Button (new Rect (Screen.width / 3 + 90, Screen.height / 2 + 60 +  Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), analysis3, buttonAnalysisStyle)) {

				//analysis3= "Heart rate: 60";
				analysis3 =  analysis3result;
			}

			if (GUI.Button (new Rect (Screen.width / 3 + 90 , Screen.height / 2 + 60 + 2 * Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), analysis4, buttonAnalysisStyle)) {
			
				//analysis4= "Pressure: Normal" ;
				analysis4 =  analysis4result;
			}


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


			if (showDiagnosis) { //If you have click on the button before 

				GUI.Label (new Rect (3 * Screen.width / 4 + 50, 10, Screen.width / 6, 50), diagnosis, titleStyle);

        
				//If you click on, you discover the answer
				if (GUI.Button (new Rect (2 * Screen.width / 3, 60, Screen.width / 6 - 20, Screen.height / 7 - 20), diagnosis1, buttonStyle1)) {
			
					if (diagnosis1.Equals (diagnosisCorrect)) { //is is correct, you show in green and automatically, you show the treatments options
						buttonStyle1.normal.textColor = Color.green;
						showTreatment = true;	 

					} else { 
						buttonStyle1.normal.textColor = Color.red; 
						diagnosisErrors ++;
					}
				}


				if (GUI.Button (new Rect (2 * Screen.width / 3, 60 + Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), diagnosis2, buttonStyle2)) {
			
					if (diagnosis2.Equals (diagnosisCorrect)) {
						buttonStyle2.normal.textColor = Color.green;
						showTreatment = true;	 
					
					} else { 
						buttonStyle2.normal.textColor = Color.red; 
						diagnosisErrors ++;
					}
				}

				if (GUI.Button (new Rect (2 * Screen.width / 3, 60 + 2 * Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), diagnosis3, buttonStyle3)) {

					if (diagnosis3.Equals (diagnosisCorrect)) {
						buttonStyle3.normal.textColor = Color.green;
						showTreatment = true;	 
					
					} else { 
						buttonStyle3.normal.textColor = Color.red; 
						diagnosisErrors ++;
					}
				}


				if (GUI.Button (new Rect (2 * Screen.width / 3 + Screen.width / 6, 60, Screen.width / 6 - 20, Screen.height / 7 - 20), diagnosis4, buttonStyle4)) {
			
					if (diagnosis4.Equals (diagnosisCorrect)) {
						buttonStyle4.normal.textColor = Color.green;
						showTreatment = true;	 
					
					} else { 
						buttonStyle4.normal.textColor = Color.red; 
						diagnosisErrors ++;
					}
				}


				if (GUI.Button (new Rect (2 * Screen.width / 3 + Screen.width / 6, 60 + Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), diagnosis5, buttonStyle5)) {
			
					if (diagnosis5.Equals (diagnosisCorrect)) {
						buttonStyle5.normal.textColor = Color.green;
						showTreatment = true;	 
					
					} else { 
						buttonStyle5.normal.textColor = Color.red; 
						diagnosisErrors ++;
					}
				}

				if (GUI.Button (new Rect (2 * Screen.width / 3 + Screen.width / 6, 60 + 2 * Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), diagnosis6, buttonStyle6)) {
			
					if (diagnosis6.Equals (diagnosisCorrect)) {
						buttonStyle6.normal.textColor = Color.green;
						showTreatment = true;	 
					
					} else { 
						buttonStyle6.normal.textColor = Color.red; 
						diagnosisErrors ++;
					}
				}

			} else {

				if (GUI.Button (new Rect (2 * Screen.width / 3 + 100, Screen.height / 8, Screen.width / 4, Screen.height / 6), "Are you ready for the diagnosis?\n Click here.", buttonReadyStyle)) {
				
					showDiagnosis = true;
				}



			}

//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

			if (showTreatment) {

				GUI.Label (new Rect (9 * Screen.width / 12 + 50, Screen.height / 2, Screen.width / 6, 50), treatment, titleStyle);
		
		

				if (GUI.Button (new Rect (2 * Screen.width / 3, Screen.height / 2 + 60, Screen.width / 6 - 20, Screen.height / 7 - 20), treatment1, buttonStyle7)) {
					if (treatment1.Equals (treatmentCorrect1) || treatment1.Equals (treatmentCorrect2) || treatment1.Equals (treatmentCorrect3)) {  //If is correct you show a message and a button for the next case (or you finish and exit)
						buttonStyle7.normal.textColor = Color.green;
						correctAnswersTreatment++;
						if (numberTreatmentCorrect == correctAnswersTreatment) showNextCase = true;
						
					} else { 
						buttonStyle7.normal.textColor = Color.red; 
						treatmentErrors ++; //You add 1 to the errors.
					}

				}
		
				if (GUI.Button (new Rect (2 * Screen.width / 3, Screen.height / 2 + 60 + Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), treatment2, buttonStyle8)) {
			
					if (treatment2.Equals (treatmentCorrect1)|| treatment2.Equals (treatmentCorrect2) || treatment2.Equals (treatmentCorrect3)) {
						buttonStyle8.normal.textColor = Color.green;
						correctAnswersTreatment++;
						if (numberTreatmentCorrect == correctAnswersTreatment) showNextCase = true;
						
						
					} else { 
						buttonStyle8.normal.textColor = Color.red; 
						treatmentErrors ++;
					}
				}
		
		

				if (!treatment3.Equals("")){
				if (GUI.Button (new Rect (2 * Screen.width / 3, Screen.height / 2 + 60 + 2 * Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), treatment3, buttonStyle9)) {
			
						if (treatment3.Equals (treatmentCorrect1)|| treatment3.Equals (treatmentCorrect2) || treatment3.Equals (treatmentCorrect3)) {
						buttonStyle9.normal.textColor = Color.green;
							correctAnswersTreatment++;
							if (numberTreatmentCorrect == correctAnswersTreatment) showNextCase = true;
					
					} else { 
						buttonStyle9.normal.textColor = Color.red; 
						treatmentErrors ++;
					}
				}
				}


		
				if (!treatment4.Equals("")){
				if (GUI.Button (new Rect (2 * Screen.width / 3 + Screen.width / 6, Screen.height / 2 + 60, Screen.width / 6 - 20, Screen.height / 7 - 20), treatment4, buttonStyle10)) {
			
						if (treatment4.Equals (treatmentCorrect1)|| treatment4.Equals (treatmentCorrect2) || treatment4.Equals (treatmentCorrect3)) {
						buttonStyle10.normal.textColor = Color.green;
							correctAnswersTreatment++;
							if (numberTreatmentCorrect == correctAnswersTreatment) showNextCase = true;

					
					} else { 
						buttonStyle10.normal.textColor = Color.red; 
						treatmentErrors ++;
					}
				}
				}

				if (!treatment5.Equals("")){
				if (GUI.Button (new Rect (2 * Screen.width / 3 + Screen.width / 6, Screen.height / 2 + 60 + Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), treatment5, buttonStyle11)) {
						if (treatment5.Equals (treatmentCorrect1)|| treatment5.Equals (treatmentCorrect2) || treatment5.Equals (treatmentCorrect3)) {
						buttonStyle11.normal.textColor = Color.green;
							correctAnswersTreatment++;
							if (numberTreatmentCorrect == correctAnswersTreatment) showNextCase = true;

					
					} else { 
						buttonStyle11.normal.textColor = Color.red; 
						treatmentErrors ++;
					}
		
				}
		
				}


				if (!treatment6.Equals("")){
				if (GUI.Button (new Rect (2 * Screen.width / 3 + Screen.width / 6, Screen.height / 2 + 60 + 2 * Screen.height / 7, Screen.width / 6 - 20, Screen.height / 7 - 20), treatment6, buttonStyle12)) {
						if (treatment6.Equals (treatmentCorrect1)|| treatment6.Equals (treatmentCorrect2) || treatment6.Equals (treatmentCorrect3)) {
						buttonStyle12.normal.textColor = Color.green;
						  correctAnswersTreatment++;
						  if (numberTreatmentCorrect == correctAnswersTreatment) showNextCase = true;
					
					} else { 
						buttonStyle12.normal.textColor = Color.red; 
						treatmentErrors ++;
					}

				}
				}

			}


//-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

		}else {

			if (currentCaseNumber == lastCase) { //We finished all the cases

				GUI.Label (new Rect (Screen.width / 6, 2* Screen.height/3, 2 *Screen.width / 3, Screen.height / 3), endCase, nextCaseStyle); 
				
				
				if (GUI.Button (new Rect (5 * Screen.width / 6, 5*Screen.height / 6 , Screen.width / 8, Screen.height / 8), "Exit", buttonReadyStyle)){
					Application.LoadLevel("FinalScene"); //We go to the final scene
				}




			} else { //We have more cases

			    GUI.Label (new Rect (Screen.width / 6, 2* Screen.height/3, 2 *Screen.width / 3, Screen.height / 3), nextCase, nextCaseStyle); 


			    if (GUI.Button (new Rect (5 * Screen.width / 6, 5*Screen.height / 6 , Screen.width / 8, Screen.height / 8), "Next case", buttonReadyStyle)){ 
				    currentCaseNumber ++;
				    showNextCase = false;
					loadCase(currentCaseNumber); //We load the next case


			    }
			
			}
		}

	} 



}
