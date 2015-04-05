using UnityEngine;
using System.Collections;

abstract public class GeometryElementDisplay : MonoBehaviour 
{
	protected GeometryElement element_;

	void Start () 
	{
		DoStart ();
	}
	protected abstract void DoStart();

	public void OnStateChanged()
	{
		DoOnStateChanged ();
	}
	abstract protected void DoOnStateChanged();
}
