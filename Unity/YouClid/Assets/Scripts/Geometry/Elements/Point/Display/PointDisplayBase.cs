using UnityEngine;
using System.Collections;

abstract public class PointDisplayBase : GeometryElementDisplay 
{
	GeometryElementPoint pointElement_ = null;

	protected override void DoStart()
	{
		element_ = pointElement_ = transform.parent.GetComponent< GeometryElementPoint > ();
		if (pointElement_ == null) 
		{
			Debug.LogError ("element isn't a point");
		}
	}
}
