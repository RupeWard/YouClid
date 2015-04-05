using UnityEngine;
using System.Collections;

public class PointControllerSelect : PointControllerBase 
{

	protected override void DoStart () 
	{
		base.DoStart ();	
		Init ("SelectController");
	}
	
	void Update () 
	{
	
	}

	#region GeometryElementController
	protected override void DoRegisterTouch(TouchPhase phase)
	{
		Debug.Log ("PointSelect controller registered touch phase " + phase);
		if (phase == TouchPhase.Began) 
		{
			switch (Element.State)
			{
				case EElementState.Active:
				{
					Element.SetState(EElementState.Selected);
					break;
				}
				case EElementState.Selected:
				{
					Element.SetState(EElementState.Active);
					break;
				}
			}
		}
	}

	protected override void DoActivate()
	{
	}

	protected override void DoDeactivate()
	{
	}
	#endregion GeometryElementController

}
