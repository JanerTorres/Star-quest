using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public string planeta;
    public float mayorAxis;
    public float planetRadio;
    public float excentricity;
    public float roucheLimit; //AQUI
    public string type;
    float period;
    float angularVelocity;
    float focalDistance;
    int primeraVez = 0;
    bool autodestruction;
    float globalRadius;
    
    // Start is called before the first frame update
    public void setParameters(string planeta, float mayorAxis, float excentricity, float planetRadio, Star star, float rocheLimit, string type )
    {
        this.planeta = planeta;
        this.mayorAxis = mayorAxis;
        this.excentricity = excentricity;
        this.period = 2.0f * Mathf.PI * Mathf.Sqrt(Mathf.Pow(mayorAxis, 3) / (4*Mathf.Pow(Mathf.PI,2) * star.masa))*10;
        this.autodestruction = false;
        this.roucheLimit = rocheLimit;
        primeraVez = 1;
        this.type = type;
        this.globalRadius = 0f;
    }

    void Start()
    {
       
    }


    void Update()
    {
        float mayorAxisUnity = this.mayorAxis;
        float distanceToSun = transform.position.magnitude;
        #if UNITY_EDITOR
        angularVelocity = 2 * Mathf.PI / period;
        //   focalDistance = Mathf.Sqrt(Mathf.Pow(mayorAxis, 2) - Mathf.Pow(mayorAxis * excentricity, 2));
        focalDistance = 2 * (mayorAxisUnity * excentricity);
        #endif

        angularVelocity = 2 * Mathf.PI / period;
        float angle = angularVelocity * Time.time;
        if (roucheLimit > distanceToSun)
        {
            this.globalRadius = distanceToSun;
            this.autodestruction = true;
        }
        if (!autodestruction)
        {
            this.globalRadius = (mayorAxisUnity * (1 - Mathf.Pow(excentricity, 2))) / (1 + excentricity * Mathf.Cos(angle));
            float x = this.globalRadius * Mathf.Cos(angle) + focalDistance;
            float z = this.globalRadius * Mathf.Sin(angle);
            transform.position = new Vector3(x, 0, z);
        }
        //float radius = mayorAxis*(1-excentricity*Mathf.Cos(angle));
        Debug.Log(roucheLimit);
        
        if (autodestruction)
        {
            Debug.Log("Entro");
            this.globalRadius = this.globalRadius - 0.05f;
            float x = this.globalRadius * Mathf.Cos(angle);
            float z = this.globalRadius * Mathf.Sin(angle);
            transform.position = new Vector3(x, 0, z);
        }
        

        
        if(globalRadius <= 0)
        {
            Destroy(gameObject);
        }
    }
}
