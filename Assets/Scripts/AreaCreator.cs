using UnityEngine;
using System.Collections;

// When the object stops moving, it creates an area
public class AreaCreator : MonoBehaviour {
	
	public string PrefabNameToCreate = "No area";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rigidbody.velocity.magnitude < Mathf.Epsilon)
		{
			CreateArea ();	
		}
	}
	
	// Creates an area where we are and removes ourselves.
	private void CreateArea()
	{
		// Create the area prefab
		Instantiate(Resources.Load("Areas/" + PrefabNameToCreate), transform.position + new Vector3(0,0,1), Quaternion.identity);
		
		// Seppuku
		Object.Destroy(this.gameObject);
	}
}
