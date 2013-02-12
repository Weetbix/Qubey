using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class ConstantAreaEffector : MonoBehaviour {
	
	List<GameObject> effectedObjects = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var obj in effectedObjects)
		{
			Effect(obj);	
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		effectedObjects.Add(other.gameObject);
	}
	
	void OnTriggerExit(Collider other)
	{
		effectedObjects.Remove(other.gameObject);
	}
	
	// You do it!
	protected abstract void Effect(GameObject obj);
}
