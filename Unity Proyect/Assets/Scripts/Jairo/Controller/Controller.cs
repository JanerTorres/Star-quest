using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject star;
    public GameObject planet;
    public Text txtPlanetas;
    public int planetsNumber;

    List<Star> starList = new List<Star>();
    // Start is called before the first frame update
    void Start()
    {

        
        //GameObject plo = Instantiate(planet, transform.position, transform.rotation);

    }
    
}
