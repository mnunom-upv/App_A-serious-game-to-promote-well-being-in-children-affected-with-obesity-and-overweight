using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UnityEngine.UI;
using System.IO;
using System;
using Newtonsoft.Json;
using classAlimenots;
using ClassConfig;
using FuncionesNms;
using TMPro;




public class ChangeGenderr : MonoBehaviour
{
	public DynamicCharacterAvatar avatar;
	public DynamicCharacterAvatar avatarAnterior;
	public DynamicCharacterAvatar avatarPosterior;
	private Dictionary<string, DnaSetter> dna;
	//public Slider edadSlider;
	public Slider gordoSlider;
	public Slider estaturaSlider;
	public float peso = 0;
	public bool bandera = true;
	public string myAvatar;//recipiente

	public Toggle hombre;
	public Toggle mujer;
	

	public Camera camaraPrincipal;

	///avatar
	public GameObject Avatar3D;

	public bool Girar = true;

	public bool activarContador = false;
	//Contador
	public Text contador;
	private float tiempo = 3f;
	private float tiempoContComida;

	///Componentes simulacion
	public GameObject ButtonsalirSimulacion;//botom salir simulacion
	public GameObject textContadorGO;//tecto contador
	public GameObject ButtonterminarSimulacionGO;//salirSimulacion.enable = false;	
	public GameObject ButtonOpAGO;//opcion A act
	public GameObject ButtonOpBGO;//opcion B act
	public GameObject tipoAlimentacionText;//Text tipo alimentacion simulacion.
	public GameObject btninfoGenAlimentacion;//show btn generate aliments aleatorios


	///ventana configurar avatatar
	//public Text textPesoValue;
	public Text textEstaturaValue;
	///VEntana simulacion
	public Text txtPesoValPS;
	public Text txtImcValPS;
	public Text txtHeightValPS;
	public Text txtEdadValPS;
	public Text txtGETValPS;	

	public Dropdown edadSelect;
	public Dropdown mesesSelect;
	public List<classAlimentos> listData; //Arreglo aliemntos
	public List<classAlimentos> listAlimentsSim = new List<classAlimentos>(); //Arreglo aliemntos que el avatar consume durante la simulacion

	//Alimentos aleatorios 
	public int alimentoAleatorio1 = -1;
	public int alimentoAleatorio2 = -1;
	//Objetos para almacenar los aliemntos en la memoria.
	public classAlimentos opcionA;
	public classAlimentos opcionB;

	public List<Configuracion> dataConfig;

	public GameObject btnIniciarSimulacion;
	//Comidas por dia
	public string[] alimentosESP;
	public int comidasPorDia;
	public int contadorComidasPorDia = 0;//contador para cambiar el text de la alimentacion
	public int contDias ;// contador de los dias que han transcurrido
	public Text tipoAlimentacionTX;
	public Text contDiasText;
	public GameObject contDiasTextGO;

	public GameObject panelFinSimulacion;//panel que se muestra cuando se termina la simulacion

	public Funciones ins;
	public double contCaloriasPorDia = 0.0;

	public float pesoAuxiliar = 0f;
	public double getAuxiliar = 0.0;
	
	public GameObject  ButtonInfoOpAGO;
	public GameObject ButtonInfoOpBGO;
	public GameObject ButtonRandomAliments;
 
	
	////txt del panel de resultados
	public Text pesoAnteriorTxt;
	public Text pesoPosteriorTxt;

	public Text imcAnteriorTxt;
	public Text imcPosteriorTxt;

	public Text estaturaAnteriorTxt;
	public Text estaturaPosteriorTxt;



	public Text edadAnteriorTxt;
	public Text edadPosteriorTxt;
	public TextMeshProUGUI estatusActual;
	public TextMeshProUGUI estatusSimulado;
	public TextMeshProUGUI estatusIMC;

	public TextMeshProUGUI estatVistaConfAvatar;//text del estatus del peso en la vista de conf avatar


	//peso conf avatar panel
	public Text valPesoCAP;	

	public double rangoPeso = 132.0;//var para saber el rango de edad ej(1.10 a 1.70)= .63, para saber en cuantas partes se dividira el slider
	public	double dimencionSlider = 1.0;//tam del slider	

	public bool btnTerminarPress;
	public string[] tipoPT;///Tipo del periodo dia, semana, mes
	public int incrContComidas = 0;//variable para saber de cuanto en cuanto se aumentara las comidas
	
	////Variables para el panel de informacion nutimental}

	public Text nombrePItext;
	public Text namePItext;
	public Text cantidadPItext;
	public Text caloriasPItext;
	public Text proteinasPItext;
	public Text carbohidratosPItext;
	public Text fibraPItext;
	public Text azucarPItext;
	public Text GrasaPItext;
	public Text grasasSaturadaPItext;
	public Text grasasPolisaturadaPItext;
	public Text grasaMonoinstaturadaPItext;
	public Text colesterolPItext;
	public Text sodioPItext;
	public Text potasioPItext;
	public GameObject panelError;
	public Text textError;

	public int gordoSliderMinValue = 15;
	public GameObject panelConfirmation;
	public List<Configuracion> data;	
	public List<double> listCal =  new List<double>();


	void Awake(){
		gordoSliderMinValue = 15;
		//Debug.Log("Si entra a la primera fincion prueba we 1"); 	
		ins = new Funciones();
			
		if(ins.ifExisteArchivoConfig()){
			//Debug.Log("Si entra a la primera fincion prueba we"); 	
			crateAndonfigureAvatar();
		}
		recargarParametros();
		rangoEdadPeso();
		//cargarAvatar();
	}
	void Start(){
		inicializarVariables();//inicializa todas las variables a su valor por defecto 						
		recargarParametros();
		idiomaYComidasPorDia();	
			
		//panelError.SetActive(true);
		//textError.text = "No se puede inciar la app";
		
	}
	void Update(){
		

		if(activarContador){
			if(tiempo >= 0)
				tiempo -= Time.deltaTime;
			contador.text = ""+tiempo.ToString("f0");
			//Debug.Log("Si entra a la  funcion afuera: "+tiempo);
			if(tiempo <= 0 && tiempo >-2){
				//Debug.Log("Si entra a la prra duncion");
				//Se muestran los componentes para que inicie la simulacion
				funcComenzarSimulacion();
				
				dataConfig = ins.datosDeConfiguracion();//trae los datos de configuracion
				idiomaYComidasPorDia();
				tiempo = -4;
			}				
		}

	


		mainSimulacion();//Cuerpo de la simulacion
	}
	
	
	public void crateAndonfigureAvatar(){
		TextAsset avAux =(TextAsset)Resources.Load("avatarTxt");
		File.WriteAllText(Application.persistentDataPath + "/avatarTxt.txt",avAux.ToString()); 	
		
	}

   	public void guardarAvatar(){
   		myAvatar = avatar.GetCurrentRecipe();
   		File.WriteAllText(Application.persistentDataPath + "/avatarTxt.txt",myAvatar); 	
		//recargarParametros();	
   	}

   	public void cargarAvatar(){
   		myAvatar = File.ReadAllText(Application.persistentDataPath + "/avatarTxt.txt");
   		avatar.ClearSlots();
   		avatar.LoadFromRecipeString(myAvatar);
		avatar.CharacterUpdated.AddListener(Updated);
   	}

   	public void recargarParametros(){
   		cargarAvatar();			
		if(("o3n Stunner John" == avatar.activeRace.name)){//recarga el genero y los toggle
			mujer.isOn = true;
		}else{
			hombre.isOn = true;
		}
		ins = new Funciones();
		dataConfig = ins.datosDeConfiguracion();
		//solo se recarha el peso y estatura por que son los unicos que cambian 
		gordoSlider.value = dataConfig[0].pesoSlider;
		estaturaSlider.value = dataConfig[0].EstaturaSlider;
		Gordo(gordoSlider.value);
		Estatura(estaturaSlider.value);
		
   	}
	public void inicializarVariables(){
		ins = new Funciones();
		tiempo = 3f;
		contDias = 1;
		contador.text = ""+ tiempo;		
		contDiasText.text = "Dia "+contDias;	
		btnTerminarPress = false;
		activarContador = false;			
		contadorComidasPorDia = 0;
		listAlimentsSim = new List<classAlimentos>();		
	}
	public void mainSimulacion(){
		if((contadorComidasPorDia == 0 ) && (contDias != diasSimulacionFuncion()+1)){
			//Inicio del dia			
			const string sl = "/";
			contDiasText.text = tipoPT[dataConfig[0].periodo_tiempo]+contDias+" / "+dataConfig[0].periodo;			
			tiempoContComida = 1f;
		}	
		
		if(tiempoContComida >= 0f  && ButtonOpAGO.activeSelf){
			//condicion para que se active el texto dias
			tiempoContComida -= Time.deltaTime;
			contDiasTextGO.SetActive(true);			
		}
		else{
			contDiasTextGO.SetActive(false);
		}	

		/// Codigo que sirve para el texto de la alimentacion en la simulacion		
		if(contadorComidasPorDia > 4){
			//Findel dia cuando son 3 comidas al dia			
			afectacionCalorica();
			contadorComidasPorDia = 0;
			contDias += 1;						
		}
		///condicion para saber cuando la simulacion termino
		if((contDias == diasSimulacionFuncion()+1) || btnTerminarPress){
			//TErminar simulacion si
			Debug.Log("Fin de la simulacion");
			ButtonOpAGO.SetActive(false);
			ButtonOpBGO.SetActive(false);
			tipoAlimentacionText.SetActive(false);
			panelFinSimulacion.SetActive(true);
			ButtonterminarSimulacionGO.SetActive(false);
			ButtonInfoOpAGO.SetActive(false);//btn inf opcion A
			ButtonInfoOpBGO.SetActive(false);//btn inf opcion B
			ButtonRandomAliments.SetActive(false);//Generar alimentos aleatorios
			btninfoGenAlimentacion.SetActive(false);			
			finSimulacion();
			guardarAvatar();
			btnTerminarPress = false;
			//Debug.Log("SI ENTRA EN LA FUNCION");
			//Debug.Log("CONT DIAS ES: "+contDias);
			double suma = 0.0;
			foreach(double i in listCal){
				Debug.Log(i);
				suma = i + suma;
			}
			//Debug.Log("LA SUMATORIA CALORICA ES: "+ suma);
		}			
		tipoAlimentacionTX.text  = alimentosESP[contadorComidasPorDia];	
	}
	public void panelConfAvatarEstatusPeso(){
		///FUNCION PARA PONER EN EL NIVEL DEL PSEO SE QUE SE ENCUENTRA LA PEERSONA 
		///EN EL PANEL DE CONFIGURAR AVATAR
		string sexo = "";
		if(hombre.isOn){
			sexo = "Mujer";}
		else{
			sexo = "Hombre";
		}
		float peso =(float)Math.Round((double)(gordoSliderMinValue+gordoSlider.value),1,MidpointRounding.ToEven);
		//Debug.Log("El peso del gordoSlider es: "+gordoSliderMinValue);
		float estatura =(float)(1.10+(double)((int) estaturaSlider.value*.01));//estatura 1.70
		estatura=(float)Math.Round((double)estatura,1,MidpointRounding.ToEven);
		//Debug.Log("El peso es: "+peso+" La estatura es: "+estatura);
		float imc = peso/(estatura*estatura);
		Funciones ins = new Funciones();
		dataConfig = ins.datosDeConfiguracion();
		string res= ins.estatusPeso(
			dataConfig[0].idioma,
			imc,
			edadSelect.value+7,
			mesesSelect.value+1,			
			sexo
		);
		estatVistaConfAvatar.text = res;
		estatVistaConfAvatar.color = ins.returnColor(res);

		estatusIMC.text = "IMC: "+Math.Round((double)imc,1,MidpointRounding.ToEven);
	
	}
    //fun save conf avatar simulation
    public void guardarConfiguracionAvatarJson(){
		//Guardar conf Avatar
		Funciones instFun =new Funciones();
      	data =  instFun.datosDeConfiguracion();         
		//Asigna los valores de unity al script de configuracion       	   
       	data[0].Edad = GameObject.Find("DroppdownYears").GetComponent<Dropdown>().value;
		data[0].Meses = GameObject.Find("DroppdownMoths").GetComponent<Dropdown>().value;   //Edad
		data[0].pesoSlider = gordoSlider.value;
     	data[0].Peso = (float)(gordoSliderMinValue+gordoSlider.value);//peso
		data[0].Peso = (float)Math.Round((double)data[0].Peso,1,MidpointRounding.ToEven);
     	data[0].EstaturaSlider = estaturaSlider.value;//
		data[0].Estatura = (float)(1.10+(double)((int) estaturaSlider.value*.01));//estatura 1.70
		if(hombre.isOn == true){ data[0].Sexo = "Mujer";}else{data[0].Sexo = "Hombre";}
      	//Guarda en el json		  
      	data[0].IMC =(float)Math.Round((data[0].Peso)/(data[0].Estatura*data[0].Estatura), 1 , MidpointRounding.ToEven); 
		 // int edad, int AF,double peso, double estatura,bool sexo		
     	data[0].GET = instFun.calcularGET(data[0].Edad,
		 								  data[0].tipo_ejercicio,
										  data[0].Peso,
										  data[0].Estatura,
										  hombre.isOn);
		data[0].GET = Math.Round(data[0].GET,1,MidpointRounding.ToEven);
		data[0].idioma = data[0].idioma;
		instFun.guardarDatosConfiguración(data);		
		panelConfirmation.SetActive(true);		
		
    }
	

	public void activadorTerminarSim(){
		//esta funcion sive para saber que se ha presionado el btn rerminar simulacion sin que esta 
		//haya cumplido con el periodo de tiempo
		btnTerminarPress = true;
	}
	public void finSimulacion(){
		//Funcion para agregar los parametros del panel final
		pesoAnteriorTxt.text = Math.Round(dataConfig[0].Peso,1,MidpointRounding.ToEven)+" kg";
		pesoPosteriorTxt.text = txtPesoValPS.text;
		imcAnteriorTxt.text = dataConfig[0].IMC+"";
		imcPosteriorTxt.text = txtImcValPS.text+"";
		estaturaAnteriorTxt.text = dataConfig[0].Estatura+" mts";
		estaturaPosteriorTxt.text= dataConfig[0].Estatura+" mts";
		if(dataConfig[0].idioma == "español"){
			edadAnteriorTxt.text = (dataConfig[0].Edad+7)+" años";
			edadPosteriorTxt.text = (dataConfig[0].Edad+7)+" años";
		}else{
			edadAnteriorTxt.text = (dataConfig[0].Edad+7)+" years";
			edadPosteriorTxt.text = (dataConfig[0].Edad+7)+" years";			
		}
				
		float imcPost = Convert.ToSingle(imcPosteriorTxt.text,System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));//imc simulado
		ins = new Funciones();
		//Funciones para saber en que nivel se peso se encuentran
		string res1 = ins.estatusPeso(dataConfig[0].idioma,dataConfig[0].IMC,dataConfig[0].Edad+7,dataConfig[0].Meses+1,dataConfig[0].Sexo);
		string res2 = ins.estatusPeso(
			dataConfig[0].idioma,
			imcPost,
			dataConfig[0].Edad+7,
			dataConfig[0].Meses+1,
			dataConfig[0].Sexo
		);
		estatusActual.text = res1;
		estatusActual.color = ins.returnColor(res1);
		estatusSimulado.text =  res2;
		estatusSimulado.color = ins.returnColor(res2);
		imcAnteriorTxt.color = ins.returnColor(res1);
		imcPosteriorTxt.color = ins.returnColor(res2);
		


		inicializarVariables();

		txtImcValPS.text = dataConfig[0].IMC+"";
		txtGETValPS.text = dataConfig[0].GET+"";
		txtHeightValPS.text = dataConfig[0].Estatura+" mts";
		txtPesoValPS.text = dataConfig[0].Peso+" kg";	
		if(dataConfig[0].idioma == "español")
		{txtEdadValPS.text = (dataConfig[0].Edad+7)+" años";	}
		else{
			txtEdadValPS.text = (dataConfig[0].Edad+7)+" years";	
		}
		
	}
	///Funcion para aumentar las calorias al avatar
	public void afectacionCalorica(){
		double decrementoCalorico = 0.0;
		int[] periodoArray = {0,1,7,28};//x,dias,sem, meses
		decrementoCalorico = (contCaloriasPorDia*periodoArray[dataConfig[0].periodo_tiempo+1]) - (getAuxiliar*periodoArray[dataConfig[0].periodo_tiempo+1]);//Diferencia calorica
		Debug.Log("GETS "+contCaloriasPorDia*periodoArray[dataConfig[0].periodo_tiempo+1]+", GETT: "+(getAuxiliar*periodoArray[dataConfig[0].periodo_tiempo+1]));
		//Debug.Log("La resta de las Kcal cons y Kcal sim es: "+decrementoCalorico);
		decrementoCalorico = decrementoCalorico/7000.0;//las dif calorica se convierten a kg para la afectación
		//decrementoCalorico = decrementoCalorico * periodoArray[dataConfig[0].periodo_tiempo+1];		
		listCal.Add(contCaloriasPorDia*(double)periodoArray[dataConfig[0].periodo_tiempo+1]);
		

		//Se covierten las calorias a calorias simulacion
		//Debug.Log("El decremento calorico es: "+ decrementoCalorico);
		if(getAuxiliar < contCaloriasPorDia){
			//Las calorias simuladas son < a las calorias necesarias
			//Quiere decir que el avatar disminuira su peso
			pesoAuxiliar = (float)decrementoCalorico+pesoAuxiliar;//Afectacion calorica
			gordoSlider.value = pesoAuxiliar;
		}
		else{
			pesoAuxiliar = pesoAuxiliar - Math.Abs((float)decrementoCalorico);
			if(pesoAuxiliar > 0)
				gordoSlider.value = pesoAuxiliar;
			//Debug.Log("El decremento calorico es negativo");
		}
		contCaloriasPorDia= 0.0;
		Funciones instFun = new Funciones();
		//Se recalcula el get de acuerdo a sus cambios corporales
		float peso = (float)(gordoSliderMinValue+gordoSlider.value);//peso
		getAuxiliar = instFun.calcularGET(dataConfig[0].Edad,
		 								  dataConfig[0].tipo_ejercicio,
										  peso,
										  dataConfig[0].Estatura,
										  hombre.isOn);	
		getAuxiliar = Math.Round(getAuxiliar,1,MidpointRounding.ToEven);
		Debug.Log("El peso simulado es: "+peso);										  
		txtGETValPS.text = getAuxiliar+"";											
	}
	public int diasSimulacionFuncion(){		
		dataConfig = ins.datosDeConfiguracion();		
		int dias =0;
		int[] periodoTiempo = {1,7,28};
		//Cuando es por dias
		if(dataConfig[0].periodo_tiempo == 0)//Dias
			dias = dataConfig[0].periodo * periodoTiempo[dataConfig[0].periodo_tiempo];
		else//Semanas y meses
		   dias = dataConfig[0].periodo;		
		return dias;
	}
	public void idiomaYComidasPorDia(){
		if(dataConfig[0].idioma == "español"){
			alimentosESP = new string[] {"Selecciona Almuerzo","Selecciona Colacion","Selecciona Comida", "Selecciona Colacion","Selecciona Cena"};
			tipoPT = new string[] {"Dia ","Sem ","Mes "};
		}
		else{//idiom español
			alimentosESP = new string[] {"Choose lunch","Choose Snack", "Choose eat","Choose Snack","Choose dinner"};
			tipoPT = new string[] {"Day ","Week ","Month "};
		}
		comidasPorDia = dataConfig[0].Comidas_dia;
		if(comidasPorDia == 0){
			incrContComidas = 2;}
		else{ 
			incrContComidas = 1;}		
		tipoAlimentacionTX.text = alimentosESP[0];
		//Debug.Log("Las comidas POR DI ASON: "+ comidasPorDia);
	}



	///VEr informacion opcion A
	public void verInfoOpA(){
		paramtrosNutrimentales(opcionA);
	}
	///VEr informacion opcion B
	public void verInfoOpB(){
		paramtrosNutrimentales(opcionB);
	}	
	public void seleccionOpcionA(){
		///Esta funcion se ejecutara cuando el usuario precione el boton verde de la simulacion 
		//Se guarda en una lista  el alimento
		listAlimentsSim.Add(opcionA);
		contCaloriasPorDia += opcionA.calorias;//contador calorias	
		contadorComidasPorDia += incrContComidas;	
		
		generarAlimentoAleatorio();

	}
	public void seleccionOpcionB(){
		///Esta funcion se ejecutara cuando el usuario precione el boton Amarillo de la simulacion 
		// se guarda en una lista el alimento
		listAlimentsSim.Add(opcionB);
		contadorComidasPorDia += incrContComidas;		
		contCaloriasPorDia += opcionB.calorias;//contador calorias	
		generarAlimentoAleatorio();	
	}	


	public void paramtrosNutrimentales(classAlimentos opcion){
			if(dataConfig[0].idioma == "ingles"){
				nombrePItext.text = opcion.name+ ""; 
			}else{
            	nombrePItext.text = opcion.nombre+ ""; 
			}           
            namePItext.text = opcion.name+ "";
            cantidadPItext.text = opcion.cantidad+ "";
            caloriasPItext.text = opcion.calorias+ " kcal"; 
            proteinasPItext.text = opcion.proteinas+ " g"; 
            carbohidratosPItext.text = opcion.carbohidratos+ " g"; 
            fibraPItext.text = opcion.fibra+ " g"; 
            azucarPItext.text = opcion.azucar+ " g"; 
            GrasaPItext.text = opcion.Grasa+ " g"; 
            grasasSaturadaPItext.text = opcion.grasasSaturada+ " g"; 
            grasasPolisaturadaPItext.text = opcion.grasasPolisaturada+ " g"; 
            grasaMonoinstaturadaPItext.text = opcion.grasaMonoinstaturada+ " g"; 
            colesterolPItext.text = opcion.colesterol+ " mg"; 
            sodioPItext.text = opcion.sodio+ " mg"; 
            potasioPItext.text = opcion.potasio+ " mg";          

	}

	//Funcion para generar alimentos de forma aleatoria
	public void generarAlimentoAleatorio(){
		//Intancia de random
		System.Random random = new  System.Random();
		//int=random.Next(lim_inf, lim_superior+1);
		//leer json
		int i= 0 ;
		
		do{
			alimentoAleatorio1 = random.Next(0, listData.Count);
			alimentoAleatorio2 = random.Next(0, listData.Count);
		}while(listData[alimentoAleatorio1].id==listData[alimentoAleatorio2].id);
		//Debug.Log(listData[1].nombre);
		

		if(dataConfig[0].idioma == "ingles"){
			GameObject.Find("opcionA").GetComponentInChildren<Text>().text = listData[alimentoAleatorio1].name;
			GameObject.Find("opcionB").GetComponentInChildren<Text>().text = listData[alimentoAleatorio2].name;
		}
		else{
			GameObject.Find("opcionA").GetComponentInChildren<Text>().text = listData[alimentoAleatorio1].nombre;
			GameObject.Find("opcionB").GetComponentInChildren<Text>().text = listData[alimentoAleatorio2].nombre;			
		}

		opcionA = listData[alimentoAleatorio1];//se le asgna la opcion uno a la variable temporal
		opcionB = listData[alimentoAleatorio2];//se le asgna la opcion uno a la variable temporal

		//Debug.Log("Este es el name after: "+opcionA.nombre);
		//Debug.Log("Este es el name after: "+opcionA.calorias);
	}

	//funcion para activar el contador
	public void funActivarContador(){
		activarContador = true;
	}
	//funcion para comenzar la simulación despues del contador


	public void funcComenzarSimulacion(){
		ButtonsalirSimulacion.SetActive(false);//botonSalurSimulacion dac				
		ButtonterminarSimulacionGO.SetActive(true);//botonfinalizar ac
		
		ButtonOpAGO.SetActive(true);//opcion A act
		ButtonOpBGO.SetActive(true);//opcion B act
		ButtonInfoOpAGO.SetActive(true);//btn inf opcion A
		ButtonInfoOpBGO.SetActive(true);//btn inf opcion B
		ButtonRandomAliments.SetActive(true);//Generar alimentos aleatorios
		textContadorGO.SetActive(false);//text contador dac
		btninfoGenAlimentacion.SetActive(true);

		tipoAlimentacionText.SetActive(true);// Texto que muestra el tipo de alimentacion
		//Se traeb lis alinentos de la base de datos
		listData =  new List<classAlimentos>();		
      	listData = ins.bddAlimentos();	
		//Se genera aliments aleatorio
		generarAlimentoAleatorio();
		//tiempoContComida = -1f;
		
		dataConfig =ins.datosDeConfiguracion();
		//Se guarda la apariencia del avatar en un inicio

		myAvatar = avatar.GetCurrentRecipe();
   		File.WriteAllText(Application.persistentDataPath + "/avatarAuxTxt.txt",myAvatar); 

		pesoAuxiliar = (float)dataConfig[0].pesoSlider;//Se inicializa el peso
		getAuxiliar = dataConfig[0].GET;//Se inicializa el GET


		
	}




	//funcion para mostrar los resultados de la sumulacion
	public void funcionMostrarResFinal(){
		//Se activaran todos los componentes para mostrar la simulacion final
		//Se cargaran los parametros de los avatares.
		//Debug.Log("EL contador es: "+activarContador);
   		string avatarAux = File.ReadAllText(Application.persistentDataPath + "/avatarAuxTxt.txt");
		string avatarAfectado = File.ReadAllText(Application.persistentDataPath + "/avatarTxt.txt");

   		avatarAnterior.ClearSlots();
   		avatarAnterior.LoadFromRecipeString(avatarAux);	

   		avatarPosterior.ClearSlots();
   		avatarPosterior.LoadFromRecipeString(avatarAfectado);	

		avatar.ClearSlots();
		avatar.LoadFromRecipeString(avatarAux);
		guardarAvatar();		   	
		
	}
   	void Updated(UMAData data){
    	dna = avatar.GetDNA(); 		
    }

	void OnEnable(){
		avatar.CharacterUpdated.AddListener(Updated);
		//edadSlider.onValueChanged.AddListener(funEdad);
		gordoSlider.onValueChanged.AddListener(Gordo);
		estaturaSlider.onValueChanged.AddListener(Estatura);
		
	}

	void OnDiable(){
		avatar.CharacterUpdated.RemoveListener(Updated);
		//edadSlider.onValueChanged.RemoveListener(funEdad);
		gordoSlider.onValueChanged.RemoveListener(Gordo);
		estaturaSlider.onValueChanged.RemoveListener(Estatura);
	}



	//FUNCION PARA CAMBIAR DE GENERO	
    public void SwitchGender(bool male)
    {			
    	if(!mujer.isOn){
    		avatar.ChangeRace("o3n Stunner Jane");
		}
    	else{
    		avatar.ChangeRace("o3n Stunner John");	
		}
		dna = avatar.GetDNA(); 	

    }
 
	public void rangoEdadPeso(){
		//FUNCION PARA DEF EL RANGO DE PESO DE ACUERO A LA EDAD.
		//EL peso maximo cambiara de acuerdo a la estatura
		//Debug.Log("Si ENTRA");
		float estatura = (float)(1.10+(double)((int)estaturaSlider.value*.01));//estatura 1.70
		if(hombre.isOn){
			///El avatar es mujer
			if(estatura  < (float)1.20){
				gordoSliderMinValue = 15; gordoSlider.maxValue = 40-gordoSliderMinValue;
			}
			else if(estatura < (float)1.30){
				gordoSliderMinValue = 15; gordoSlider.maxValue = 50-gordoSliderMinValue;
			}
			else if(estatura < (float)1.40){
				gordoSliderMinValue = 15; gordoSlider.maxValue = 60-gordoSliderMinValue;
			}
			else if(estatura < (float)1.50){
				gordoSliderMinValue = 20; gordoSlider.maxValue = 70-gordoSliderMinValue;
			}	
			else if(estatura < (float)1.60){
				gordoSliderMinValue = 20; gordoSlider.maxValue = 85-gordoSliderMinValue;
			}				
			else if(estatura < (float)1.70){
				gordoSliderMinValue = 25; gordoSlider.maxValue = 95-gordoSliderMinValue;
			}
			else{	
				gordoSliderMinValue = 30; gordoSlider.maxValue = 100-gordoSliderMinValue;
			}
		}
		else{
			//El avatar es hombre
			if(estatura  < (float)1.20){
				gordoSliderMinValue = 15; gordoSlider.maxValue = 45-gordoSliderMinValue;
			}
			else if(estatura < (float)1.30){
				gordoSliderMinValue = 15; gordoSlider.maxValue = 55-gordoSliderMinValue;
			}
			else if(estatura < (float)1.40){
				gordoSliderMinValue = 15; gordoSlider.maxValue = 65-gordoSliderMinValue;
			}
			else if(estatura < (float)1.50){
				gordoSliderMinValue = 20; gordoSlider.maxValue = 70-gordoSliderMinValue;
			}	
			else if(estatura < (float)1.60){
				gordoSliderMinValue = 20; gordoSlider.maxValue = 85-gordoSliderMinValue;
			}				
			else if(estatura < (float)1.70){
				gordoSliderMinValue = 25; gordoSlider.maxValue = 95-gordoSliderMinValue;
			}
			else{	
				gordoSliderMinValue = 30; gordoSlider.maxValue = 100-gordoSliderMinValue;
			}
		}				

		Gordo(gordoSlider.value);
	}
    public void Estatura(float val){
		double rangoStature = 63.0;//var para saber el rango de edad ej(1.10 a 1.70)= .63, para saber en cuantas partes se dividira el slider
		double dimencionSlider = 1.0;//tam del slider

		double valueSlider = (double)(((int)val)*(dimencionSlider/rangoStature));//VALOR del slider de 0.0 a 1.0
		double valueEstatura = (1.10+(double)((int)val*.01));//estatura 1.70

    	dna["stature"].Set((float)valueSlider);
		if(valueSlider > 0.2)
			dna["ageBase"].Set((float)0.2);
		if(valueSlider <  0.2)
			dna["ageBase"].Set((float)valueSlider);	
    	
		avatar.BuildCharacter();
		//0.0158730159		

		textEstaturaValue.text = ""+valueEstatura+" mts";
    }
    public void Gordo(float val){

		//convertir el valor a float		
		float valueSlider = (float)(((int)val)*(dimencionSlider/gordoSlider.maxValue));//VALOR del slider de 0.0 a 1.0
		float valuePeso = (float)(gordoSliderMinValue+val);//estatura 1.70
    	///FUNCION PARA CAMBIAR EL PESO DEL AVATAR
    	dna["armWidth"].Set(valueSlider);
    	dna["chinSize"].Set(valueSlider);//anchura de la barbilla
    	dna["forearmWidth"].Set(valueSlider);//anchura ante brazo
    	dna["gluteusWidth"].Set(valueSlider);//ancho cadera
    	dna["headWidth"].Set(valueSlider);//anchura de la cabeza
    	dna["jawsSize"].Set(valueSlider);//ancho mandubula
    	dna["neckThickness"].Set(valueSlider);//ancho del cuello
    	dna["belly"].Set(valueSlider);//barriga
    	dna["waist"].Set(valueSlider);//cintura
    	dna["weightUpper"].Set(valueSlider);//Gurdura parte superior
    	dna["weightLower"].Set(valueSlider);//Gprduta parte inferior
    	dna["breastCleavage"].Set(valueSlider);//pecho-abierto
    	dna["noseWidth"].Set(valueSlider);//ancho de la nariz
    	dna["breastSize"].Set(valueSlider);//cachetes
    	dna["gluteusSize"].Set(valueSlider);//tam gluteos
    	dna["agePosture"].Set(valueSlider);//postura inclinacion
    	dna["cheekSize"].Set(valueSlider);//ancho mejilla
    	avatar.BuildCharacter();	
		double peso = (val*100.0)+18.0;
		///Text panel simulacion
		double estatura = (1.10+(double)((int) estaturaSlider.value*.01));//estatura 1.70
		txtHeightValPS.text = estatura+" mts";//estatura 1.70
		txtPesoValPS.text = Math.Round(valuePeso, 1 , MidpointRounding.ToEven)+" kg";
		txtImcValPS.text = ""+Math.Round((valuePeso/(estatura * estatura)), 1 , MidpointRounding.ToEven);
		
		///configuracion avatar panel
		valPesoCAP.text = Math.Round(valuePeso, 1 , MidpointRounding.ToEven)+" kg";

    }

	
	
}
