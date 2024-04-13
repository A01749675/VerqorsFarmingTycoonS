using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class TreeManager : MonoBehaviour
{
    
    public GameObject Arbol;
    private GameObject Texto;
    public Dictionary<int,bool> Mejoras = new Dictionary<int, bool>();
    private GameObject BotonComprar;
    private GameObject BotonInfo;
    private int TFin;
    public GameObject Reg1;
    public GameObject Reg2;
    public GameObject Reg3;
    public GameObject Seg1;
    public GameObject Seg2;
    public GameObject Seg3;
    private int seleccion;
    public Image Imagen;
    public Sprite Tractor;
    public Sprite Empleado;
    public Sprite Tierra;
    public Sprite Aspersor;
    public Sprite Tanque;
    public Sprite Agua;
    public Sprite Seguro;
    public Sprite IMRegen;



    public void Awake()
    {
        Texto=Arbol.transform.GetChild(0).GetChild(1).gameObject;
        BotonComprar = Arbol.transform.GetChild(0).GetChild(2).gameObject;
        BotonInfo = Arbol.transform.GetChild(0).GetChild(3).gameObject;
        TFin = 1;
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá ver el agua que tiene cada parcela. Costo: 1000";
        Imagen.sprite = Agua;
        if (!Mejoras[1])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 1;
        BotonInfo.SetActive(false);
        
    }
    public void ShowUpgrade2()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 4 parcelas más para plantar más cultivos. Costo: 2000";
        Imagen.sprite = Tierra;
        if (Mejoras[1] && !Mejoras[2])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 2;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade3()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 3 de tus parcelas. Costo: 3000";
        Imagen.sprite = Empleado;
        if (Mejoras[2] && !Mejoras[3])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 3;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade4()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar un tractor, el cual te ayudará a recoger los cultivos de 3 parcelas. Costo: 4000";
        Imagen.sprite = Tractor;
        if (Mejoras[2] && !Mejoras[4])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 4;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade5()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar un tanque de agua, en el cual se podrá almacenar agua en caso de sequía. Costo: 5000";
        Imagen.sprite = Tanque;
        if (Mejoras[2] && !Mejoras[5])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        
        }
        seleccion = 5;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade6()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un seguro para tus cultivos. Si se mueren tus cultivos recibirás un 20% de su valor. Costo: 6000";
        Imagen.sprite = Seguro;
        if (Mejoras[2] && !Mejoras[6])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 6;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade7()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá tener riego automático en 3 de tus parcelas. Esto será bastante útil en sequías. Costo: 7000";
        Imagen.sprite = Aspersor;
        if (Mejoras[3] && Mejoras[4] && Mejoras[5] && !Mejoras[7])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 7;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade8()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: 8000";
        Imagen.sprite = IMRegen;
        if (Mejoras[3] && Mejoras[4] && Mejoras[5] && !Mejoras[8])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 8;
        BotonInfo.SetActive(true);
    }
    public void ShowUpgrade9()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 2 parcelas más. Costo: 9000";
        Imagen.sprite = Empleado;
        if (Mejoras[7] && Mejoras[8] && !Mejoras[9])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 9;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade10()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar un tractor, el cual te ayudará a recoger los cultivos de 2 parcelas más. Costo: 10000";
        Imagen.sprite = Tractor;
        if (Mejoras[7] && Mejoras[8] && !Mejoras[10])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 10;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade11()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora incrementa la capacidad del tanque de agua. Costo: 11000";
        Imagen.sprite = Tanque;
        if (Mejoras[7] && Mejoras[8] && !Mejoras[11])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 11;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade12()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un mejor seguro para tus cultivos. Si se mueren tus cultivos recibirás un 35% de su valor. Costo: 12000";
        Imagen.sprite = Seguro;
        if (Mejoras[7] && Mejoras[8] && Mejoras[6] && !Mejoras[12])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 12;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade13()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 7 parcelas más para plantar más cultivos. Costo: 13000";
        Imagen.sprite = Tierra;
        if (Mejoras[9] && Mejoras[10] && Mejoras[11] && !Mejoras[13])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 13;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade14()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá tener riego automático en 2 de tus parcelas. Esto será bastante útil en sequías. Costo: 14000";
        Imagen.sprite = Aspersor;
        if (Mejoras[9] && Mejoras[10] && Mejoras[11] && !Mejoras[14])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 14;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade15()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: 15000";
        Imagen.sprite = IMRegen;
        if (Mejoras[9] && Mejoras[10] && Mejoras[11] && !Mejoras[15])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 15;
        BotonInfo.SetActive(true);
    }
    public void ShowUpgrade16()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 4 parcelas más. Costo: 16000";
        Imagen.sprite = Empleado;
        if (Mejoras[13] && Mejoras[14] && Mejoras[15] && !Mejoras[16])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 16;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade17()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar un tractor, el cual te ayudará a recoger los cultivos de 4 parcelas más. Costo: 17000";
        Imagen.sprite = Tractor;
        if (Mejoras[13] && Mejoras[14] && Mejoras[15] && !Mejoras[17])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 17;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade18()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora incrementa la capacidad del tanque de agua. Costo: 18000";
        Imagen.sprite = Tanque;
        if (Mejoras[13] && Mejoras[14] && Mejoras[15]  && !Mejoras[18])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 18;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade19()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un mejor seguro para tus cultivos. Si se mueren tus cultivos recibirás un 50% de su valor. Costo: 19000";
        Imagen.sprite = Seguro;
        if (Mejoras[13] && Mejoras[14] && Mejoras[15] && Mejoras[12] && !Mejoras[19])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 19;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade20()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 8 parcelas más para plantar más cultivos. Costo: 20000";
        Imagen.sprite = Tierra;
        if (Mejoras[16] && Mejoras[17] && Mejoras[18] && !Mejoras[20])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 20;
        BotonInfo.SetActive(false);
    }
    public void ShowUpgrade21()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: 21000";
        Imagen.sprite = IMRegen;
        if (Mejoras[16] && Mejoras[17] && Mejoras[18] && !Mejoras[21])
        {
            BotonComprar.SetActive(true);
        } else{
            BotonComprar.SetActive(false);
        }
        seleccion = 21;
        BotonInfo.SetActive(true);
    }

    public void Comprar(){
        Mejoras[seleccion] = true;
        BotonComprar.SetActive(false);
    }
       
}
