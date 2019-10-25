using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public string name;
    public float masa;
    public float radio;
    public string type;
    public float t_eff;
    public float per_main_sec;
    public float lum_s;
    public void initStar(string name, float masa, float radio, string type)
    {
        this.name = name;
        this.masa = masa;
        this.radio = radio;
        this.type = type;
  
    }
    void Start()
    {
       
    }
    void Update()
    {

    }

}
