using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuInteraction : MonoBehaviour {

	public string newGameScene;

	void Start () {

	}
	
	public void startNewGame(){
		GameInformation.main.allResourcesToDefault();
		GameInformation.main.allStatsToDefault();
		SceneManager.LoadScene(newGameScene);
	}

	public void loadSaveGame(){
		// loading will go here
	}

	public void exitApplication(){
		Application.Quit();
	}


}
