using UnityEngine;
using System.Collections;

public class GeometryInputManager : MonoBehaviour 
{
//	GeometryElementController selectedElement_ = null;

	private bool isActive_ = true;
	private int layerMask_ = 0;

	void Awake()
	{
		layerMask_ = UnityHelpers.GetLayerMask ("GeomElementsLayer");
	}

	void Start () 
	{
	
	}

	/*
	public void Deselect( GeometryElementController element)
	{
	}

	public void Select( GeometryElementController element )
	{
	}
	*/

	private Vector3 lastTouchPosition = Vector3.zero;
	private float touchMoveDistanceThreshold = 0.1f;
	private bool isSamePosition(Vector3 v1, Vector3 v2) // TODO Extension?
	{
		Vector3 diff = v1 - v2;
		return (diff.magnitude < touchMoveDistanceThreshold);
	}

	void Update () 
	{
		if (isActive_) 
		{
			Vector3 position = Vector3.zero;
			bool touchDetected = false;
			TouchPhase phase = TouchPhase.Stationary;

			if (Input.GetMouseButtonDown (0)) 
			{
				Debug.Log("BUTTON 0 DOWN at "+Input.mousePosition);
				touchDetected = true;
				position = Input.mousePosition;
				phase = TouchPhase.Began;
			} 
			else if (Input.GetMouseButtonUp(0))
			{
				Debug.Log("BUTTON 0 UP at "+Input.mousePosition);
				touchDetected = true;
				position = Input.mousePosition;
				phase = TouchPhase.Ended;
			}
			else if (Input.GetMouseButton(0))
			{
				Debug.Log("BUTTON 0 at "+Input.mousePosition);
				touchDetected = true;

				if (isSamePosition(position, Input.mousePosition))
				{
					phase = TouchPhase.Stationary;
				}
				else
				{
					phase = TouchPhase.Moved;
					position = Input.mousePosition;
				}

			}
						
			if (Input.touches.Length > 0) 
			{
				position = Input.touches[0].position;
				phase = Input.touches[0].phase;
				if (phase == TouchPhase.Began)
				{
					Debug.Log("TOUCH START");
				}
				touchDetected = true;
			}

			if (touchDetected)
			{
				Ray ray = GeometrySceneManager.Instance.geometryCamera.ScreenPointToRay (position);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, 200, layerMask_))
				{
					Transform t = hit.transform;
					Debug.Log("Ray hit "+t.gameObject.name); 
					
					GeometryElementController hitElement = t.gameObject.GetComponent< GeometryElementController >();
					if (hitElement != null)
					{
						Debug.Log ("GIM: touched "+hitElement.gameObject.name);
						hitElement.RegisterTouch( phase );
					}

				}          
			}
		}
	}
}
