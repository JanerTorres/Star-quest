using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaHabitabilidad : MonoBehaviour
{
    // Se va a trabajar con valores arbitrarios iniciales (Ejemplo del sol)
    // Al final se extraen los datos de la API
    //
    // radioMinimo = 0.75f;
    // radioMaximo = 1.765f;
    // radioIce = 2.7f;
    // 1RadioSolar = 0,00465047 UA

    public float radioMinimo = 0.75f;
    public float radioMaximo = 1.765f;
    public float radioIce = 2.7f;

    public float rMinUnity, rMaxUnity, rIceUnity;


    public float distanciaPunto;
    public int size = 1024;

    public Texture2D zonas;
    public Color[] colores;
    public Material matColores;

    public void Start()
    {
        transform.localScale = Vector3.one * 2 * radioIce;

        radioMinimo = ConvertirUA(radioMinimo);
        radioMaximo = ConvertirUA(radioMaximo);
        radioIce = ConvertirUA(radioIce);

        zonas = new Texture2D(1024, 1024);
        colores = new Color[1024 * 1024];

        matColores.mainTexture = zonas;

        CalcularTextura();
    }


    public void CalcularTextura()
    {
        distanciaPunto = 0;
        int constante = size / 2;

        radioIce = 1f * constante;
        radioMaximo = (radioMaximo / radioIce) * constante;
        radioMinimo = (radioMinimo / radioIce) * constante;

        for (int i = 0; i < 1024; i++)
        {
            for (int j = 0; j < 1024; j++)
            {
                distanciaPunto = (new Vector2(i - constante, j - constante)).magnitude;
                
                if(distanciaPunto > radioIce)
                {
                    colores[j + 1024 * i] = new Color(1, 1, 1, 0);
                }
                else if(Mathf.Abs(distanciaPunto - radioIce) < 2)
                {
                    colores[j + 1024 * i] = Color.red;
                }
                else if(distanciaPunto < radioIce)
                {
                    if(distanciaPunto > radioMaximo)
                    {
                        colores[j + 1024 * i] = new Color(1, 1, 1, 0);
                    }
                    else if(distanciaPunto <= radioMaximo)
                    {
                        if(distanciaPunto >= radioMinimo)
                        {
                            colores[j + 1024 * i] = Color.green;
                        }
                        else if(distanciaPunto < radioMinimo)
                        {
                            colores[j + 1024 * i] = new Color(1, 1, 1, 0);
                        }
                    }
                }

            }
        }

        zonas.SetPixels(colores);
        zonas.Apply();
    }

    public static float ConvertirUA(float valorUA)
    {
        // 1 UA = 30f
        return valorUA * 500;
    }
    
}
