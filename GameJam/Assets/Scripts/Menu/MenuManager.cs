using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{


	public void StartRun() {
		SceneManager.LoadScene("Gameplay");
	}


	public void Quit() {
		Application.Quit();
	}
}
