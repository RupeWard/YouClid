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

	public void AddElement( GeometryElement element)
	{
		element.gameObject.transform.parent = elementsContainer_.transform; 
	}

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
}
