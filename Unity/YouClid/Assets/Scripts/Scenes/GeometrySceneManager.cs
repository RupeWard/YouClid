using UnityEngine;
using System.Collections;

public class GeometrySceneManager : SingletonSceneLifetime< GeometrySceneManager > 
{
	private GeometrySpace geometrySpace_;

	void Start () 
	{
		Debug.Log ("GeometryScene:Start");

		CreateGeometrySpace ();

	}	

	private PrefabLoader< GeometrySpace > prefabLoader = new PrefabLoader< GeometrySpace >( "Prefabs/Geometry/GeometrySpace"); 
	private void CreateGeometrySpace()
	{
		geometrySpace_ = prefabLoader.LoadInstance ();

		float[] xs = new float[]{ -6f, 6f };
		float[] ys = new float[]{ -2f, 2f };
		float[] zs = new float[]{ -3f, 3f };

		foreach (float x in xs) 
		{
			foreach (float y in ys)
			{
				foreach (float z in zs)
				{
					GeometryElementPoint.Create( new Vector3( x, y, z ));
				}
			}
		}
	}
}
