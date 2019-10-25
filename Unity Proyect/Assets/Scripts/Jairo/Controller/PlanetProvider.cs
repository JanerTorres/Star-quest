using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlanetProvider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject planet;
    public GameObject starProvider;
    List<GameObject> planetList = new List<GameObject>();
    float upperRockLimit;
    string restUrl;
    string planeta;
    float mayorAxis;
    float excentricity;
    float planetRadio;
    float planetMass;
    float solarTemp;
    float solarLum;
    float solarMass;
	public Transform contenedor;
	public GameObject botonPlaneta;
    void Start()
    {
        restUrl = "http://192.168.43.124:8080";
        this.solarMass = starProvider.GetComponent<StarProvider>().uniqueStar().GetComponent<Star>().masa;
        this.solarTemp = starProvider.GetComponent<StarProvider>().uniqueStar().GetComponent<Star>().t_eff;
        this.solarLum = starProvider.GetComponent<StarProvider>().uniqueStar().GetComponent<Star>().lum_s;
        upperRockLimit = 2.7f * Mathf.Pow(solarMass, 2);
    }
    public void addPlanet(string planeta, float mayorAxis, float excentricity, float planetRadio, float planetMass)
    {
        
        this.planeta = planeta;
        this.mayorAxis = mayorAxis;
        this.excentricity = excentricity;
        this.planetRadio = planetRadio;
        this.planetMass = planetMass;
        StartCoroutine(dataPlanetRestGet());

    }
	IEnumerator dataPlanetRestGet()
	{
		PlanetType dataPlanet = null;
		UnityWebRequest api = UnityWebRequest.Get(restUrl + "/planettype/" + this.planetMass.ToString() + "&" + this.planetRadio.ToString() + "&" + this.solarTemp.ToString()
			+ "&" + this.solarTemp.ToString() + "&" + this.solarMass.ToString() + "&" + this.mayorAxis);
		yield return api.SendWebRequest();
		if (api.isNetworkError || api.isHttpError)
		{
			Debug.Log(api.error);
		}
		else
		{
			dataPlanet = JsonUtility.FromJson<PlanetType>(api.downloadHandler.text);

		}
		float solarMass = starProvider.GetComponent<StarProvider>().uniqueStar().GetComponent<Star>().masa;
		float starRadius = starProvider.GetComponent<StarProvider>().uniqueStar().GetComponent<Star>().radio;
		float rocheLimit = starRadius * 0.0046f * Mathf.Pow((2.0f * bodyDensity(solarMass * 1.989f * Mathf.Pow(10, 30), starRadius * 695510000f)) / bodyDensity(planetMass * 5.972f * Mathf.Pow(10, 24), planetRadio * 6371000), 1 / 3f);
		GameObject planetNewGO = Instantiate(planet, transform.position, Quaternion.Euler(-90, 0, 0));
		GameObject botonNewGO = Instantiate(botonPlaneta, contenedor) as GameObject;
		botonNewGO.GetComponent<BotonNaveUI>().Inicializar(planetNewGO.GetComponent<RecursosPlaneta>(),this.planeta);
        float radioUnity = (1 + (0.1f * planetRadio)) * 0.05f;
        Planet planetNew = planetNewGO.GetComponent<Planet>();
        planetNew.transform.localScale = Vector3.one * 2 * radioUnity;
        RandomMaterial randomPlanet = planetNewGO.GetComponent<RandomMaterial>();
        string type = dataPlanet.type;

		/* switch (type)
		 {
			 case "Gaseoso":
				 randomPlanet.tipo = TipoPlaneta.gaseoso;
				 break;
			 case "Habitable":
				 randomPlanet.tipo = TipoPlaneta.habitable;
				 break;
			 case "Solido":
				 randomPlanet.tipo = TipoPlaneta.solido;
				 break;
			 default:
				 randomPlanet.tipo = TipoPlaneta.luna;
				 break;
		 }*/
		print("TIPO: " + type);
		randomPlanet.AsignarTipo(type);
		randomPlanet.Generar();

        StarProvider starprov = starProvider.GetComponent<StarProvider>();
        Star star = starprov.uniqueStar().GetComponent<Star>();
        planetNew.setParameters(planeta, mayorAxis, excentricity, planetRadio, star, rocheLimit, type);
        planetList.Add(planetNewGO);
    }

    public float bodyDensity(float mass, float radius)
    {
        return (3.0f / 4.0f ) * (mass / (Mathf.PI * Mathf.Pow(radius, 3f)));
    }
    void Update()
    {
        
    }
}
[System.Serializable]
public class PlanetType
{
    public string type;
}

