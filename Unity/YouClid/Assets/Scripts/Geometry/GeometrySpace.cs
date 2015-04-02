using UnityEngine;
using System.Collections;

public class GeometrySpace : MonoBehaviour 
{
	private GameObject elementsContainer_ = null;

	void Awake()
	{
		elementsContainer_ = new GameObject ();
		elementsContainer_.transform.parent = this.transform;
		elementsContainer_.name = "Elements";
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
