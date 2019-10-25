using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalPlanetas : MonoBehaviour
{
	public static ModalPlanetas singleton;
	public GameObject imagen;
	public Slider slMineral;
	public Slider slGas;
	public Slider slEnergia;

	public RecursosPlaneta planetaSeleccionado;
	
    void Awake()
    {
		singleton = this;
    }

	
    public void Activar()
	{
		imagen.SetActive(true);
	}

	public void SeleccionarPlaneta(RecursosPlaneta r)
	{
		planetaSeleccionado = r;
		if (planetaSeleccionado==null)
		{
			return;
		}
		ActualizarSliders(planetaSeleccionado.rm, planetaSeleccionado.rg, planetaSeleccionado.re);
		Activar();
	}

	public void Recolectar()
	{
		if (planetaSeleccionado == null)
		{
			return;
		}
		planetaSeleccionado.Recolectar();
		imagen.SetActive(true);
	}

	public void ComprarRecolector()
	{
		if (planetaSeleccionado == null)
		{
			return;
		}
		planetaSeleccionado.AgregarRecolector();
	}

	private void FixedUpdate()
	{
		if (planetaSeleccionado == null)
		{
			return;
		}
		ActualizarSliders(planetaSeleccionado.rm, planetaSeleccionado.rg, planetaSeleccionado.re);
	}

	public void ActualizarSliders(float m, float g, float e)
	{
		slGas.value = g;
		slMineral.value = m;
		slEnergia.value = e;
	}
}
