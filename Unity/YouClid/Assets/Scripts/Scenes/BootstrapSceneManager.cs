using UnityEngine;
using System.Collections;

public class BootstrapSceneManager : MonoBehaviour 
{

	void Start () 
	{
		Debug.Log ("Bootstrap:Start");
		Application.LoadLevel ("GeometryScene");
	}	
}
