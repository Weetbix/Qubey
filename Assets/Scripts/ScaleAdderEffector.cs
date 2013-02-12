using System;
using UnityEngine;

public class ScaleAdderEffector : ConstantAreaEffector
{
	public Vector3 scaleAddedPerSecond = new Vector3(0,0,0);
	
	public ScaleAdderEffector(){}
	
	protected override void Effect(GameObject obj)
	{
		if(obj.rigidbody != null)
		{
			obj.transform.localScale += scaleAddedPerSecond * Time.deltaTime;
			obj.rigidbody.WakeUp();
		}
	}
}



