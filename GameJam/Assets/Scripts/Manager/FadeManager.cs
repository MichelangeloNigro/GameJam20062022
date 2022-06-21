using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : SingletonDDOL<FadeManager> {
	public GameObject fade;
	
	public GameObject caricamento;

	private IEnumerator Start() {
		//caricamento.SetActive(true);
		//Time.timeScale = 0.1f;
		yield return new WaitForSeconds(0.2f);
		//caricamento.SetActive(false);
		fade.GetComponent<RawImage>().CrossFadeAlpha(0, 1.2f, false);
		//Time.timeScale = 1;
		
	}

	public IEnumerator ReloadScene() {
		fade.GetComponent<RawImage>().CrossFadeAlpha(1, 1.2f, false);
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//SaveAndLoad.Instance.StartCoroutine(SaveAndLoad.Instance.Load());
	}
	
	
	
}
