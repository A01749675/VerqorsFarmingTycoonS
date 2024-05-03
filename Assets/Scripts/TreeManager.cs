using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class TreeManager : MonoBehaviour
{
    
    public GameObject arbol;
    private GameObject texto;
    public Dictionary<int,bool> mejoras = new Dictionary<int, bool>();
    private GameObject botonComprar;
    private GameObject botonInfo;
    private int tFin;
    public GameObject reg1;
    public GameObject reg2;
    public GameObject reg3;
    public GameObject seg1;
    public GameObject seg2;
    public GameObject seg3;
    private int seleccion;
    public Image imagen;
    public Sprite tractor;
    public Sprite empleado;
    public Sprite tierra;
    public Sprite Aspersor;
    public Sprite Tanque;
    public Sprite Agua;
    public Sprite Seguro;
    public Sprite IMRegen;
    public GameObject celular;
    public ObtenerDatos obtenerDatos;
    [SerializeField]
    private UserController userController;

    [SerializeField]
    private GameObject boton1;
    [SerializeField]
    private GameObject boton2;
    [SerializeField]
    private GameObject boton3;
    [SerializeField]
    private GameObject boton4;
    [SerializeField]
    private GameObject boton5;
    [SerializeField]
    private GameObject boton6;
    [SerializeField]
    private GameObject boton7;
    [SerializeField]
    private GameObject boton8;
    [SerializeField]
    private GameObject boton9;
    [SerializeField]
    private GameObject boton10;
    [SerializeField]
    private GameObject boton11;
    [SerializeField]
    private GameObject boton12;
    [SerializeField]
    private GameObject boton13;
    [SerializeField]
    private GameObject boton14;
    [SerializeField]
    private GameObject boton15;
    [SerializeField]
    private GameObject boton16;
    [SerializeField]
    private GameObject boton17;
    [SerializeField]
    private GameObject boton18;
    [SerializeField]
    private GameObject boton19;
    [SerializeField]
    private GameObject boton20;
    [SerializeField]
    private GameObject boton21;
    private Dictionary<int,int> costos = new Dictionary<int, int>();

    public bool update = false;
    // Regresa el valor de las mejoras
    public bool getMejoras(int mejora){
        return mejoras[mejora];
    }

    public void Awake(){
        // Inicializa el diccionario de mejoras
        for (int i = 1; i < 22; i++)
            {
                mejoras.Add(i, false);
            }
        print("TreeManager awake");
    }

    public void Start()
    {
        // Se obtienen los objetos de la escena
        texto=arbol.transform.GetChild(0).GetChild(1).gameObject;
        botonComprar = arbol.transform.GetChild(0).GetChild(2).gameObject;
        botonInfo = arbol.transform.GetChild(0).GetChild(3).gameObject;
        // Se inicializan los costos de las mejoras
        costos.Add(1,300000);
        costos.Add(2,400000);
        costos.Add(3,225000);
        costos.Add(4,350000);
        costos.Add(5,100000);
        costos.Add(6,200000);
        costos.Add(7,500000);
        costos.Add(8,300000);
        costos.Add(9,300000);
        costos.Add(10,400000);
        costos.Add(11,200000);
        costos.Add(12,300000);
        costos.Add(13,700000);
        costos.Add(14,500000);
        costos.Add(15,400000);
        costos.Add(16,400000);
        costos.Add(17,500000);
        costos.Add(18,250000);
        costos.Add(19,450000);
        costos.Add(20,800000);
        costos.Add(21,500000);
        // Se pone el arbol del financimiento del usuario
        if(userController.user_data.ContainsKey("financiamiento")){
            tFin = userController.GetParameter("financiamiento");
        }
        else{
            tFin = 1;
        }

        if (obtenerDatos.success)
        {
            print("Datos obtenidos para el tree manager");
            foreach (Mejoras mejora in obtenerDatos.mejoras)
            {
                mejoras[mejora.id_mejora] = mejora.estado;
            }
            update = true;
            print("mejoras obtenidas");
        }
        else
        {
            print("Datos por default");
            
        }
        
        switch(tFin){
            case 1:
                reg1.SetActive(true);
                reg2.SetActive(false);
                reg3.SetActive(false);
                mejoras[15]=true;
                mejoras[21]=true;
                seg1.SetActive(false);
                seg2.SetActive(false);
                seg3.SetActive(false);
                celular.SetActive(true);
                break;
            case 2:
                reg1.SetActive(false);
                reg2.SetActive(true);
                reg3.SetActive(false);
                mejoras[8]=true;
                mejoras[21]=true;
                seg1.SetActive(true);
                seg2.SetActive(true);
                seg3.SetActive(true);
                celular.SetActive(false);
                break;
            case 3:
                reg1.SetActive(false);
                reg2.SetActive(false);
                reg3.SetActive(true);
                mejoras[8]=true;
                mejoras[15]=true;
                seg1.SetActive(false);
                seg2.SetActive(false);
                seg3.SetActive(false);
                celular.SetActive(false);
                break;
        }
    }
    // A continuación se definen los métodos para mostrar las  distintas mejoras
    public void ShowUpgrade1()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá ver el agua que tiene cada parcela. Costo: $300,000";
        imagen.sprite = Agua;
        if (!mejoras[1])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        
        }
        seleccion = 1;
        botonInfo.SetActive(false);
        
    }
    public void ShowUpgrade2()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 4 parcelas más para plantar más cultivos. Costo: $400,000";
        imagen.sprite = tierra;
        if (mejoras[1] && !mejoras[2])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        
        }
        seleccion = 2;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade3()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 3 de tus parcelas. Costo: $225,000";
        imagen.sprite = empleado;
        if (mejoras[2] && !mejoras[3])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        
        }
        seleccion = 3;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade4()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar unos tractores, los cuales te ayudarán a recoger los cultivos de 3 parcelas. Costo: $350,000";
        imagen.sprite = tractor;
        if (mejoras[2] && !mejoras[4])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        
        }
        seleccion = 4;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade5()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar un tanque de agua, en el cual se podrá almacenar agua en caso de sequía. Costo: $100,000";
        imagen.sprite = Tanque;
        if (mejoras[2] && !mejoras[5])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        
        }
        seleccion = 5;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade6()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un seguro para tus cultivos. Si se mueren tus cultivos recibirás un 20% de su valor. Costo: $200,000";
        imagen.sprite = Seguro;
        if (mejoras[2] && !mejoras[6])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 6;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade7()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá tener riego automático en 3 de tus parcelas. Esto será bastante útil en sequías. Costo: $500,000";
        imagen.sprite = Aspersor;
        if (mejoras[3] && mejoras[4] && mejoras[5] && !mejoras[7])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 7;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade8()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: $300,000";
        imagen.sprite = IMRegen;
        if (mejoras[3] && mejoras[4] && mejoras[5] && !mejoras[8])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 8;
        botonInfo.SetActive(true);
    }
    public void ShowUpgrade9()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 2 parcelas más. Costo: $300,000";
        imagen.sprite = empleado;
        if (mejoras[7] && mejoras[8] && !mejoras[9])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 9;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade10()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar unos tractores, los cuales te ayudarán a recoger los cultivos de 2 parcelas más. Costo: $400,000";
        imagen.sprite = tractor;
        if (mejoras[7] && mejoras[8] && !mejoras[10])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 10;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade11()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora incrementa la capacidad del tanque de agua. Costo: $200,000";
        imagen.sprite = Tanque;
        if (mejoras[7] && mejoras[8] && !mejoras[11])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 11;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade12()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un mejor seguro para tus cultivos. Si se mueren tus cultivos recibirás un 35% de su valor. Costo: $300,000";
        imagen.sprite = Seguro;
        if (mejoras[7] && mejoras[8] && mejoras[6] && !mejoras[12])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 12;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade13()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 7 parcelas más para plantar más cultivos. Costo: $700,000";
        imagen.sprite = tierra;
        if (mejoras[9] && mejoras[10] && mejoras[11] && !mejoras[13])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 13;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade14()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá tener riego automático en 2 de tus parcelas. Esto será bastante útil en sequías. Costo: $500,000";
        imagen.sprite = Aspersor;
        if (mejoras[9] && mejoras[10] && mejoras[11] && !mejoras[14])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 14;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade15()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: $400,000";
        imagen.sprite = IMRegen;
        if (mejoras[9] && mejoras[10] && mejoras[11] && !mejoras[15])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 15;
        botonInfo.SetActive(true);
    }
    public void ShowUpgrade16()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 4 parcelas más. Costo: $400,000";
        imagen.sprite = empleado;
        if (mejoras[13] && mejoras[14] && mejoras[15] && !mejoras[16])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 16;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade17()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar unos tractores, los cuales te ayudará a recoger los cultivos de 4 parcelas más. Costo: $500,000";
        imagen.sprite = tractor;
        if (mejoras[13] && mejoras[14] && mejoras[15] && !mejoras[17])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 17;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade18()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora incrementa la capacidad del tanque de agua. Costo: $250,000";
        imagen.sprite = Tanque;
        if (mejoras[13] && mejoras[14] && mejoras[15]  && !mejoras[18])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 18;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade19()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un mejor seguro para tus cultivos. Si se mueren tus cultivos recibirás un 50% de su valor. Costo: $450,000";
        imagen.sprite = Seguro;
        if (mejoras[13] && mejoras[14] && mejoras[15] && mejoras[12] && !mejoras[19])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 19;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade20()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 8 parcelas más para plantar más cultivos. Costo: $800,000";
        imagen.sprite = tierra;
        if (mejoras[16] && mejoras[17] && mejoras[18] && !mejoras[20])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 20;
        botonInfo.SetActive(false);
    }
    public void ShowUpgrade21()
    {
        arbol.transform.GetChild(0).gameObject.SetActive(true);
        texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: $500,000";
        imagen.sprite = IMRegen;
        if (mejoras[16] && mejoras[17] && mejoras[18] && !mejoras[21])
        {
            botonComprar.SetActive(true);
        } else{
            botonComprar.SetActive(false);
        }
        seleccion = 21;
        botonInfo.SetActive(true);
    }
    public void MasInformacion(){
        Application.OpenURL("https://verqor.com/blog/48/agricultura-regenerativa-que-es-y-cuales-son-sus-beneficios");
    }

    public void UpdateColors(){
        if(mejoras[1]){
            boton1.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton1.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[2] || !mejoras[1]){
            boton2.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton2.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[3] || !mejoras[2]){
            boton3.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton3.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[4] || !mejoras[2]){
            boton4.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton4.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[5] || !mejoras[2]){
            boton5.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton5.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[6] || !mejoras[2]){
            boton6.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton6.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[7] || !mejoras[3] || !mejoras[4] || !mejoras[5]){
            boton7.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton7.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[8] || !mejoras[3] || !mejoras[4] || !mejoras[5]){
            boton8.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton8.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[9] || !mejoras[7] || !mejoras[8]){
            boton9.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton9.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[10] || !mejoras[7] || !mejoras[8]){
            boton10.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton10.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[11] || !mejoras[7] || !mejoras[8]){
            boton11.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton11.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[12] || !mejoras[7] || !mejoras[8] || !mejoras[6]){
            boton12.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton12.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[13] || !mejoras[9] || !mejoras[10] || !mejoras[11]){
            boton13.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton13.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[14] || !mejoras[9] || !mejoras[10] || !mejoras[11]){
            boton14.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton14.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[15] || !mejoras[9] || !mejoras[10] || !mejoras[11]){
            boton15.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton15.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[16] || !mejoras[13] || !mejoras[14] || !mejoras[15]){
            boton16.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton16.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[17] || !mejoras[13] || !mejoras[14] || !mejoras[15]){
            boton17.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton17.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[18] || !mejoras[13] || !mejoras[14] || !mejoras[15]){
            boton18.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton18.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[19] || !mejoras[13] || !mejoras[14] || !mejoras[15] || !mejoras[12]){
            boton19.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton19.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[20] || !mejoras[16] || !mejoras[17] || !mejoras[18]){
            boton20.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton20.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(mejoras[21] || !mejoras[16] || !mejoras[17] || !mejoras[18]){
            boton21.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton21.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        
    }

    //Se compra la mejora y se actualiza el capital
    public void Comprar(){
        if(userController.GetCapital() >= costos[seleccion]){
            mejoras[seleccion] = true;
            userController.UpdateCapital(-costos[seleccion]);
            botonComprar.SetActive(false);
            update = true;
            UpdateColors();
        }
    }
}
