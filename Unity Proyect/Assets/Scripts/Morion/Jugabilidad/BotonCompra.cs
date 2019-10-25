using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonCompra : MonoBehaviour
{
	public int precioEnergia;
	public int precioMineral;
	public int precioGas;

	public Button boton;
	public Text txtE;
	public Text txtM;
	public Text txtG;

	void Start()
    {
		txtE.text = precioEnergia.ToString();
		txtG.text = precioMineral.ToString();
		txtM.text = precioGas.ToString();
	}

	public void Comprar()
	{
		ControlRecursos.singleton.RestarRecursos(precioEnergia, precioMineral, precioGas);
	}

    void FixedUpdate()
    {
		boton.interactable = (ControlRecursos.singleton.recursoEnergia >= precioEnergia && ControlRecursos.singleton.recursoGas >= precioGas && ControlRecursos.singleton.recursoMineral >= precioMineral);
    }
}
