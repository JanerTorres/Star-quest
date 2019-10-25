using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
	public float velocidad;
	public float daño = 10;
	public int bando;
	public Gradient[] gradientes;
	public TrailRenderer lr;


	public void CambiarBando(int b)
	{
		bando = b;
		lr.colorGradient = gradientes[b];
	}

    void Start()
    {
		Destroy(gameObject, 3);
    }

	private void Update()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
	}

	private void OnTriggerEnter(Collider other)
	{
		NaveIA nia = other.GetComponent<NaveIA>();
		if (nia != null)
		{
			if (nia.bando != bando)
			{
				nia.CausarDaño(daño);
			}
		}
	}
}
