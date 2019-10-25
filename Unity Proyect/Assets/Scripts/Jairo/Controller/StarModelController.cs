using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarModelController : MonoBehaviour
{
    public GameObject starModal;
    public GameObject starProvider;
    public GameObject menu;
    public InputField name;
    public InputField mass;
    public InputField radius;
    public InputField age;
    public static StarModelController singleton;
	public GameObject InterJuego;
    // Start is called before the first frame update
    public void Awake()
    {
        singleton = this;
    }

    public void createStarButton()
    {
        StarProvider starProv = starProvider.GetComponent<StarProvider>();
        starProv.AddStar(this.name.text, float.Parse(this.mass.text), float.Parse(this.radius.text));

        //ColorEstrellaCtrol.singleton.Inicializar();

        resetForm();
        menu.active = true;
		InterJuego.SetActive(true);
        starModal.active = false;
    }
    private void resetForm()
    {
        name.text = "";
        mass.text = "";
        radius.text = "";
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void turnON()
    {
        menu.active = false;
        starModal.active = true;
    }
}
