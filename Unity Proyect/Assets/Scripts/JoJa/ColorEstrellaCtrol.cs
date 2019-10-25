using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ColorEstrellaCtrol : MonoBehaviour
{

    public ColorEstrella[] colores;
    public Color colorActual;
    public SpriteRenderer[] aros;
    public Material solido;
    public string restUrl;
    public TipoEstrella tipoEstrella;
    public GameObject estrellaProvider;
    public GameObject estrella;
    public int contadorColor;

    public float radioEstrellaUA; // Recibe el radio en UA y luego se convierte a una escala predefinida de unity
    public float radioEstrellaUnity;
    public string letra;

    // Sigleton
    public static ColorEstrellaCtrol singleton;

    private void Awake()
    {
        
        singleton = this;
    }

    private void Start()
    {
        
    }
    public void Inicializar()
    {
        restUrl = "http://10.0.25.76:8080/";
        StartCoroutine(colorRestGet());
        StarProvider estrellaProv = estrellaProvider.GetComponent<StarProvider>();
        estrella = estrellaProv.starList[0];

        radioEstrellaUA = estrella.GetComponent<Star>().radio;
        radioEstrellaUnity = ZonaHabitabilidad.ConvertirUA(radioEstrellaUA);

       // GameObject d = Instantiate(estrella, Vector3.zero, Quaternion.identity) as GameObject; 
       //d.transform.localScale = Vector3.one * 2 * radioEstrellaUnity;
        

    }
    IEnumerator colorRestGet()
    {
        UnityWebRequest api = UnityWebRequest.Get(restUrl);
        yield return api.SendWebRequest();
        if(api.isNetworkError || api.isHttpError)
        {
            Debug.Log(api.error);
        }
        else
        {
            tipoEstrella = JsonUtility.FromJson<TipoEstrella>(@"{""type"": ""G""}");
        }
        tipoEstrella = JsonUtility.FromJson<TipoEstrella>(@"{""type"": ""G""}");
        letra = tipoEstrella.type.ToString();
    }
    public void CambiarColor(string type)
    {
        if (type == "")
        {
            contadorColor = (contadorColor + 1) % 7;
            letra = colores[contadorColor].nombre;
            AplicarColores(colores[contadorColor].color);

        }
        else
        {
            letra = type;
            for (int i = 0; i < colores.Length; i++)
            {
                if (this.letra == colores[i].nombre)
                {
                    colorActual = colores[i].color;
                }
            }
            AplicarColores(colorActual);
        }
        

    }

    public void AplicarColores(Color colorElegido)
    {
        solido.color = colorElegido;
        for (int i = 0; i < aros.Length; i++)
        {
            colorElegido.a = 0.22f;
            aros[i].color = colorElegido;
            
        }
        
    }



}

[System.Serializable]
public class ColorEstrella
{
    public string nombre;
    public Color color;
}

