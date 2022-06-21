using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
	public Action hasDestoyed;
	
	
	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Scenario")) {
			hasDestoyed?.Invoke();
		}
	}
}
