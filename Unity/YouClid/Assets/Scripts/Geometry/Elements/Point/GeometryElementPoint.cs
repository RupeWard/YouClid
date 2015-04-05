using UnityEngine;
using System.Collections;

public class GeometryElementPoint : GeometryElement 
{


	private float displaySize_ = 0.2f;

	private Vector3 position_ = Vector3.zero;

	#region Chaining setters
	public GeometryElementPoint SetPosition( Vector3 position)
	{
		position_ = position;
		transform.position = position_;
		mainDisplay.transform.localScale = displaySize_ * Vector3.one;
		return this;
	}
	#endregion Chaining setters

	protected void Awake()
	{
	}
	
	#region GeometryElement (base)
	protected override void DoStart () 
	{
		// for a solo point, override for child points
	}
	
	protected override void DoUpdate () 
	{
	
	}

	protected override void DoSetState(EElementState state)
	{

	}

	#endregion GeometryElement (base)


}
