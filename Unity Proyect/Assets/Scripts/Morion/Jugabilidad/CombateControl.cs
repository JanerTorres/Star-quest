using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateControl : MonoBehaviour
{
	public static CombateControl singleton;
	public GameObject[] naves;
	

	void Awake()
    {
		singleton = this;
    }

}
