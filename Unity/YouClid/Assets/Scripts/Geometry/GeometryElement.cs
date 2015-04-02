using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class GeometryElement : MonoBehaviour 
{
	public GameObject mainDisplay;

	private GeometryElement parentElement_ = null;
	private List < GeometryElement > childElements_ = new List<GeometryElement>();

	// Use this for initialization
	private void Start () 
	{
		this.DoStart ();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		this.DoUpdate ();	
	}

	protected void Init()
	{
	}

	abstract protected void DoStart();
	abstract protected void DoUpdate();
}
