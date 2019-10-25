using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonNaveUI : MonoBehaviour
{
	public RecursosPlaneta recursos;
	public string nombre;
	public Text miTexto;

	public void Inicializar(RecursosPlaneta r, string nomb)
	{
		nombre = nomb;
		miTexto.text = nomb;
		recursos = r;
	}
	
	public void SeleccionarPlaneta()
	{
		ModalPlanetas.singleton.SeleccionarPlaneta(recursos);
	}
}
