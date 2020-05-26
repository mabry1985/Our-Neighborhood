// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class PolyverseSkies : StyledMonoBehaviour
{
    [StyledBanner(0.968f, 0.572f, 0.890f, "Polyverse Skies", "", "https://docs.google.com/document/d/1z7A_xKNa2mXhvTRJqyu-ZQsAtbV32tEZQbO1OmPS_-s/edit?usp=sharing")]
    public bool styledBanner;

    public GameObject sunDirection;
	public GameObject moonDirection;

    [StyledSpace(10)]
    public bool styledSpace0;

    private Vector3 GlobalSunDirection = Vector3.zero;
	private Vector3 GlobalMoonDirection = Vector3.zero;

	void Update ()
    {
		if (sunDirection != null)
        {
			GlobalSunDirection = -sunDirection.transform.forward;
			Shader.SetGlobalVector ("GlobalSunDirection", GlobalSunDirection);
		}
        else
        {
			GlobalSunDirection = Vector3.zero;
			Shader.SetGlobalVector ("GlobalSunDirection", GlobalSunDirection);
		}

		if (moonDirection != null)
        {
			GlobalMoonDirection = -moonDirection.transform.forward;
			Shader.SetGlobalVector ("GlobalMoonDirection", GlobalMoonDirection);
		}
        else
        {
			GlobalSunDirection = Vector3.zero;
			Shader.SetGlobalVector ("GlobalMoonDirection", GlobalMoonDirection);
		}
	}
}
