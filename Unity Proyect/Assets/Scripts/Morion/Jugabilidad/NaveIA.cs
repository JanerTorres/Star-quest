using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveIA : MonoBehaviour
{
	public float velocidad;
	public float velRotacion;
	public Transform objetivo;
	public float rangoEnemigos=16;
	public int bando;
	public float vida = 100;
	public MeshRenderer miMaya;
	public Material[] materiales;

    void Start()
    {
		CambiarRotacionRandom();
		InvokeRepeating("CambiarRotacionRandom", Random.Range(2f, 12f), Random.Range(6f, 14f));
		InvokeRepeating("MirarEnemigo", Random.Range(1f, 10f), Random.Range(2f,5f));
	}
	
    void Update()
    {
		transform.Translate(Vector3.forward * Time.deltaTime * velocidad);
		Quaternion q = transform.rotation;
		transform.LookAt(objetivo);
		transform.rotation = Quaternion.Lerp(q, transform.rotation, velRotacion * Time.deltaTime);

		if (objetivo != null && (transform.position - objetivo.position).sqrMagnitude<3)
		{
			CambiarRotacionRandom();
		}
    }

	public void CambiarBando(int b)
	{
		bando = b;
		miMaya.material = materiales[b];
	}

	public void CausarDaño(float c)
	{
		vida -= c;
		if (vida <= 0)
		{
			ControlBatalla.singleton.LimpiarListas();
			Destroy(gameObject);
		}
	}

	void MirarEnemigo()
	{
		Collider[] cols = Physics.OverlapSphere(transform.position, rangoEnemigos);
		for (int i = 0; i < cols.Length; i++)
		{
			NaveIA nia = cols[i].gameObject.GetComponent<NaveIA>();
			if (nia!=null)
			{
				if (nia.bando != bando)
				{
					objetivo = cols[i].transform;
					return;
				}
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, rangoEnemigos);
	}

	void CambiarRotacionRandom()
	{
		objetivo = ControlBatalla.singleton.GetPuntoAleatorio();
	}
}
