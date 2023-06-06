using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using ClassConfig;


public class lenguajeComponents : MonoBehaviour
{
    // Start is called before the first frame update

    //panels
    //public Button ConfigurarAvatar;
    //public TextMeshProUGUI b1text;
    public Text edadText;
    public Text estaturaText;
    public Text pesoText;


    //paramets panel information
    public TextMeshProUGUI parametroIMCText;
    public TextMeshProUGUI parametroEstaturaText;
    public TextMeshProUGUI parametroEdadText;
    public TextMeshProUGUI parametroGetTex;
    public TextMeshProUGUI parametroPesoText;
    public TextMeshProUGUI parametroAF;
    //public ButtonMeshRender

    //panel information programadores
    public Text informationCreadores;
    public Text title;
    public Text Autor;
    public Text Director;
    public Text coDirector;

    //panel confirmation 
    public Text panelConfirmationText;



    //paneles 
    public GameObject panelMenuPrincipal;
    public GameObject panelSimulation;
    public GameObject panelConfAvatar;
    public GameObject panelConfirmation;
    public GameObject panelInformationProgramation;
    public GameObject panelConfSimulation;
    public GameObject panelResultados;
    //dropdowns
    public Dropdown DropdownEcuacion;


    //json
	public List<Configuracion> data;

    //
    public InputField inFieldPeriod;

    //buton salir simulacion
    public Button ButtonSalirSumulacion;
	
    public Button ButtonTerminarSimulacion;

    ///panel resulatados
    public TextMeshProUGUI pesoPRtext;
    public TextMeshProUGUI imcPRtext;
    public TextMeshProUGUI estaturaPRtext;
    public TextMeshProUGUI edadPRtext;

    ///conf panle fin simulacion
    public Text finSimText;
    public Button ButtonVerResultadoS;
    public Button ButtonTermPS;
    //Panel informacion nutricional
    public Text titlePItext;
    public Text porcionText;
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
    public Button ButtonClosePIN; 
    public GameObject panelInfNutrimental;
    public GameObject panelEspera;
    public GameObject panelInfButton;
    public TextMeshProUGUI textInfButton;
    void Start()
    {
        if(idioma() == "ingles"){
            Ingles();

        }
        else{
            Espanol();       
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(idioma() == "ingles"){
            Ingles();         
        }
        else{
            Espanol();             
        }        
    }
    

    public string idioma(){
    	data =  new List<Configuracion>();
      	string json = Application.persistentDataPath+"/config.json";	
      	string loadData = File.ReadAllText(json);
      	data = JsonConvert.DeserializeObject<List<Configuracion>>(loadData.ToString()); 
        return data[0].idioma;        
    }

    public void infButtonGeneral(int value){
        string[] mesaggePanel;
        //Debug.Log("El valor que recibe es :"+value);
        if(idioma() != "ingles"){
            mesaggePanel = new string[8]{
                "Botón para configurar el género, peso, edad y estatura.",
                "Botón para configurar la ecuación predictiva, ejercicio, comidas al día, tiempo de simulación y cada cuanto desea cambiar el menú(Dia, Semana o meses).",
                "Botón para iniciar la simulación en la que se deben de seleccionar las comidas con el fin de observar el cambio corporal del avatar y sus parámetros.",
                "Ecuación para calcular cuantas calorias necesita el tu cuerpo durante el dia.",
                "Sedentario: Nunca realiza, Ligeramente activo: de 1 a 3 veces por semana , Moderadamente: 3 a 5 veces por semana, Hiperactivo: Toda la semana intensamente.",
                "Comidas que come o desea comer por dia.",
                "Por cuánto tiempo desea realizar la simulación, puede ser por días, semanas o meses.",
                "Botón para generar otras opciones de alimentos."
            };
        }else{
            mesaggePanel = new string[8]{
                 "Button to configure gender, weight, age and height.",
                 "Button to configure the predictive equation, exercise, meals per day, simulation time and how often you want to change the menu (Day, Week or months).",
                 "Button to start the simulation in which the meals must be selected in order to observe the change in the body of the avatar and its parameters.",
                 "Equation to calculate how many calories your body needs during the day.",
                 "Sedentary: Never performs, Slightly active: 1 to 3 times a week, Moderately: 3 to 5 times a week, Hyperactive: Intensively all week.",
                 "Meals you eat or want to eat per day.",
                 "For how long you want to run the simulation, it can be for days, weeks or months.",
                 "Button to generate other food options."

            };
        }

        textInfButton.text = mesaggePanel[value];
    }

 

    //idioma en ingles
    public void Ingles(){
        //panel donde se cargan los datos del servidor
        if(panelEspera.activeSelf){
            GameObject.Find("textoEspera").GetComponentInChildren<Text>().text = "Starting up, wait a few minutes.";
        }
        // Ventana del menu principal        
        //aparece una excepcion cuando los componentes estan desactivados
        if(panelMenuPrincipal.activeSelf){
            //Debug.Log("Menu principal Activado");
            GameObject.Find("Configurar").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Manage avatar";
            GameObject.Find("confSimulacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Manage Simulation";
            GameObject.Find("iniciarSimulacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Start Simulation";
            GameObject.Find("salirAplicacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Exit";
           // GameObject.Find("salirButtom").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Salir";
            GameObject.Find("idiomaEsp").GetComponent<Image>().color = new Color32( 255,255,255, 255);
            GameObject.Find("idiomaIngles").GetComponent<Image>().color = new Color32( 63, 144, 226, 255 );           
        }
        /// ConfAvatarPanel
        if(panelConfAvatar.activeSelf){
            edadText.text = "Age";
            pesoText.text = "Weight";//Peso
            estaturaText.text = "Height";//Estatura
            GameObject.Find("guardarButtom").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Save";
            GameObject.Find("opMujer").GetComponentInChildren<Text>().text = "Man";
            GameObject.Find("opHombre").GetComponentInChildren<Text>().text = "Woman";
            GameObject.Find("TextMeses").GetComponentInChildren<Text>().text = "Months";
        }
        
        //PAnel simulation information
        if(panelSimulation.activeSelf){
            parametroIMCText.text = "BMI:";
            parametroEstaturaText.text = "Height:";
            parametroEdadText.text = "Age:";
            parametroGetTex.text = "GET:";
            parametroPesoText.text = "Weight:";
            parametroAF.text = "Exercise:";
            ButtonSalirSumulacion.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Finish";
            ButtonTerminarSimulacion.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Finish";
        }


        //panelinformationProgramation
        if(panelInformationProgramation.activeSelf){ 
            title.text = "Mobile application to educate children on Obesity Prevention.";
            Autor.text = "Author: Ing. Cristian Isidro Echartea de la Rosa.";
            Director.text = "Director: Dr. Marco Aurlio Nuño Maganda.";
            coDirector.text = "Co-Director: Dr. Yahi Hernandez Mier.";             
            informationCreadores.text = "Application developed to obtain a master's degree at the Polytechnic University of Victoria.";
            GameObject.Find("closePanelErrors").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Close";
        }            


        //panel confirmation
        if(panelConfirmation.activeSelf){
            panelConfirmationText.text = "Successful operation!";
        }


        //conf panel simulacion
        if(panelConfSimulation.activeSelf){
            GameObject.Find("textEcuacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Ecuation";
            GameObject.Find("textAF").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Physical activity";
            GameObject.Find("textComidasDia").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Food/Day";
            GameObject.Find("textPeriodo").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Period";
            GameObject.Find("ButtonGuardar").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Save";


            var dropdown = GameObject.Find("DropdownEcuacion").GetComponent<Dropdown>();
            dropdown.options.Clear();

            dropdown.options.Add(new Dropdown.OptionData(){text = "Schofiled"});
            //dropdown.options.Add(new Dropdown.OptionData(){text = "Harris-Benedict"});
            //dropdown.options.Add(new Dropdown.OptionData(){text = "3"});
            dropdown.RefreshShownValue();


            //Activdad fisica
            var dropdownAF = GameObject.Find("DropdownEjercicio").GetComponent<Dropdown>();
            dropdownAF.options.Clear();
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Sedentary"});
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Slightly Active"});
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Moderately Active"});            
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Very Active"});            
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Hyperactive"});            
            dropdownAF.RefreshShownValue();     

            //Periodo   
            var dropdownPeriodo = GameObject.Find("DropdownPeriodo").GetComponent<Dropdown>();
            dropdownPeriodo.options.Clear();
            dropdownPeriodo.options.Add(new Dropdown.OptionData(){text = "Days"});
            dropdownPeriodo.options.Add(new Dropdown.OptionData(){text = "Weeks"});
            dropdownPeriodo.options.Add(new Dropdown.OptionData(){text = "Months"});          
            dropdownPeriodo.RefreshShownValue();                           
            
        }
      
        if(panelResultados.activeSelf){
            pesoPRtext.text = "Weight :";
            imcPRtext.text = "BMI :";
            estaturaPRtext.text = "height :";
            edadPRtext.text = "Age :";
        }
        ///Panel que se muestra cuando termina la simulacion        
        finSimText.text = "The simulation was successfully completed.";
        ButtonVerResultadoS.GetComponentInChildren<Text>().text  = "Acept";
        ButtonTermPS.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Finish";

        //panel informacion nutri inglish
        if(panelInfNutrimental.activeSelf){
            titlePItext.text = "Nutritional information";
            porcionText.text = "Per portion";
            cantidadPItext.text = "Amount";
            caloriasPItext.text = "Calories"; 
            proteinasPItext.text = "Protein"; 
            carbohidratosPItext.text =  "Carbohydrates"; 
            fibraPItext.text =  "Fiber"; 
            azucarPItext.text ="Sugar"; 
            GrasaPItext.text = "Fats"; 
            grasasSaturadaPItext.text =  "Saturated"; 
            grasasPolisaturadaPItext.text = "Polyunsaturated"; 
            grasaMonoinstaturadaPItext.text = "Monounsaturated"; 
            colesterolPItext.text =  "Cholesterol"; 
            sodioPItext.text =  "Sodium"; 
            potasioPItext.text = "Potassium";  
            ButtonClosePIN.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Close";           
        }  
        
        
    }
    //idioma espanol
    public void Espanol(){
        //panel donde se cargan los datos del servidor
        if(panelEspera.activeSelf){
            GameObject.Find("textoEspera").GetComponentInChildren<Text>().text = "Iniciando, espere unos minutos.";
        }

        // Ventana del menu principal        
        //aparece una excepcion cuando los componentes estan desactivados
        if(panelMenuPrincipal.activeSelf){
            //Debug.Log("Menu principal Activado");
            GameObject.Find("Configurar").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Configurar Avatar";
            GameObject.Find("confSimulacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Configurar Simulacion";
            GameObject.Find("iniciarSimulacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Comenzar Simulacion";
            GameObject.Find("salirAplicacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Salir";

            GameObject.Find("idiomaIngles").GetComponent<Image>().color = new Color32( 255,255,255, 255);
            GameObject.Find("idiomaEsp").GetComponent<Image>().color = new Color32( 63, 144, 226, 255 );                 
           // GameObject.Find("salirButtom").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Salir";
        }
        /// ConfAvatarPanel
        if(panelConfAvatar.activeSelf){
            edadText.text = "Edad";
            pesoText.text = "Peso";//Peso
            estaturaText.text = "Estatura";//Estatura
            GameObject.Find("guardarButtom").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Guardar";
            GameObject.Find("opMujer").GetComponentInChildren<Text>().text = "Hombre";
            GameObject.Find("opHombre").GetComponentInChildren<Text>().text = "Mujer";
        }
        
        //PAnel simulation information
        if(panelSimulation.activeSelf){
            parametroIMCText.text = "IMC:";
            parametroEstaturaText.text = "Estatura:";
            parametroEdadText.text = "Edad:";
            parametroGetTex.text = "GET:";
            parametroPesoText.text = "Peso:";
            parametroAF.text = "Ejercicio:";
            ButtonSalirSumulacion.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Salir";
        }


        //panelinformationProgramation
        if(panelInformationProgramation.activeSelf){ 
            title.text = "Aplicación móvil para concientizar a niños en la Prevención de la Obesidad.";
            Autor.text = "Autor: Ing. Cristian Isidro Echartea de la Rosa.";
            Director.text = "Director: Dr. Marco Aurlio Nuño Maganda.";
            coDirector.text = "Co-Director: Dr. Yahi Hernandez Mier.";            
            informationCreadores.text = "Aplicación desarrollada para obtener el grado de maestría en la Universidad Politécnica de Victoria.";
            GameObject.Find("closePanelErrors").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Cerrar";
        }            


        //panel confirmation
        if(panelConfirmation.activeSelf){
            panelConfirmationText.text = "¡Operación exitosa!";
        }


        //conf panel simulacion
        if(panelConfSimulation.activeSelf){
            GameObject.Find("textEcuacion").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Ecuacion";
            GameObject.Find("textAF").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Actividad Fisica";
            GameObject.Find("textComidasDia").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Comida/Dia";
            GameObject.Find("textPeriodo").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Periodo";
            GameObject.Find("ButtonGuardar").GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Guardar";


            var dropdown = GameObject.Find("DropdownEcuacion").GetComponent<Dropdown>();
            dropdown.options.Clear();

            dropdown.options.Add(new Dropdown.OptionData(){text = "Schofield"});
            //dropdown.options.Add(new Dropdown.OptionData(){text = "Harris-Benedict"});
            //dropdown.options.Add(new Dropdown.OptionData(){text = "3"});
            dropdown.RefreshShownValue();


            //Activdad fisica
            var dropdownAF = GameObject.Find("DropdownEjercicio").GetComponent<Dropdown>();
            dropdownAF.options.Clear();
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Sedentario"});//1.2
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Ligeramente Activo"});//1.375
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Moderadamente Activo"});//1.55            
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Muy Activo"}); //1.725           
            dropdownAF.options.Add(new Dropdown.OptionData(){text = "Hiperactivo"});//1.9    
            dropdownAF.RefreshShownValue();     

            //Periodo   
            var dropdownPeriodo = GameObject.Find("DropdownPeriodo").GetComponent<Dropdown>();
            dropdownPeriodo.options.Clear();
            dropdownPeriodo.options.Add(new Dropdown.OptionData(){text = "Dias"});
            dropdownPeriodo.options.Add(new Dropdown.OptionData(){text = "Semanas"});
            dropdownPeriodo.options.Add(new Dropdown.OptionData(){text = "Meses"});          
            dropdownPeriodo.RefreshShownValue();                                       
        }

        if(panelResultados.activeSelf){
            pesoPRtext.text = "Peso :";
            imcPRtext.text = "IMC :";
            estaturaPRtext.text = "Est :";
            edadPRtext.text = "Edad :";
        }
        ///Panel que se muestra cuando termina la simulacion        
        finSimText.text = "La simulación ha terminado con exito.";
        ButtonVerResultadoS.GetComponentInChildren<Text>().text  = "Aceptar";
        ButtonTermPS.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Salir";      

        if(panelInfNutrimental.activeSelf){
            titlePItext.text = "Información Nutircional";
            porcionText.text = "Por porción";
            cantidadPItext.text = "Cantidad";
            caloriasPItext.text = "Calorias"; 
            proteinasPItext.text = "Proteina"; 
            carbohidratosPItext.text =  "Carbohidratos"; 
            fibraPItext.text =  "Fibra"; 
            azucarPItext.text ="Azucar"; 
            GrasaPItext.text = "Grasa"; 
            grasasSaturadaPItext.text =  "Saturada"; 
            grasasPolisaturadaPItext.text = "Poliinsaturada"; 
            grasaMonoinstaturadaPItext.text = "Monoinsaturada"; 
            colesterolPItext.text =  "Colesterol"; 
            sodioPItext.text =  "Sodio"; 
            potasioPItext.text = "Potasio";  
            ButtonClosePIN.GetComponentInChildren<TMPro.TextMeshProUGUI>().text  = "Cerrar";           
        }  
        
    }    






}
