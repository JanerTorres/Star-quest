using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursosPlaneta : MonoBehaviour
{
	[Header("Recursos del Planeta")]
	public float recursoMineral;
	public float recursoGas;
	public float recursoEnergía;
	[Header("Recolectores")]
	public int recolectores;
	[Header("Recursos Recolectados")]
	public float rm;
	public float rg;
	public float re;

	public void AsignarTipo(TipoPlaneta t)
	{
		recursoEnergía = Random.Range(0.2f, 0.4f);
		recursoGas = Random.Range(0.2f, 0.4f);
		recursoMineral = Random.Range(0.2f, 0.4f);
		switch (t)
		{
			case TipoPlaneta.gaseoso:
				recursoGas = Random.Range(0.5f, 0.9f);
				break;
			case TipoPlaneta.solido:
				recursoMineral = Random.Range(0.5f, 0.9f);
				break;
			case TipoPlaneta.habitable:
				recursoEnergía = Random.Range(0.5f, 0.9f);
				break;
			case TipoPlaneta.luna:
				break;
			default:
				break;
		}
	}
	void Update()
    {
		re += recursoEnergía * recolectores * Time.deltaTime;
		rg += recursoGas * recolectores * Time.deltaTime;
		rm += recursoMineral * recolectores * Time.deltaTime;

		re = Mathf.Clamp(re, 0f, 100f);
		rg = Mathf.Clamp(rg, 0f, 100f);
		rm = Mathf.Clamp(rm, 0f, 100f);
	}

	public void Recolectar()
	{
		ControlRecursos.singleton.recursoEnergia	+= re;
		ControlRecursos.singleton.recursoGas		+= rg;
		ControlRecursos.singleton.recursoMineral	+= rm;
		rm = 0;
		rg = 0;
		re = 0;
	}

	private void OnMouseUp()
	{
		//ModalPlanetas.singleton.SeleccionarPlaneta(this);
	}

	public void AgregarRecolector()
	{
		recolectores++;
	}
}
