using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ClassConfig{
    public class Configuracion
    {
            public int Ecuacion;
            public int Comidas_dia;
            public int periodo;
            public int periodo_tiempo;
            public int tipo_ejercicio;
            public int Edad;
            public int Meses;
            public float Peso;
            public float pesoSlider;
            public float Estatura;
            public float EstaturaSlider;
            public string Sexo;
            public float IMC;
            public double GET;
            public string idioma;

            public Configuracion(){
                this.Ecuacion = 0;
                this.Comidas_dia = 0;
                this.periodo = periodo;
                this.periodo_tiempo = 0;
                this.tipo_ejercicio = 0;
                this.Edad = 0;
                this.Meses = 0;
                this.Peso = 0F;
                this.pesoSlider = 0F;
                this.Estatura = 0F;
                this.EstaturaSlider = 0F;
                this.Sexo = "";
                this.IMC = 0F;
                this.GET = 0F;	
                this.idioma = "";                
            }
            public Configuracion(int Ecuacion, int Comidas_dia, int periodo, int periodo_tiempo, int tipo_ejercicio, int Edad, int Meses, float Peso, float pesoSlider, float Estatura,float EstaturaSlider,string Sexo, float IMC,double GET, string idioma){
                this.Ecuacion = Ecuacion;
                this.Comidas_dia = Comidas_dia;
                this.periodo = periodo;
                this.periodo_tiempo = periodo_tiempo;
                this.tipo_ejercicio = tipo_ejercicio;
                this.Edad = Edad;
                this.Meses = Meses;
                this.Peso = Peso;
                this.pesoSlider = pesoSlider;
                this.Estatura = Estatura;
                this.EstaturaSlider = EstaturaSlider;
                this.Sexo = Sexo;
                this.IMC = IMC;
                this.GET = GET;	
                this.idioma = idioma;				
            }

    }
}