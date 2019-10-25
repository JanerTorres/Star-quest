using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotador : MonoBehaviour
{
	public Vector3 velocidad;
    // Start is called before the first frame update
    public void Random()
    {
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(velocidad * Time.deltaTime);
    }
}
