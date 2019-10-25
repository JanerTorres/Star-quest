using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
	public int			bando;
	public GameObject	objetivo;
	public GameObject	disparo;
	public Transform	cañon;

    void Start()
    {
		bando = transform.parent.gameObject.GetComponent<NaveIA>().bando;
		InvokeRepeating("Disparar", 1, 0.3f);
    }

	public void Disparar()
	{
		if (objetivo!=null)
		{
			GameObject go = Instantiate(disparo, cañon.position, cañon.rotation) as GameObject;
			go.GetComponent<Bala>().CambiarBando(bando);
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		NaveIA nia = other.GetComponent<NaveIA>();
		if (nia!=null)
		{
			if (nia.bando != bando)
			{
				objetivo = other.gameObject;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == objetivo)
		{
			objetivo = null;
		}
	}
}
