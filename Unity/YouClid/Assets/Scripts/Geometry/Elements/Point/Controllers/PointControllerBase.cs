using UnityEngine;
using System.Collections;

abstract public class PointControllerBase : GeometryElementController 
{
	protected GeometryElementPoint pointElement_ = null;

	protected override void DoStart () 
	{
		pointElement_ = Element as GeometryElementPoint;
		if (pointElement_ == null) 
		{
			Debug.LogWarning("Parent of input element "+gameObject.name+" is not a GeometryElementPoint");
		}	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
