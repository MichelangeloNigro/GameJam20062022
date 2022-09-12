using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {
	public bool disableShoot;

	private void Awake() {
		disableShoot = false;
	}

	private void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(ray.origin,ray.direction*1000);
		if(Physics.Raycast(ray.origin,ray.direction,out hit,10000,LayerMask.GetMask("Building")) && !disableShoot)
		{ 
			Debug.Log(hit.collider.name);
			if (hit.collider.GetComponent<BuilderLoaderScene>()) {
				Debug.Log("building");
				if (Input.GetMouseButtonDown(0)) {
					disableShoot = true;
					StartCoroutine(FadeManager.Instance.LoadNewScene(hit.collider.GetComponent<BuilderLoaderScene>().LoadSceneSelected()));
				}
			}
			
		}
	}
}
