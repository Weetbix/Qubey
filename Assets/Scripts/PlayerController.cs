using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	
	public float MaxMoveForce = 100.0f;
	public float MaxHorizontalVelocity = 3.0f;
	public float JumpRayDistance = 1.1f;
	public float JumpForce = 4000.0f;
	
	private float currentThrowStrength = 0.0f;
	public float MaxThrowStrength = 1.0f;
	public float MinThrowStrength = 0.0f;
	public float ThrowStrengthBuildUpSpeed = 0.5f;
	
	private int currentGun = 0;
	private List<List<string>> gunSelection = new List<List<string>>()
	{
		// GUI String - Prefab name to create when you fire
		{ new List<string>(){ "Velocity Effector", "AreaCreator_VelocityEffector" } },
		{ new List<string>(){ "Scale Effector", "AreaCreator_ScaleEffector" } },
		{ new List<string>(){ "Scale Doubler", "AreaCreator_ScaleSetterEffector" } }
	};
	
	
	// Gets the normalised 'aiming' vector
	public Vector3 AimDirection
	{
		get
		{
			// We need to specify a z position to correctly use the ScreenToWorldPoint function,
			// so work our how far away the camera is to 0 on the z plane..
			var mousePos = Input.mousePosition;
			mousePos.z = -Camera.main.transform.position.z;
		
			var mousePointInWorld = Camera.main.ScreenToWorldPoint(mousePos);
			return (mousePointInWorld - transform.position).normalized;
		}
	}
	
	public bool IsOnGround
	{
		get 
		{
			Debug.DrawLine(transform.position, transform.position + (Vector3.down * JumpRayDistance), Color.red);
			return Physics.Raycast(transform.position, Vector3.down, JumpRayDistance);
		}
	}
	
	void OnGUI(){
		GUI.Label( new Rect(10,10, 500, 100), "Weapon: " + gunSelection[currentGun][0]);
		GUI.Label( new Rect(10, Screen.height - 40, 400, 20), "Number keys to change weapons" );
		GUI.Label( new Rect(10, Screen.height - 25, 400, 20), "R to Reset" );
	}
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	private void DrawDebug()
	{
		const float throwLineMaxLength = 3.0f;
		float actualThrowLineLength = (currentThrowStrength/MaxThrowStrength) * throwLineMaxLength;
		Debug.DrawLine(transform.position,transform.position + (AimDirection * actualThrowLineLength), Color.red);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if(Input.GetAxis("Horizontal") != 0.0f && Mathf.Abs(rigidbody.velocity.x) <= MaxHorizontalVelocity)
		{			
			rigidbody.AddForce(Vector3.right * MaxMoveForce * Input.GetAxis ("Horizontal"));
		}
		
		if(Input.GetKeyDown(KeyCode.W) && IsOnGround)
		{
			rigidbody.AddForce(Vector3.up * JumpForce);
		}
		
		if(Input.GetMouseButton(0))
		{
			if(currentThrowStrength < MaxThrowStrength)
			{
				currentThrowStrength += Time.deltaTime * ThrowStrengthBuildUpSpeed;
			}
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			if(currentThrowStrength > 0.0f)
			{
				Throw();
				currentThrowStrength = 0.0f;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(0);	
		}
		
		HandleWeaponSelection();
		DrawDebug();
	}
	
	private void Throw()
	{
		const float throwOffsetDistance = 1.0f;
		var objectPosition = transform.position + (AimDirection * throwOffsetDistance);
		GameObject newObject = Instantiate(Resources.Load(gunSelection[currentGun][1]), objectPosition, Quaternion.identity) as GameObject;
		
		// Throw it!
		newObject.rigidbody.AddForce(AimDirection * (MinThrowStrength + currentThrowStrength)); 
	}
	
	void HandleWeaponSelection()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1)) currentGun = 0;
		if(Input.GetKeyDown(KeyCode.Alpha2)) currentGun = 1;
		if(Input.GetKeyDown(KeyCode.Alpha3)) currentGun = 2;
	}
}
