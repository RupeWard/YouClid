using UnityEngine;
using System.Collections;

public class PointDisplayMain : PointDisplayBase 
{
	private MeshRenderer renderer_ = null;

	protected void SetColour(Color c)
	{
		renderer_.material.SetColor ("_Color", c);
	}

	#region GeometryElementDisplay
	protected override void DoStart()
	{
		base.DoStart ();
		renderer_ = GetComponent < MeshRenderer > ();
		if (renderer_ == null) 
		{
			Debug.LogError ("No MeshRender");
		}
	}

	private Color activeColour_ = Color.yellow;
	private Color inactiveColour_ = Color.white;
	private Color selectedColour_ = Color.green;
	private Color movingColour_ = Color.blue;

	protected override void DoOnStateChanged()
	{
		switch (element_.State) 
		{
			case EElementState.Hidden:
			{
				gameObject.SetActive(false);
				break;
			}
			case EElementState.Active:
			{
				SetColour(activeColour_);
				gameObject.SetActive(true);
				break;
			}
			case EElementState.Inactive:
			{
				SetColour(inactiveColour_);
				gameObject.SetActive(true);
				break;
			}
			case EElementState.Moving:
			{
				SetColour(movingColour_);
				gameObject.SetActive(true);
				break;
			}
			case EElementState.Selected:
			{
				SetColour(selectedColour_);
				gameObject.SetActive(true);
				break;
			}
		}
	}
	#endregion GeometryElementDisplay
}
