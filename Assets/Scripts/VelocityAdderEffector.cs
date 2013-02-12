using System;
using UnityEngine;

public class VelocityAdderEffector : ConstantAreaEffector
{
	public Vector3 velocityAddedPerSecond = new Vector3(0,0,0);
	
	public VelocityAdderEffector(){}
	
	protected override void Effect(GameObject obj)
	{
		obj.rigidbody.velocity += velocityAddedPerSecond * Time.deltaTime;
	}
}

