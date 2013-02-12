using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

// An area effector which has one effect that is applied when the object enters the 
// area, and is potentially 'un-applied' when the object leaves the area.
public abstract class StaticAreaEffector : MonoBehaviour
{
	// Keep a list of all the currently collided objects and some saved/attached data
	Dictionary<GameObject, object> effectedObjects = new Dictionary<GameObject, object>();
	
	public StaticAreaEffector (){}
	
	// Whenever an object enters our collider, apply the effect and let the
	// derived classes save out some data about this game object when it
	// originally collided (ie. original mass, so we can set it back when we exit).
	void OnTriggerEnter(Collider other)
	{
		object savedData = null;
		EffectOnEnter(other.gameObject, out savedData);
		
		effectedObjects[other.gameObject] = savedData;
	}
	
	// When an object exits the collider, we want to let the base class 'un-apply' its
	// changes, usually by using something they have saved in the saved object (map value)
	void OnTriggerExit(Collider other)
	{
		EffectOnExit(other.gameObject, effectedObjects[other.gameObject]);
		effectedObjects.Remove(other.gameObject);
	}
	
	// Override these in concrete classes to effect the game objects
	protected abstract void EffectOnEnter(GameObject obj, out object savedData);
	protected abstract void EffectOnExit(GameObject obj, object savedData);
}