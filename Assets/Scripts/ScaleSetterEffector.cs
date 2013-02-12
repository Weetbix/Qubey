using System;
using UnityEngine;

// Instantly multiplies the scale of any rigid bodies that enter this zone, and sets
// their scale back once they leave the zone.
public class ScaleSetterEffector : StaticAreaEffector
{
	// There's only one value here at the moment so this sub class is overkill
	// but its an example of how to use the saved data to save multiple stats
	// about an object when it enters the zone.
	private class OriginalValues
	{
		public Vector3 Scale { get; set; }
	}
	
	public float ScaleMultiplier = 1.0f;
	
	public ScaleSetterEffector (){}
	
	protected override void EffectOnEnter(GameObject obj, out object savedData)
	{
		if(obj.rigidbody != null)
		{
			savedData = new OriginalValues { Scale = obj.transform.localScale };
			obj.transform.localScale *= ScaleMultiplier;
		}
		else 
		{
			savedData = null;	
		}
	}
	
	protected override void EffectOnExit(GameObject obj, object savedData)
	{
		if(obj.rigidbody != null)
		{
			obj.transform.localScale = (savedData as OriginalValues).Scale;
		}
	}
}