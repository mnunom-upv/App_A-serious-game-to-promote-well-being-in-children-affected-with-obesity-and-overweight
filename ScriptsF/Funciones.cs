using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClassConfig;
using classAlimenots;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FuncionesNms{
public class Funciones :MonoBehaviour
{
    
	public string dataJsonAlimentos = "";
	
// Ecuacion,Comidas_dia,periodo,periodo_tiempo,tipo_ejercicio,Edad,Meses,Peso,pesoSlider,Estatura,EstaturaSlider,Sexo,IMC,GET,idioma){
	
	//Funcion para verificar si existe el archivo, si no es asi lo crea
	public bool ifExisteArchivoConfig(){
		if(!File.Exists(Path.Combine(Application.persistentDataPath, "config.json"))){
			List<Configuracion> data = new List<Configuracion>(); 
			data.Add(new Configuracion(0,0,5,0,0,0,0,(float)15,0,(float)1.10,0,"Mujer",(float)12.4,1107.7,"español"));//constructor de la clase para la inicializacion de los parametros
			FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "config.json"));
			string newConf = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);
			file.Close();
			//inicializar el json
			string dir = Application.persistentDataPath+"/config.json";  
			File.WriteAllText(dir, newConf); 
			//Debug.Log("Entra a la opcion que treturn true");
			return true;       
		}else{
			//Debug.Log("Entra a la opcion que treturn false");
			return false;
		}		
		
	}

	public List<classAlimentos> datAliments;
	public bool inicalizarDBAlimentos(){
		//Se valida que exista el documento, de lo contrario se crea
		datAliments = new List<classAlimentos>();			
		StartCoroutine("peticionServer");	
			
		
		return true;
	}

	///FUNCION PARA TRAER LOS ALIMENTOS DEL SERVIDOR
	public IEnumerator peticionServer(){
		//Se crea el archivo
		FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "dbAlimentos.json"));
		file.Close();	

		//Conexion al server
		string dirJson = Application.persistentDataPath+"/dbAlimentos.json";
		string url = "http://178.128.151.68/show/alimento/unity";
		WWW getJson = new WWW(url);		
		yield return getJson;

		//Se trabaja con el json obtenido
		datAliments =  new List<classAlimentos>();
		if(getJson.text!=""){//Si se pudo hacer la conexxion con el server			
			datAliments = JsonConvert.DeserializeObject<List<classAlimentos>>(getJson.text);			      				
			Debug.Log("Si trae los datos del server");
						
		}else{//No c pudo hacer la conexion con el server			
			TextAsset json = (TextAsset)Resources.Load("dbAlimentosAux");
      		datAliments = JsonConvert.DeserializeObject<List<classAlimentos>>(json.ToString()); 						
			Debug.Log("No trae los datos del server");
			//Se debe de cargar los datos por defecto	
		}
		string alimentos = JsonConvert.SerializeObject(datAliments.ToArray(), Formatting.Indented);		
		File.WriteAllText(dirJson, alimentos);
		GameObject.Find("ventanaDeEspera").SetActive(false);
				
		
	}
    //Funcion para traer los alimentos
	public List<classAlimentos> bddAlimentos(){
		string dirJson = Application.persistentDataPath+"/dbAlimentos.json";
		datAliments =  new List<classAlimentos>();
		string loadData = File.ReadAllText(dirJson);
      	datAliments = JsonConvert.DeserializeObject<List<classAlimentos>>(loadData.ToString()); 
        return datAliments;		
	}
	
	//Funcion para traer los datos de la configuracion
    public List<Configuracion> datosDeConfiguracion(){
		string dirJson = Application.persistentDataPath+"/config.json";
        List<Configuracion> data = new List<Configuracion>();        
        	
      	string loadData = File.ReadAllText(dirJson);
		//Debug.Log(loadData);
      	data = JsonConvert.DeserializeObject<List<Configuracion>>(loadData.ToString()); 
        return data;
    }
    //Funcion para guardar los datos de configuracion
    public void guardarDatosConfiguración(List<Configuracion> data){
		string dirJson = Application.persistentDataPath+"/config.json";
        string newConf = JsonConvert.SerializeObject(data.ToArray(), Formatting.Indented);
      	File.WriteAllText(dirJson, newConf);
    }
	///Calculo del gasto calorico total   
   	public double calcularGET(int edad, int AF,double peso, double estatura,bool sexo){
		//AF = Actividad fisica
   		double GER,GET,ETA;
        edad = edad + 7;
   		double[] aFisica= {1.2,1.375,1.55,1.725,1.9};//Valor para cada ejercicio   		
   		GER = ecuacionSchifield(edad,sexo,peso,estatura);
        //formula para obtener el ETA que pertenece al 10% del ger
        ETA = GER*.10;
        //formula para obtener el gasto de energia total
        GET = GER*aFisica[AF]+ETA;
        Debug.Log("EL GEt total es: "+GET);
        //getText.text =  GET+"kcal";  
        return GET; 		
   	}
	
	//Funcion para retornar el peso lo mas realista posible
	public float returnPeso(float peso){
		//proporcion de peso redondeado
		
		return peso;
	}

	public Color32 returnColor(string res){
		Color32 color = new Color32(0,0,0,0);
		//la funcion recibe el estatus del peso y regresa el color de acuerdpo al peso
		if(res == "Desnutrición severa" || res == "Severe malnutrition"){color = new Color32(238,145,113,255);}
		else if(res == "Desnutrición Moderada"  || res == "Moderate malnutrition"){color = new Color32(235,238,113,255);}
		else if(res == "Normal"){color = new Color32(148,238,113,255);}
		else if(res == "Sobrepeso" || res == "Overweight"){color = new Color32(209,193,240,255);}
		else{color = new Color32(141,93,235,255);}		
		
		return color;
	}

	//Funcion para saber en que nivel de sobrepeso se encuentra
	public string estatusPeso(string idioma,float IMC,int edad, int meses,string sexo){
		//Tabla de imc para niñas de 7 a 13
		int[,] tableIMC;
		string[] vecStatus;
		if(idioma == "español"){
			//el abcedario esta en codigo ascii para los clores
			//238,145,113
			vecStatus= new string[5]{"Desnutrición severa",
									"Desnutrición Moderada",
									"Normal",
									"Sobrepeso",
									"Obesidad"};
		}
		else
		{
			vecStatus = new string[5]{"Severe malnutrition",
									  "Moderate malnutrition",
									  "Normal",
									  "Overweight",
									  "Obesity"};
		}
				
		
		if(sexo == "Mujer"){
		 	tableIMC= new int[12,6]    {{7,7,118,127,174,199},
										{7,13,118,128,176,202},
										{8,7,119,129,178,207},
										{8,13,120,130,181,211},
										{9,7,121,131,184,216},
										{9,13,122,133,188,221},
										{10,7,124,135,191,227},
										{10,13,125,137,195,232},
										{11,7,127,139,200,238},
										{11,13,129,141,204,244},
										{12,7,132,144,209,251},
										{12,13,134,147,214,257}};
		}
		else{
			//Tabla de imc para niños de 7 a 13
			tableIMC = new int[12,6] {{7,7,123,131,171,191},
										{7,13,123,132,173,194},
										{8,7,124,133,175,198},
										{8,13,125,134,178,202},
										{9,7,126,135,180,206},
										{9,13,127,136,183,213},
										{10,7,128,137,186,215},
										{10,13,129,139,189,220},
										{11,7,131,141,193,226},
										{11,13,132,142,196,231},
										{12,7,134,145,200,237},
										{12,13,136,147,205,243}};	
		}
		//Convertir la edad y meses en un solo entero
		//Evaluar que sea igual o menor que la edad
		//hubicarlo en su estatis
		//Debug.Log(Math.Round(IMC*10));
		for(int i = 0;i<12;i++){
			if(tableIMC[i,0] == edad && tableIMC[i,1]>meses){
				for(int j=2; j<6;j++){
					if(tableIMC[i,j] > Math.Round(IMC*10)){
						return vecStatus[j-2];
					}
				}
				return vecStatus[4];
			}
		}

		return vecStatus[4];									
	}

	///Ecucacion de shofield
	public double ecuacionSchifield(int Edad,bool Sexo,double Peso,double Estatura){
		double GER;
		double ETA;
		Estatura = Estatura*100;

		if(Sexo == false){
			//opcion Hombre
			if(Edad>= 7 && Edad < 10){
				GER = (19.59*Peso)+(1.303*Estatura)+414.9;
			}
			else{
				GER = (16.25*Peso)+(1.72*Estatura)+515.5;
			}			
		}
		else{
			//opcion Mujer
			if(Edad>= 7 && Edad < 10){
				GER = (16.969*Peso)+(1.618*Estatura)+371.2;
			}
			else{
				GER = (8.365*Peso)+(4.65*Estatura)+200;
			}		
		}
		//fin GER

		return GER;

	}      		

}
}