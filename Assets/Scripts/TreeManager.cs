using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    
    public GameObject Arbol;
    private GameObject Texto;
    private Dictionary<int,bool> Mejoras = new Dictionary<int, bool>();
    private GameObject BotonComprar;
    private int TFin;
    public GameObject Reg1;
    public GameObject Reg2;
    public GameObject Reg3;
    public GameObject Seg1;
    public GameObject Seg2;
    public GameObject Seg3;
    private int seleccion;

    public void Awake()
    {
        Texto=Arbol.transform.GetChild(0).GetChild(1).gameObject;
        BotonComprar = Arbol.transform.GetChild(0).GetChild(2).gameObject;
        TFin = 2;
        if(false){
            print("Base de datos");
        } else{
            print("No hay base de datos");
            for (int i = 1; i < 22; i++)
            {
                Mejoras.Add(i,false);
            }
        }
        switch(TFin){
            case 0:
                Reg1.SetActive(true);
                Reg2.SetActive(false);
                Reg3.SetActive(false);
                Mejoras[15]=true;
                Mejoras[21]=true;
                Seg1.SetActive(false);
                Seg2.SetActive(false);
                Seg3.SetActive(false);
                break;
            case 1:
                Reg1.SetActive(false);
                Reg2.SetActive(true);
                Reg3.SetActive(false);
                Mejoras[8]=true;
                Mejoras[21]=true;
                Seg1.SetActive(true);
                Seg2.SetActive(true);
                Seg3.SetActive(true);
                break;
            case 2:
                Reg1.SetActive(false);
                Reg2.SetActive(false);
                Reg3.SetActive(true);
                Mejoras[8]=true;
                Mejoras[15]=true;
                Seg1.SetActive(false);
                Seg2.SetActive(false);
                Seg3.SetActive(false);
                break;
        }
    }

    public void ShowUpgrade1()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 1000";
        if (!Mejoras[1])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 1;
        
    }
    public void ShowUpgrade2()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 2000";
        if (Mejoras[1] && !Mejoras[2])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 2;
    }
    public void ShowUpgrade3()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 3000";
        if (Mejoras[2] && !Mejoras[3])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 3;
    }
    public void ShowUpgrade4()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 4000";
        if (Mejoras[2] && !Mejoras[4])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 4;
    }
    public void ShowUpgrade5()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 5000";
        if (Mejoras[2] && !Mejoras[5])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 5;
    }
    public void ShowUpgrade6()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 6000";
        if (Mejoras[2] && !Mejoras[6])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 6;
    }
    public void ShowUpgrade7()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 7000";
        if (Mejoras[3] && Mejoras[4] && Mejoras[5] && !Mejoras[7])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 7;
    }
    public void ShowUpgrade8()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 8000";
        if (Mejoras[3] && Mejoras[4] && Mejoras[5] && !Mejoras[8])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 8;
    }
    public void ShowUpgrade9()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 9000";
        if (Mejoras[7] && Mejoras[8] && !Mejoras[9])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 9;
    }
    public void ShowUpgrade10()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 10000";
        if (Mejoras[7] && Mejoras[8] && !Mejoras[10])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 10;
    }
    public void ShowUpgrade11()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 11000";
        if (Mejoras[7] && Mejoras[8] && !Mejoras[11])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 11;
    }
    public void ShowUpgrade12()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 12000";
        if (Mejoras[7] && Mejoras[8] && Mejoras[6] && !Mejoras[12])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 12;
    }
    public void ShowUpgrade13()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 13000";
        if (Mejoras[9] && Mejoras[10] && Mejoras[11] && !Mejoras[13])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 13;
    }
    public void ShowUpgrade14()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 14000";
        if (Mejoras[9] && Mejoras[10] && Mejoras[11] && !Mejoras[14])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 14;
    }
    public void ShowUpgrade15()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 15000";
        if (Mejoras[9] && Mejoras[10] && Mejoras[11] && !Mejoras[15])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 15;
    }
    public void ShowUpgrade16()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 16000";
        if (Mejoras[13] && Mejoras[14] && Mejoras[15] && !Mejoras[16])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 16;
    }
    public void ShowUpgrade17()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 17000";
        if (Mejoras[13] && Mejoras[14] && Mejoras[15] && !Mejoras[17])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 17;
    }
    public void ShowUpgrade18()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 18000";
        if (Mejoras[13] && Mejoras[14] && Mejoras[15]  && !Mejoras[18])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 18;
    }
    public void ShowUpgrade19()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 19000";
        if (Mejoras[13] && Mejoras[14] && Mejoras[15] && Mejoras[12] && !Mejoras[19])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 19;
    }
    public void ShowUpgrade20()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 20000";
        if (Mejoras[16] && Mejoras[17] && Mejoras[18] && !Mejoras[20])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 20;
    }
    public void ShowUpgrade21()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Costo: 21000";
        if (Mejoras[16] && Mejoras[17] && Mejoras[18] && !Mejoras[21])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 21;
    }

    public void Comprar(){
        Mejoras[seleccion] = true;
        BotonComprar.SetActive(false);
    }
     
       
}
