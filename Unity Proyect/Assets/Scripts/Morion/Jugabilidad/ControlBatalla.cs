using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlBatalla : MonoBehaviour
{
	public static ControlBatalla singleton;
	public GameObject[] naves;
	public Transform[] puntos;

	public int[] navesJ1;
	public int[] navesJ2;

	public List<NaveIA> navesJugador1;
	public List<NaveIA> navesJugador2;

	public Transform zonaNavesJ1;
	public Transform zonaNavesJ2;
	public Vector2 rangoVariacionCreaNaves;

	public Text txtNaves1;
	public Text txtNaves2;

	public GameObject imGanar;
	public GameObject imPerder;


	void Awake()
    {
		singleton = this;
	}

	void Start()
	{
		navesJ1[0] = PlayerPrefs.GetInt("J10", 4);
		navesJ1[1] = PlayerPrefs.GetInt("J11", 4);
		navesJ1[2] = PlayerPrefs.GetInt("J12", 4);

		navesJ2[0] = PlayerPrefs.GetInt("J20", 4);
		navesJ2[1] = PlayerPrefs.GetInt("J21", 4);
		navesJ2[2] = PlayerPrefs.GetInt("J22", 4);

		Jugar();
	}

	private void FixedUpdate()
	{
		if(txtNaves1 != null) txtNaves1.text = "Naves: " + navesJugador1.Count.ToString("00");
		if (txtNaves2 != null) txtNaves2.text = "Naves: " + navesJugador2.Count.ToString("00");
	}

	public void LimpiarListas()
	{
		Invoke("LimpiarAhoraSi", 0.5f);
	}

	void LimpiarAhoraSi()
	{
		for (int i = navesJugador1.Count - 1; i >= 0; i--)
		{
			if (navesJugador1[i] == null)
			{
				navesJugador1.RemoveAt(i);
			}
		}
		for (int i = navesJugador2.Count - 1; i >= 0; i--)
		{
			if (navesJugador2[i] == null)
			{
				navesJugador2.RemoveAt(i);
			}
		}
		if (navesJugador1.Count==0)
		{
			imPerder.SetActive(true);
		}
		if (navesJugador2.Count == 0)
		{
			imGanar.SetActive(true);
		}
	}

	public Transform GetPuntoAleatorio()
	{
		return (puntos[Random.Range(0, puntos.Length)]);
	}

	void Jugar()
	{
		StartCoroutine(CoJugar1());
		StartCoroutine(CoJugar2());
	}

	public IEnumerator CoJugar1()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < navesJ1[i]; j++)
			{
				Vector3 offset = new Vector3(Random.Range(rangoVariacionCreaNaves.x, rangoVariacionCreaNaves.y), Random.Range(rangoVariacionCreaNaves.x, rangoVariacionCreaNaves.y), Random.Range(rangoVariacionCreaNaves.x, rangoVariacionCreaNaves.y));
				GameObject g = (Instantiate(naves[i], zonaNavesJ1.position + offset, zonaNavesJ1.rotation) as GameObject);
				g.GetComponent<NaveIA>().CambiarBando(0);
				navesJugador1.Add(g.GetComponent<NaveIA>());
				yield return new WaitForSeconds(0.5f);
			}

		}
	}
	public IEnumerator CoJugar2()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < navesJ2[i]; j++)
			{
				Vector3 offset = new Vector3(Random.Range(rangoVariacionCreaNaves.x, rangoVariacionCreaNaves.y), Random.Range(rangoVariacionCreaNaves.x, rangoVariacionCreaNaves.y), Random.Range(rangoVariacionCreaNaves.x, rangoVariacionCreaNaves.y));
				GameObject g = (Instantiate(naves[i], zonaNavesJ2.position + offset, zonaNavesJ2.rotation) as GameObject);
				g.GetComponent<NaveIA>().CambiarBando(1);
				navesJugador2.Add(g.GetComponent<NaveIA>());
				yield return new WaitForSeconds(0.5f);
			}

		}
	}
}
