using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    public InputField inp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ReceiveData(float numero)
    {
        inp.text = numero.ToString();
    }
   
}
