using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlNaves : MonoBehaviour
{
	public static ControlNaves singleton;
	public int[] naves;

	public Text[] txtNaves;

	private void Awake()
	{
		singleton = this;
	}

	public void SumarNave(int n)
	{
		naves[n] += 1;
		txtNaves[n].text = naves[n].ToString("00");
		PlayerPrefs.SetInt("J1" + n, naves[n]);
		PlayerPrefs.SetInt("J2" + n, naves[n]);
	}
}
