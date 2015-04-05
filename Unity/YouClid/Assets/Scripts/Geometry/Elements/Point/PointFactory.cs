using UnityEngine;
using System.Collections;

public class PointFactory : SingletonSceneLifetimeLazy< PointFactory> 
{
	#region Factory methods
	private PrefabLoader< GeometryElementPoint > s_prefabLoader 
		= new PrefabLoader< GeometryElementPoint >( "Prefabs/Geometry/Elements/GeometryPoint"); 
	
	public void CreateSolo( GeometrySpace space, Vector3 position, string name )
	{
		StartCoroutine (CreateSoloCR (space, position, name));
	}
	private IEnumerator CreateSoloCR( GeometrySpace space, Vector3 position, string name )
	{
		GeometryElementPoint point = s_prefabLoader.LoadInstance ();
		
		PointControllerSelect pcs = point.mainDisplay.AddComponent< PointControllerSelect > ();
		point.Init (space);
		yield return null;

		point.SetName (name);
		point.SetPosition(position)
			.AddController (pcs, true)
			.AddDisplay (point.mainDisplay.GetComponent< PointDisplayMain > ())
				.SetState (EElementState.Active);

	}
	
	#endregion Factory methods

}
