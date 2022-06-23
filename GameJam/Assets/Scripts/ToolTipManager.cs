using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour {
	public Canvas canvas;
	public GameObject toolTip;
	public TMP_Text header;
	public TMP_Text text;
	public static ToolTipManager instace;

	private void Start() {
		//canvas.gameObject.SetActive(false);
	}

	private void Awake() {
		instace = this;
	}

	public void ShowToolTip(string headerText, string description) {
		canvas.gameObject.SetActive(true);
		toolTip.transform.position = Input.mousePosition;
		header.text = headerText;
		text.text = description;
		StartCoroutine(Off());
	}


	public void OffToolTip() {
		StartCoroutine(Off());
	}

	IEnumerator Off() {
		yield return new WaitForSeconds(1f);
		canvas.gameObject.SetActive(false);
		
	}
}
