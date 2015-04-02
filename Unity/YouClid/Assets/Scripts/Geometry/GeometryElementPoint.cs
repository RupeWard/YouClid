using UnityEngine;
using System.Collections;

public class GeometryElementPoint : GeometryElement 
{
	static private PrefabLoader< GeometryElementPoint > s_prefabLoader = new PrefabLoader< GeometryElementPoint >( "Prefabs/Geometry/GeometryPoint"); 

	static public GeometryElementPoint Create( Vector3 position )
	{
		GeometryElementPoint point = s_prefabLoader.LoadInstance ();
		point.SetPosition (position);

		return point;
	}

	private float displaySize_ = 0.2f;

	private Vector3 position_ = Vector3.zero;

	protected void Awake()
	{
	}

	protected override void DoStart () 
	{
		
	}
	
	protected override void DoUpdate () 
	{
	
	}

	public void Init(Vector3 position)
	{
		SetPosition (position); 
	}

	public void SetPosition( Vector3 position)
	{
		position_ = position;
		mainDisplay.transform.localScale = displaySize_ * Vector3.one;
		mainDisplay.transform.position = position_;
	}
}
