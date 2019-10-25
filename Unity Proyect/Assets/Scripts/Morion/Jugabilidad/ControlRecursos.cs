using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlRecursos : MonoBehaviour
{
	public static ControlRecursos singleton;
	public float recursoMineral;
	public float recursoGas;
	public float recursoEnergia;
	[Header("Recursos Recolectados")]
	public Text txtE;
	public Text txtG;
	public Text txtM;

	void Awake()
    {
		singleton = this;
    }
	
    void FixedUpdate()
    {
		txtE.text = "Energía: " + recursoEnergia.ToString("0.0");
		txtM.text = "Mineral: " + recursoMineral.ToString("0.0");
		txtG.text = "Gas: " + recursoGas.ToString("0.0");
	}

	public void RestarRecursos(float cE, float cM, float cG)
	{
		recursoEnergia	= Mathf.Clamp(recursoEnergia- cE, 0, 1000000);
		recursoMineral	= Mathf.Clamp(recursoMineral- cM, 0, 1000000);
		recursoGas		= Mathf.Clamp(recursoGas	- cG, 0, 1000000);
	}
}
