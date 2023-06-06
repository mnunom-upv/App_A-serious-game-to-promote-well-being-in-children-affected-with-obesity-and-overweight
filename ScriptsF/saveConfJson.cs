using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using System;
using EasyUI.Toast;
using ClassConfig;
using classAlimenots;
using FuncionesNms;
//using Random;

public class saveConfJson : MonoBehaviour
{
	//VARIABLES PARA SAVE CONF SIMULATION
	public Dropdown selectEjercicio;//select ejercicio
	public Dropdown selectEcuacion;//selecct Ecuacion
	public Dropdown selectComidasDia;//select comidas dia 
	public InputField textPeriodo;
	public Dropdown selectPeriodoTiempo;//select cperiodo(dias, meses, sem)

	public float imc;//imc
	public Text imcText;
	public Text getText;

	//VARIABES PARA SAVE CONF AVATAR
	//public Slider edadSlider;
	public Slider pesoSlider;
	public Slider estaturaSlider;
	public Toggle hombre;
	public Toggle mujer;
	public Dropdown edadSelect;
	public Dropdown mesesSelect;

  	//mensajes de error
  	public GameObject panelError;
  	public GameObject panelConfirmation;
  	public Text textError;
	public List<Configuracion> data;
	public List<classAlimentos> listData;



	//value text de la vista de simulacion
	public Text valuePeso;
	public Text valueIMC;
	public Text valueEstatura;
	public Text valueEdad;
	public Text valueGET;
	public Text valueAF;
	//Vista de config avatar
	public Text valuePesoConfAvatar;
    // Start is called before the first frame update


	//Game object del prefab para el listado de alimentos
	public Transform panelContenedor;
	public GameObject prefab;
   
	public Funciones ins;

	void Awake(){
      //Se ejecuta antes del start
	  
	  Funciones inst = (new GameObject("SomeObjName")).AddComponent<Funciones>();
	  inst.inicalizarDBAlimentos();	  
		  
	  

    }

    void Start()
    {
		TraerDatos();
		Debug.Log("Que comience la machaca");
		mostrarListaAlimentos();	    
    }







	
	public void mostrarListaAlimentos(){
		//leer json
		int i= 0 ;
		listData =  new List<classAlimentos>();
		TextAsset json = (TextAsset)Resources.Load("dbAlimentosAux");
      	listData = JsonConvert.DeserializeObject<List<classAlimentos>>(json.ToString()); 	

		//instanciar panel
			foreach(classAlimentos dat in listData){
				GameObject inst  =  Instantiate(prefab,panelContenedor);
				//if(i==0)inst.GetComponent<RectTransform>().anchoredPosition =  new Vector2(0, i * -1200f);
				inst.GetComponent<RectTransform>().anchoredPosition =  new Vector2(0, (i+1) * -200f);
     			panelContenedor.GetChild(i).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = dat.name;
     			panelContenedor.GetChild(i).GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = ""+dat.proteinas;
                panelContenedor.GetComponent<RectTransform>().offsetMin =  new Vector2(0, i * -600f);//cambiar el tam del panel que contiene el texto
                //tabla.GetComponent<RectTransform>().sizeDelta =  new Vector2(0, i * -70f);
                //Al seleccionar
                //tabla.GetChild(i).GetComponent<Button>().onClick.AddListener(() => OnSelect());
                //prefab.GetChild(i).GetChild(2).GetComponent<Button>().onClick.AddListener(() => OnSelect(level.name,level.description));
                //Debug.Log("Nombre: "+tabla.GetChild(i).GetChild(2).GetComponent<Button>().name);
				i = i+1;			
			}
	} 
	
	///Aumento calorico
	public void aumentoCalorico(){
		Toast.Show("Chingos de calorias",2f,ToastColor.Black);
	}

	///decremento calorico
	public void decrementoCalorico(){
		Toast.Show("Pocas Calorias",2f,ToastColor.Black);
	}	

	public void idiomaEsp(){
		//Cambio de colores
		GameObject.Find("idiomaEsp").GetComponent<Image>().color = new Color32( 63, 144, 226, 255 );
		GameObject.Find("idiomaIngles").GetComponent<Image>().color = new Color32( 255,255,255, 255);
		Toast.Show("Idioma Español Activado",2f,ToastColor.Black);				

		///Guardar idioma
		Funciones instFun =new Funciones();
      	data =  instFun.datosDeConfiguracion(); 
        
		//Asigna los valores de unity al script de configuracion       	   
       	data[0].idioma = "español";  
		instFun.guardarDatosConfiguración(data);
		TraerDatos();		
	}

	public void idiomaIngl(){
		//Cambio de colores
		GameObject.Find("idiomaEsp").GetComponent<Image>().color = new Color32( 255,255,255, 255);
		GameObject.Find("idiomaIngles").GetComponent<Image>().color = new Color32( 63, 144, 226, 255 );
		Toast.Show("Lenguage english activate",2f,ToastColor.Black);

		///Guardar idioma
      	Funciones instFun =new Funciones();
      	data =  instFun.datosDeConfiguracion(); 
        
		//Asigna los valores de unity al script de configuracion       	   
       	data[0].idioma = "ingles";  
		instFun.guardarDatosConfiguración(data);	
		TraerDatos();		
	}






	public void valuesVistaConfAvatar(float pesoSld, float estaturaSld, int edad, int meses,double peso){
		pesoSlider.value = pesoSld;
		estaturaSlider.value = estaturaSld;
		edadSelect.value = edad;
		mesesSelect.value = meses;
		valuePesoConfAvatar.text = (float)Math.Round(peso,1,MidpointRounding.ToEven)+" Kg";		
		//Falta el sexo
	}

	public void valueVistaConfSimulation(int tipoAf, int ecuacion, int comidas_dia,int periodo_tiempo, int periodo){
		selectEcuacion.value = ecuacion;//selecct Ecuacion
		selectComidasDia.value = comidas_dia ;
		selectPeriodoTiempo.value = periodo_tiempo;
		textPeriodo.text= periodo+"";
		selectEjercicio.value = tipoAf;//select ejercicio
	}

	public void textsVistaSimulacion(int tAF,string peso,string imc, string estatura, string edad, string GET, string idioma){
		string[] AF;
		valuePeso.text = peso+" kg";
		valueIMC.text = imc;
		valueEstatura.text = estatura+" mts";
		if(idioma == "español"){
			valueEdad.text = edad+ " años";
			AF =  new string[5]{"Sedentario","Poco Activo","Medio Activo","Muy Activo","Hiperactivo"};
		}else{
			AF =  new string[5]{"Sedentary", "Little Active", "Medium Active", "Very Active", "Hyperactive"};
			valueEdad.text = edad+ " years";
		}
		valueGET.text = GET;	
		valueAF.text = AF[tAF];
		
		
	}	
    public void TraerDatos(){
		//traer data json
		Debug.Log("Se traer datos");
    	Funciones instFun =new Funciones();      	
      	data =  instFun.datosDeConfiguracion(); 
     	//Asignar a variables 		
     	valueVistaConfSimulation(data[0].tipo_ejercicio,data[0].Ecuacion,data[0].Comidas_dia,data[0].periodo_tiempo,data[0].periodo);								
		valuesVistaConfAvatar(data[0].pesoSlider,data[0].EstaturaSlider,data[0].Edad,data[0].Meses,data[0].Peso);								
		textsVistaSimulacion(data[0].tipo_ejercicio,data[0].Peso+"",""+data[0].IMC, data[0].Estatura+"", ((int)data[0].Edad+7)+"", ""+data[0].GET,data[0].idioma);	
		
    }

  


    //fun save conf simulation en el json
   	public void guardarConfiguracionSimulationJson(){
		string pValue = textPeriodo.text;   		
		Funciones inst = new Funciones();
		//traer data json
    	data =  inst.datosDeConfiguracion();

   		data[0].Ecuacion = selectEcuacion.value;//ecuacion
   		data[0].tipo_ejercicio = selectEjercicio.value;//tipo ejercicio
   		data[0].Comidas_dia= selectComidasDia.value;//comidad dia
   		Debug.Log(pValue);
   		data[0].periodo = Int32.Parse(pValue);
   		data[0].periodo_tiempo= selectPeriodoTiempo.value;//Periodo tiempo(diad, meses, semena)
		data[0].idioma = data[0].idioma;
      	
     	//C:\Users\Crist\Desktop\UNITYPROJECTS\Child - copia (4)\Assets\Resources     	
		inst.guardarDatosConfiguración(data);
		TraerDatos();
		panelConfirmation.SetActive(true);		              

   	}   





}
//Link de pag en lq muestra como write json flile https://answers.unity.com/questions/1518958/write-to-json-file-on-android.html
//ubicacion appdata windows C:\Users\Crist\AppData\LocalLow\DefaultCompany\Child