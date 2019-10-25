using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomMaterial : MonoBehaviour
{
	public string[] sss;
	public string ss;
	public TipoPlaneta tipo;
	public Gradient gradienteBN;
	public Material material;
	//public RawImage imagen;
	public float[,] forma = new float[512,512];
	public bool hacerPolo;
	[Header("Habitables")]
	[Range(0, 1)]
	public float probabilidadTierra;
	[Range(0, 1)]
	public float escala = 0.3f;
	public Gradient[] coloresGaseosos;
	public Gradient[] coloresSolidos;
	public Gradient[] coloresLunas;
	public Gradient[] coloresHabitables;

	public Texture2D[] patronesGaseosos;
	public Texture2D[] patronesSolidos;
	public Texture2D[] patronesNubes;
	public Texture[] normalesPlanetas;
	public Texture[] normalesLunas;
	public Texture2D[] polos;
	Texture2D texture;

	float pDef;

	private void Start()
	{
		pDef = probabilidadTierra;
		material = new Material(material);
        GetComponent<Rotador>().velocidad = Vector3.forward * Random.Range(-90, 90);
		GetComponent<MeshRenderer>().material = material;
		//Generar();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)||false)
		{
			ss = sss[Random.Range(0, sss.Length)];
			AsignarTipo(ss);
			Generar();
		}
	}

	public void AsignarTipo(string t)
	{
		if (t[0] == '0')
		{
			tipo = TipoPlaneta.solido;
			int a = int.Parse(t[1] + "");
			gradienteBN = coloresSolidos[Random.Range(3*a, 3*a+3)];
		}else if (t[0] == '1')
		{
			tipo = TipoPlaneta.habitable;
			int a = int.Parse(t[1] + "");
			probabilidadTierra = (a + 0f) * pDef;
			material.SetTexture("_BumpMap", null);
			hacerPolo = (Random.Range(0f, 1f) > 0.5f);
		}
		else if (t[0] == '2')
		{
			tipo = TipoPlaneta.gaseoso;
		}
		else if (t[0] == '3')
		{
			tipo = TipoPlaneta.luna;
		}
	}

	public void Generar()
	{
		texture = new Texture2D(512, 512);
		//imagen.texture = texture;
		material.mainTexture = texture;
		GetComponent<RecursosPlaneta>().AsignarTipo(tipo);

		VolverBlanco();
		switch (tipo)
		{
			case TipoPlaneta.gaseoso:
				gradienteBN = coloresGaseosos[Random.Range(0, coloresGaseosos.Length)];
				material.SetTexture("_BumpMap", null);
				EstiloGaseoso();
				break;
			case TipoPlaneta.solido:
				//gradienteBN = coloresSolidos[Random.Range(0, coloresSolidos.Length)];
				material.SetTexture("_BumpMap", normalesPlanetas[Random.Range(0, normalesPlanetas.Length)]);
				EstiloSolido();
				if (hacerPolo)
				{
					GenerarPolos();
				}
				break;
			case TipoPlaneta.habitable:
				gradienteBN = coloresHabitables[Random.Range(0, coloresHabitables.Length)];
				EstiloHabitable();
				if (hacerPolo)
				{
					GenerarPolos();
				}
				break;
			default:
				break;
			case TipoPlaneta.luna:
				gradienteBN = coloresLunas[Random.Range(0, coloresLunas.Length)];
				material.SetTexture("_BumpMap", normalesLunas[Random.Range(0, normalesLunas.Length)]);
				EstiloSolido();
				break;
		}
		

		for (int x = 0; x < texture.height; x++)
		{
			for (int y = 0; y < texture.width; y++)
			{
				Color color = gradienteBN.Evaluate(forma[x,y]);
				texture.SetPixel(x, y, color);
			}
		}

		switch (tipo)
		{
			case TipoPlaneta.gaseoso:
				break;
			case TipoPlaneta.solido:
				if (hacerPolo)
				{
					GenerarPolos();
				}
				break;
			case TipoPlaneta.habitable:
				MultiplicarTextura(patronesSolidos[Random.Range(0, patronesSolidos.Length)]);
				if (hacerPolo)
				{
					GenerarPolos();
				}
				GenerarNubes();
				break;
			case TipoPlaneta.luna:
				break;
			default:
				break;
		}
		texture.Apply();
	}

	public void EstiloGaseoso()
	{
		///////////////// Generar las líneas horizontales
		int lineas = Random.Range(7,30);
		List<int> relineas = new List<int>();

		for (int i = 0; i < lineas; i++)
		{
			relineas.Add (Random.Range(0, 502));
		}

		relineas.Sort();
		
		for (int ind = 0; ind < lineas-1; ind++)
		{
			float intensidad = Random.Range(0f, 0.7f);
			if (Random.Range(0f,1f)>0.1f)
			{
				for (int i = relineas[ind]; i < relineas[ind+1]; i++)
				{
					for (int j = 0; j < 512; j++)
					{
						forma[j, i] = intensidad;
					}
				}
			}
			else
			{
				for (int j = 0; j < 512; j++)
				{
					forma[j, relineas[ind]] = intensidad;
				}
			}
		}

		///////////////// Suavizar el resultado
		int nSua = Random.Range(2, 5);
		for (int i = 0; i < nSua; i++)
		{
			SmootHorizontal();
			SmootVertical();	
		}

		///////////////// Generar las ruido vertical
		Color[] c = patronesGaseosos[Random.Range(0, patronesGaseosos.Length)].GetPixels();
		for (int i = 0; i < 512; i++)
		{
			for (int j = 0; j < 512; j++)
			{
				forma[j, i] = forma[j, i] * c[(i * 512 + j)].r;
			}
		}
	}

	public void EstiloSolido()
	{
		Color[] c = patronesSolidos[Random.Range(0, patronesSolidos.Length)].GetPixels();
		Color[] c2 = patronesSolidos[Random.Range(0, patronesSolidos.Length)].GetPixels();
		for (int i = 0; i < 512; i++)
		{
			for (int j = 0; j < 512; j++)
			{
				forma[j, i] = (c2[(i * 512 + j)].r * c[(i * 512 + j)].r);
			}
		}
	}

	public void EstiloHabitable()
	{
		int mapheight = 512;
		int mapwidth = 512;

		Vector2 shift = new Vector2(0, 0); // play with this to shift map around
		float zoom = 0.1f; // play with this to zoom into the noise field

		
	    float[,] forma2 = new float[512,512];
		float t = Time.time * Random.Range(10f,100f);
		Color[] c = patronesSolidos[Random.Range(0, patronesSolidos.Length)].GetPixels();
		Color[] c2 = patronesSolidos[Random.Range(0, patronesSolidos.Length)].GetPixels();

		for (int x = 0; x < mapwidth; x++)
		{
			for (int y = 0; y < mapheight; y++)
			{
				Vector2 pos = zoom * (new Vector2(x, y)) + shift;
				float noise = Mathf.PerlinNoise(pos.x*escala+t, pos.y * escala + t);
				if (noise<probabilidadTierra)
				{
					noise = 1;
				}
				else
				{
					noise = 0;
				}
				forma[x, y] = noise;
			}
		}

		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 512; j++)
			{

				forma2[i, j] = forma[i * 2, j];
				forma2[511-i, j] = forma[i * 2, j];
			}
		}

		forma = forma2;
		for (int i = 0; i < 10; i++)
		{

			SmootHorizontal();
			SmootVertical();
		}


	}

	public void GenerarNubes()
	{
		SumarTextura(patronesNubes[Random.Range(0, patronesNubes.Length)]);
	}

	public void GenerarPolos()
	{
		Color[] c2 = polos[Random.Range(0, polos.Length)].GetPixels();
		/*
		for (int i = 0; i < 512; i++)
		{
			for (int j = 0; j < 512; j++)
			{
				forma[j, i] = Mathf.Clamp(c2[(i * 512 + j)].r + forma[j, i],0,1);
			}
		}
		*/
		SumarTextura(polos[Random.Range(0, polos.Length)]);
	}

	public void VolverBlanco()
	{
		for (int i = 0; i < 512; i++)
		{
			for (int j = 0; j < 512; j++)
			{
				forma[i, j] = 1;
			}
		}
	}

	public void SmootHorizontal()
	{
		for (int i = 3; i < 509; i++)
		{
			for (int j = 0; j < 512; j++)
			{
				forma[j, i] = (forma[j, i - 3] + forma[j, i - 2] + forma[j, i - 1] + forma[j, i] + forma[j, i + 3] + forma[j, i + 2] + forma[j, i + 1]) / 7f;
			}
		}
	}


	public void SmootVertical()
	{
		for (int i = 0; i < 512; i++)
		{
			for (int j = 3; j < 509; j++)
			{
				forma[i, j] = (forma[i, j - 3] + forma[i, j - 2] + forma[i, j - 1] + forma[i, j] + forma[i, j + 3] + forma[i, j + 2] + forma[i, j + 1]) / 7f;
			}
		}
	}

	void MultiplicarTextura(Texture2D t)
	{
		Color[] cc = t.GetPixels();
		for (int i = 0; i < 512; i++)
		{
			for (int j = 3; j < 509; j++)
			{
				texture.SetPixel(i, j, texture.GetPixel(i, j) * t.GetPixel(i, j));
			}
		}
	}

	void SumarTextura(Texture2D t)
	{
		Color[] cc = t.GetPixels();
		for (int i = 0; i < 512; i++)
		{
			for (int j = 3; j < 509; j++)
			{
				texture.SetPixel(i, j, texture.GetPixel(i, j) + t.GetPixel(i, j));
			}
		}
	}
}

public enum TipoPlaneta
{
	gaseoso = 0,
	solido = 1,
	habitable = 2,
	luna = 3
}