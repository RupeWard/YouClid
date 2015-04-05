using UnityEngine;
using System.Collections;

public class PrefabLoader < T >
{
	static public bool DEBUG_PREFABLOADER = true;

	private string prefabPath_;
	private GameObject prefab_;

	public PrefabLoader( string path)
	{
		prefabPath_ = path;
		 
	}


	public T LoadInstance()
	{
		T result = default( T );
		if (prefab_ == null) 
		{
			prefab_ = Resources.Load(prefabPath_) as GameObject;
			if (prefab_ == null)
			{
				Debug.LogError ("PrefabLoader couldn't load prefab at '" + prefabPath_ + "'");
			}
		}

		if (prefab_ != null) 
		{
			GameObject go = GameObject.Instantiate(prefab_);
			go.name = typeof(T).ToString();
			result = go.GetComponent< T >();
			if (result == null)
			{
				Debug.LogError("PrefabLoader found prefab at '"+prefabPath_+"' had no "+go.name+" component!");
			}
			else
			{
				if (DEBUG_PREFABLOADER)
				{
					Debug.Log("PrefabLoader created "+go.name+" instance of prefab at '"+prefabPath_+"'");
				}
			}
		}
		return result;
	}
}
