using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ScaleEffector : MonoBehaviour {
	
	Dictionary<GameObject, Action<GameObject>> affectedObjects = new Dictionary<GameObject, Action<GameObject>>();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnTriggerEnter(Collider other)
	{
		var originalColor = other.gameObject.renderer.material.color;
		affectedObjects[other.gameObject] = (obj) => {
			obj.renderer.material.color = originalColor;
		};
		
		other.gameObject.renderer.material.color = Color.green;
	}
	
	void OnTriggerExit(Collider other)
	{
		affectedObjects[other.gameObject](other.gameObject);
		affectedObjects.Remove(other.gameObject);
	}
}