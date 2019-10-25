using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetModalController : MonoBehaviour
{
    public GameObject planetModal;
    public GameObject planetProvider;
    public InputField nombre;
    public InputField semiEjeMayor;
    public InputField excentricidad;
    public InputField radius;
    public InputField mass;
    public Text contadorPlanetas;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void createPlanetButton()
    {
        PlanetProvider planetProv = planetProvider.GetComponent<PlanetProvider>();
        planetProv.addPlanet(nombre.text, float.Parse(semiEjeMayor.text), float.Parse(excentricidad.text), float.Parse(radius.text), float.Parse(mass.text));
        int auxCuentaPlanetas = int.Parse(contadorPlanetas.text) + 1;
        contadorPlanetas.text = ""+ auxCuentaPlanetas;
        resetForm();
        planetModal.active = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void resetForm()
    {
        nombre.text = "";
        semiEjeMayor.text = "";
        excentricidad.text = "";
        radius.text = "";
        mass.text = "";
        Debug.Log("Entro");
    }
}
