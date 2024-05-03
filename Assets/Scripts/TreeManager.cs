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
    private Dictionary<int,int> Costos = new Dictionary<int, int>();

    public bool update = false;
    // Regresa el valor de las mejoras
    public bool getMejoras(int mejora){
        return Mejoras[mejora];
    }

    public void Awake(){
        // Inicializa el diccionario de mejoras
        for (int i = 1; i < 22; i++)
            {
                Mejoras.Add(i, false);
            }
        print("TreeManager awake");
    }

    public void Start()
    {
        // Se obtienen los objetos de la escena
        Texto=Arbol.transform.GetChild(0).GetChild(1).gameObject;
        BotonComprar = Arbol.transform.GetChild(0).GetChild(2).gameObject;
        BotonInfo = Arbol.transform.GetChild(0).GetChild(3).gameObject;
        // Se inicializan los costos de las mejoras
        Costos.Add(1,300000);
        Costos.Add(2,400000);
        Costos.Add(3,225000);
        Costos.Add(4,350000);
        Costos.Add(5,100000);
        Costos.Add(6,200000);
        Costos.Add(7,500000);
        Costos.Add(8,300000);
        Costos.Add(9,300000);
        Costos.Add(10,400000);
        Costos.Add(11,200000);
        Costos.Add(12,300000);
        Costos.Add(13,700000);
        Costos.Add(14,500000);
        Costos.Add(15,400000);
        Costos.Add(16,400000);
        Costos.Add(17,500000);
        Costos.Add(18,250000);
        Costos.Add(19,450000);
        Costos.Add(20,800000);
        Costos.Add(21,500000);
        // Se pone el arbol del financimiento del usuario
        if(userController.user_data.ContainsKey("financiamiento")){
            TFin = userController.GetParameter("financiamiento");
        }
        else{
            TFin = 1;
        }

        if (obtenerDatos.success)
        {
            print("Datos obtenidos para el tree manager");
            foreach (Mejoras mejora in obtenerDatos.mejoras)
            {
                Mejoras[mejora.id_mejora] = mejora.estado;
            }
            update = true;
            print("Mejoras obtenidas");
        }
        else
        {
            print("Datos por default");
            
        }
        
        switch(TFin){
            case 1:
                Reg1.SetActive(true);
                Reg2.SetActive(false);
                Reg3.SetActive(false);
                Mejoras[15]=true;
                Mejoras[21]=true;
                Seg1.SetActive(false);
                Seg2.SetActive(false);
                Seg3.SetActive(false);
                celular.SetActive(true);
                break;
            case 2:
                Reg1.SetActive(false);
                Reg2.SetActive(true);
                Reg3.SetActive(false);
                Mejoras[8]=true;
                Mejoras[21]=true;
                Seg1.SetActive(true);
                Seg2.SetActive(true);
                Seg3.SetActive(true);
                celular.SetActive(false);
                break;
            case 3:
                Reg1.SetActive(false);
                Reg2.SetActive(false);
                Reg3.SetActive(true);
                Mejoras[8]=true;
                Mejoras[15]=true;
                Seg1.SetActive(false);
                Seg2.SetActive(false);
                Seg3.SetActive(false);
                celular.SetActive(false);
                break;
        }
    }
    // A continuación se definen los métodos para mostrar las  distintas mejoras
    public void ShowUpgrade1()
    {
        Arbol.transform.GetChild(0).gameObject.SetActive(true);
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá ver el agua que tiene cada parcela. Costo: $300,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 4 parcelas más para plantar más cultivos. Costo: $400,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 3 de tus parcelas. Costo: $225,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar unos tractores, los cuales te ayudarán a recoger los cultivos de 3 parcelas. Costo: $350,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar un tanque de agua, en el cual se podrá almacenar agua en caso de sequía. Costo: $100,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un seguro para tus cultivos. Si se mueren tus cultivos recibirás un 20% de su valor. Costo: $200,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá tener riego automático en 3 de tus parcelas. Esto será bastante útil en sequías. Costo: $500,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: $300,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 2 parcelas más. Costo: $300,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar unos tractores, los cuales te ayudarán a recoger los cultivos de 2 parcelas más. Costo: $400,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora incrementa la capacidad del tanque de agua. Costo: $200,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un mejor seguro para tus cultivos. Si se mueren tus cultivos recibirás un 35% de su valor. Costo: $300,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 7 parcelas más para plantar más cultivos. Costo: $700,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá tener riego automático en 2 de tus parcelas. Esto será bastante útil en sequías. Costo: $500,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: $400,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá contratar empleados y te ayudarán a cultivar en 4 parcelas más. Costo: $400,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá comprar unos tractores, los cuales te ayudará a recoger los cultivos de 4 parcelas más. Costo: $500,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora incrementa la capacidad del tanque de agua. Costo: $250,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Está mejora te permitirá comprar un mejor seguro para tus cultivos. Si se mueren tus cultivos recibirás un 50% de su valor. Costo: $450,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te permitirá conseguir 8 parcelas más para plantar más cultivos. Costo: $800,000";
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
        Texto.GetComponent<TextMeshProUGUI>().text = "Esta mejora te dará los beneficios de la agricultura regenerativa. Dale click al botón para conocer sobre agricultura regenerativa. Costo: $500,000";
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
    public void MasInformacion(){
        Application.OpenURL("https://verqor.com/blog/48/agricultura-regenerativa-que-es-y-cuales-son-sus-beneficios");
    }

    public void UpdateColors(){
        if(Mejoras[1]){
            boton1.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton1.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[2] || !Mejoras[1]){
            boton2.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton2.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[3] || !Mejoras[2]){
            boton3.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton3.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[4] || !Mejoras[2]){
            boton4.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton4.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[5] || !Mejoras[2]){
            boton5.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton5.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[6] || !Mejoras[2]){
            boton6.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton6.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[7] || !Mejoras[3] || !Mejoras[4] || !Mejoras[5]){
            boton7.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton7.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[8] || !Mejoras[3] || !Mejoras[4] || !Mejoras[5]){
            boton8.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton8.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[9] || !Mejoras[7] || !Mejoras[8]){
            boton9.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton9.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[10] || !Mejoras[7] || !Mejoras[8]){
            boton10.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton10.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[11] || !Mejoras[7] || !Mejoras[8]){
            boton11.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton11.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[12] || !Mejoras[7] || !Mejoras[8] || !Mejoras[6]){
            boton12.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton12.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[13] || !Mejoras[9] || !Mejoras[10] || !Mejoras[11]){
            boton13.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton13.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[14] || !Mejoras[9] || !Mejoras[10] || !Mejoras[11]){
            boton14.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton14.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[15] || !Mejoras[9] || !Mejoras[10] || !Mejoras[11]){
            boton15.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton15.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[16] || !Mejoras[13] || !Mejoras[14] || !Mejoras[15]){
            boton16.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton16.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[17] || !Mejoras[13] || !Mejoras[14] || !Mejoras[15]){
            boton17.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton17.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[18] || !Mejoras[13] || !Mejoras[14] || !Mejoras[15]){
            boton18.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton18.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[19] || !Mejoras[13] || !Mejoras[14] || !Mejoras[15] || !Mejoras[12]){
            boton19.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton19.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[20] || !Mejoras[16] || !Mejoras[17] || !Mejoras[18]){
            boton20.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton20.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        if(Mejoras[21] || !Mejoras[16] || !Mejoras[17] || !Mejoras[18]){
            boton21.GetComponent<Image>().color = new Color(1,1,1,0.5f);
        } else{
            boton21.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        
    }

    //Se compra la mejora y se actualiza el capital
    public void Comprar(){
        if(userController.GetCapital() >= Costos[seleccion]){
            Mejoras[seleccion] = true;
            userController.UpdateCapital(-Costos[seleccion]);
            BotonComprar.SetActive(false);
            update = true;
            UpdateColors();
        }
    }
       
}
