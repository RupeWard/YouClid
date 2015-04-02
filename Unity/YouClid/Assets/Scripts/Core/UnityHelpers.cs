using UnityEngine;

static public class UnityHelpers
{
	public static int GetLayerNum(this string s)
	{
		return LayerMask.NameToLayer ( s );
	}

    public static void Destroy(UnityEngine.Object theObject)
    {
#if UNITY_EDITOR
        Object.DestroyImmediate(theObject);
#else
        Object.Destroy(theObject);
#endif
    }


	public static double DLerp(double from, double to, double fraction)
	{
		if ( fraction < 0 || fraction > 1 )
		{
			Debug.LogError ( "Out of range at "+fraction);
		}
		return from + ( to - from ) * fraction;
	}
	
	public static double DLerpFree(double from, double to, double fraction)
	{
		return from + ( to - from ) * fraction;
	}
	
	public static float LerpFree(float from, float to, float fraction)
	{
		return from + ( to - from ) * fraction;
	}

    /*
    /// <summary>
    /// Gets a gui style logging a warning if the style isn't present in the skin
    /// </summary>
    /// <param name="skin"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public static GUIStyle GetStyle(ref GUISkin skin, string styleName)
    {
        GUIStyle foundStyle = null;
        if (skin != null)
        {
            foundStyle = skin.FindStyle(styleName);
            DebugHelper.Warning(foundStyle != null, "Unable to find style with name " + styleName + " in skin " + skin.name);
        }
        else
        {
            Debug.LogError("Unable to find style as skin is null");
        }

        return foundStyle;
    }
    */

	#region Rect

	public static float MidX(this Rect rect)
	{
		return 0.5f * ( rect.xMin + rect.xMax );
	}

	public static float MidY(this Rect rect)
	{
		return 0.5f * ( rect.yMin + rect.yMax );
	}

	public static float MaxDimension(this Rect rect)
	{
		return Mathf.Max ( rect.width, rect.height);
	}

	public static float MinDimension(this Rect rect)
	{
		return Mathf.Min ( rect.width, rect.height);
	}

    public static void Set(this Rect rect, float x, float y, float width, float height)
    {
        rect.x = x;
        rect.y = y;
        rect.width = width;
        rect.height = height;
    }
	#endregion Rect

	/*
    public static void SetRectDimensionsFromNormalTex(ref GUIStyle style, ref Rect rect)
    {
        SetRectDimensionsFromTexture2D(style.normal.background, ref rect);
    }

    public static void SetRectDimensionsFromTexture2D(Texture2D tex, ref Rect rect)
    {
        rect.width = tex.width;
        rect.height = tex.height;
    }*/
}
