using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public GameObject fade;

	private void Start() {
		fade.GetComponent<RawImage>().CrossFadeAlpha(0, 0.5f, false);
	}

	public void StartRun() {
		fade.GetComponent<RawImage>().CrossFadeAlpha(1, 0.5f, false);
		
		StartCoroutine(ChangeScene());
	}

	private IEnumerator ChangeScene() {
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadSceneAsync("Gameplay");
	}

	public void Quit() {
		Application.Quit();
	}
}
