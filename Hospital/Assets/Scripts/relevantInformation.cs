using UnityEngine;
using System.Collections;

public class relevantInformation : MonoBehaviour {

	Inventory interaction;
	public Transform position;
	GameObject player;



	// Use this for initialization
	void Start () {
	
	//	interaction = GetComponent<Inventory> ();
	//	player = GameObject.Find ("Player");
	
	
	}



	// Update is called once per frame
	void Update () {

//		position = player.transform;

	
	}

	void Awake () {
		DontDestroyOnLoad (this.gameObject);
	}

}
