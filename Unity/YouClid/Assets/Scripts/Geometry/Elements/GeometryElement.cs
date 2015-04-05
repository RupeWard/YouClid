using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class GeometryElement : MonoBehaviour 
{
	public GameObject mainDisplay;
	
	private GeometryElement parentElement_ = null;
	private HashSet < GeometryElement > childElements_ = new HashSet<GeometryElement>();

	#region state
	private EElementState state_ = EElementState.Inactive;

	public EElementState State
	{
		get { return state_; }
	}

	public GeometryElement SetState(EElementState state)
	{
		if (state != state_) 
		{
			state_ = state;
			this.DoSetState (state);
			foreach (GeometryElementDisplay display in displays_) 
			{
				display.OnStateChanged();
			}
			if (currentController_ != null)
			{
				// Do anything?
			}
		}
		return this;
	}
	abstract protected void DoSetState(EElementState state);

	#endregion state

	#region Controllers
	private HashSet < GeometryElementController > controllers_ = new HashSet<GeometryElementController> ();
	private GeometryElementController currentController_ = null;

	protected GeometryElementController GetController()
	{
		return GetController< GeometryElementController > ();
	}

	protected T GetController< T >() where T:GeometryElementController
	{
		T result = default(T);
		foreach (GeometryElementController g in controllers_) 
		{
			T t = g as T;
			if (t != null)
			{
				result =  t;
				break;
			}
		}
		if (result == null) 
		{
			Debug.LogWarning("Element '"+gameObject.name+"' has no controller of type "+typeof(T));
		}
		return result;
	}

	protected GeometryElementController GetController(string name)
	{
		return GetController< GeometryElementController > (name);
	}

	protected T GetController< T >(string name) where T:GeometryElementController
	{
		T result = default(T);
		foreach (GeometryElementController g in controllers_) 
		{
			T t = g as T;
			if (t != null && t.Name == name)
			{
				result =  t;
				break;
			}
		}
		if (result == null) 
		{
			Debug.LogWarning("Element '"+gameObject.name+"' has no controller '"+name+"' of type "+typeof(T));
		}
		return result;
	}

	public GeometryElement AddController(GeometryElementController controller)
	{
		return AddController(controller, false);
	}
	public GeometryElement AddController(GeometryElementController controller, bool active)
	{
		if (controllers_.Contains (controller)) 
		{
			Debug.LogWarning ("Already contains "+controller.gameObject.name);
		}
		controllers_.Add (controller);
		if (active) 
		{
			SetController(controller);
		}
		return this;
	}

	public void SetController(string name)
	{
		GeometryElementController controller = GetController(name);

		if (currentController_ != controller) 
		{
			if (currentController_ != null)
			{
				currentController_.Deactivate();
			}
			currentController_ = controller;
			if (currentController_ != null)
			{
				currentController_.Activate();
			}
		}
	}

	public void SetController(GeometryElementController controller)
	{
		if (controllers_.Contains (controller)) 
		{
			if (currentController_ != controller) 
			{
				if (currentController_ != null)
				{
					currentController_.Deactivate();
				}
				currentController_ = controller;
				if (currentController_ != null)
				{
					currentController_.Activate();
				}
			}
		} 
		else 
		{
			Debug.LogError ("Can't set unregistered controller '"+controller.name+"'");
		}
	}

	#endregion Controllers

	#region Displays
	private HashSet < GeometryElementDisplay > displays_ = new HashSet<GeometryElementDisplay> ();

	protected T GetDisplay< T >() where T:GeometryElementDisplay
	{
		T result = default(T);
		foreach (GeometryElementDisplay g in displays_) 
		{
			T t = g as T;
			if (t != null)
			{
				result =  t;
				break;
			}
		}
		if (result == null) 
		{
			Debug.LogWarning("Element '"+gameObject.name+"' has no display of type "+typeof(T));
		}
		return result;
	}
	
	protected GeometryElementDisplay GetDisplay(string name)
	{
		return GetDisplay< GeometryElementDisplay > (name);
	}
	
	protected T GetDisplay< T >(string name) where T:GeometryElementDisplay
	{
		T result = default(T);
		foreach (GeometryElementDisplay g in displays_) 
		{
			T t = g as T;
			if (t != null && t.gameObject.name == name)
			{
				result =  t;
				break;
			}
		}
		if (result == null) 
		{
			Debug.LogWarning("Element '"+gameObject.name+"' has no display '"+name+"' of type "+typeof(T));
		}
		return result;
	}
	
	public GeometryElement AddDisplay(GeometryElementDisplay display)
	{
		return AddDisplay(display, false);
	}
	public GeometryElement AddDisplay(GeometryElementDisplay display, bool active)
	{
		if (displays_.Contains (display)) 
		{
			Debug.LogWarning ("Already contains "+display.gameObject.name);
		}
		displays_.Add (display);
		display.gameObject.SetActive (active);
		Debug.Log ("Added display of type " + typeof(Display) + " " + active);
		return this;
	}
	
	public void ActivateDisplay(string name, bool active)
	{
		GeometryElementDisplay display = GetDisplay(name);
		if (display != null) 
		{
			display.gameObject.SetActive(active);
		}
	}
	
	#endregion Displays

	private GeometrySpace space_ = null;

	public GeometryElement Init(GeometrySpace space)
	{
		space_ = space;
		space_.AddElement (this);
		return this;
	}

	#region Chaining setters
	public GeometryElement SetName(string n)
	{
		gameObject.name = n;
		return this;
	}
	#endregion Chaining setters

	#region MB Functions with subclass handlers
	private void Start () 
	{
		this.DoStart ();
	}
	abstract protected void DoStart();

	private void Update () 
	{
		this.DoUpdate ();	
	}
	abstract protected void DoUpdate();

	#endregion MB Functions with subclass handlers



}
