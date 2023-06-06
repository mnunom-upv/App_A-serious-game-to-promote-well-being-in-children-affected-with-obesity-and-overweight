using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;

public class Funcioes : MonoBehaviour
{
	public Toggle activetePanelInformation;
	public GameObject panelInformation;
	public Toggle activetePanelInformationFin;
	public GameObject panelInformationFin;
	//FUNCION SALIR
	public void Salir(){
		Application.Quit();
		Debug.Log("Fuera del juego");
		//ecuacionSchifield(23,"Hombre",63,167);
	}

	void Update(){
		panelInformation.SetActive(activetePanelInformation.isOn);//Activar el panel de informacion mediante el toggle
		panelInformationFin.SetActive(activetePanelInformationFin.isOn);//Activar el panel de informacion mediante el toggle
		
	}


}
