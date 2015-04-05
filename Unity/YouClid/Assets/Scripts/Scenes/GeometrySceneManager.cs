using UnityEngine;
using System.Collections;

public class GeometrySceneManager : SingletonSceneLifetime< GeometrySceneManager > 
{
	private GeometrySpace geometrySpace_;
	private GeometryInputManager inputManager_;
	public Camera geometryCamera;

	void Start () 
	{
		Debug.Log ("GeometryScene:Start");

		CreateGeometrySpace ();
		CreateInputManager ();
	}	

	private PrefabLoader< GeometryInputManager > inputManagerPrefabLoader = new PrefabLoader< GeometryInputManager >( "Prefabs/Geometry/GeometryInputManager"); 
	private void CreateInputManager()
	{
		inputManager_ = inputManagerPrefabLoader.LoadInstance ();
		inputManager_.transform.parent = this.transform;
	}

	private PrefabLoader< GeometrySpace > spacePrefabLoader = new PrefabLoader< GeometrySpace >( "Prefabs/Geometry/GeometrySpace"); 
	private void CreateGeometrySpace()
	{
		StartCoroutine (CreateGeometrySpaceCR ());
	}
	private IEnumerator CreateGeometrySpaceCR()
	{
		geometrySpace_ = spacePrefabLoader.LoadInstance ();
		PointFactory.Instance.transform.parent = this.transform;
		yield return null;
		if (!PointFactory.IsInitialised()) 
		{
			Debug.LogError("Failed to initialise pointFactory");
		}
		yield return null;

		float[] xs = new float[]{ -6f, 6f };
		float[] ys = new float[]{ -2f, 2f };
		float[] zs = new float[]{ -3f, 3f };

		foreach ( float x in xs) 
		{
			foreach (float y in ys)
			{
				foreach (float z in zs)
				{
					PointFactory.Instance.CreateSolo(geometrySpace_, 
						new Vector3( x, y, z ),
					    "Solo_"+x.ToString()+"_"+y.ToString()+"_"+z.ToString());
					yield return null;
				}
			}
		}
	}
}
