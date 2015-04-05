using UnityEngine;
using System.Collections;

abstract public class GeometryElementController : MonoBehaviour 
{
	private GeometryElement element_ = null;
	public GeometryElement Element
	{
		get { return element_; }
	}

	void Awake()
	{
	}

	private string name_ = "_ElementController";
	public string Name
	{
		get { return name_; }
	}

	protected void Init( string n)
	{
		name_ = n;
	}

	public void Start()
	{
		element_ = transform.parent.gameObject.GetComponent< GeometryElement > ();
		if (element_ == null) 
		{
			Debug.LogWarning("Parent of input element "+gameObject.name+" is not a GeometryElement");
		}
		DoStart ();
	}
	abstract protected void DoStart();

	public void RegisterTouch(TouchPhase phase)
	{
		Debug.Log ("Touch phase " + phase + " on controller '" + gameObject.name+"'");
		DoRegisterTouch (phase);
	}
	abstract protected void DoRegisterTouch(TouchPhase phase);

	public void Activate()
	{
		DoActivate ();
	}
	abstract protected void DoActivate();

	public void Deactivate()
	{
		DoDeactivate ();
	}
	abstract protected void DoDeactivate();
	

}
