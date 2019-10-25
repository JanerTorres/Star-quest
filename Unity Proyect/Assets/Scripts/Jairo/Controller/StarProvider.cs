using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StarProvider : MonoBehaviour
{
    public GameObject star;
    public List<GameObject> starList = new List<GameObject>();
    public GameObject starModal;
    public string restUrl;
    string tipoEstrella;
    float masa;
    float radio;
    string name;
    TipoEstrella dataEstrella;
    public bool valid;
    
    // Start is called before the first frame update
    void Start()
    {
        restUrl = "http://192.168.43.124:8080";
        tipoEstrella = "";
        valid = true;
    }
    public void AddStar(string name, float masa, float radio)
    {
        this.name = name;
        this.radio = radio;
        this.masa = masa;

        StartCoroutine(dataStarRestGet());

    }

    IEnumerator dataStarRestGet()
    {
        UnityWebRequest api = UnityWebRequest.Get(restUrl+"/stardata/"+this.masa.ToString()+"&"+this.radio.ToString());
        yield return api.SendWebRequest();
        if (api.isNetworkError || api.isHttpError)
        {
            Debug.Log(api.error);
        }
        else
        {
            this.dataEstrella = JsonUtility.FromJson<TipoEstrella>(api.downloadHandler.text);

        }
        if(dataEstrella.valid == 1)
        {
            float radio1 = radio * 0.2f;

            GameObject starGO = Instantiate(star, Vector3.zero, Quaternion.identity);
            Star star1 = starGO.GetComponent<Star>();

            star1.transform.localScale = Vector3.one * 2 * radio1;
            star1.initStar(name, masa, radio, dataEstrella.type);
            star1.t_eff = dataEstrella.T_eff;
            star1.per_main_sec = dataEstrella.per_main_sec;
            star1.lum_s = dataEstrella.lum_s;
            ColorEstrellaCtrol.singleton.CambiarColor("G");
            starList.Add(starGO);

            this.valid = true;

        }
        else
        {
            starModal.GetComponent<StarModelController>().turnON();
            this.valid = false;
        }
        yield return this.valid;
    }
    // Update is called once per frame
    public GameObject uniqueStar()
    {
        return starList[0];
    }
    void Update()
    {
        
    }
}
[System.Serializable]
public class TipoEstrella
{
    public float T_eff;
    public float lum_s;
    public string type;
    public float per_main_sec;
    public string color;
    public int valid;
    public float hab_zone_min_radius;
    public float hab_zone_max_radius;
    public float ice_zone;
}
