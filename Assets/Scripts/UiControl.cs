using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Image = UnityEngine.UI.Image;

public class UiControl : MonoBehaviour
{
    public GameObject PanelOpciones;
    public GameObject UsuarioNombre;
    public GameObject Financiamiento;
    public GameObject Produccion;
    public GameObject Rankings;
    public GameObject Ajustes;
    public GameObject Celular;
    public GameObject Mercado;
    public GameObject Inventario;
    public GameObject Deuda;

//Inventario
    public GameObject STrigoN;
    public GameObject SMaizN;
    public GameObject SChileN;
    public GameObject SAguacateN;
    public GameObject SCafeN;
    public GameObject STomateN;

    public GameObject TrigoN;
    public GameObject MaizN;
    public GameObject ChileN;
    public GameObject AguacateN;
    public GameObject CafeN;
    public GameObject TomateN;

//FinanceManager
    public FinanceManager financeManager;

//User Controller
    public UserController UserController;

//Deuda
    public GameObject DeudaTxt;
    public GameObject DeudaTime;

// Mercado 

    public GameObject MenuVender;
    public GameObject MenuComprar;
    private int maizv=0;
    public GameObject MaizVcontador;
    private int trigov=0;
    public GameObject TrigoVcontador;
    private int chilev=0;
    public GameObject ChileVcontador;
    private int aguacatev=0;
    public GameObject AguacateVcontador;
    private int cafev=0;
    public GameObject CafeVcontador;
    private int tomatev=0;
    public GameObject TomateVcontador;
    public GameObject precioTrigo;
    public GameObject precioMaiz;
    public GameObject precioChile;
    public GameObject precioAguacate;
    public GameObject precioCafe;
    public GameObject precioTomate;

    public GameObject BigPeriodico;

    public GameObject MenuPlantar;
    public int typecrop=0;

// Objeto Sonido y efectos
    public GameObject musicMute;
    public GameObject efectMute;
    public GameObject musicSlidebar;
    public GameObject efectSlidebar;
    public Sprite On;
    public Sprite Off;

// Objeto EviarDatos
    public EnviarDatos enviardatos;

// Mercado Comprar

    public MarketManager marketManager;
    public GameObject trigoPrice;
    public GameObject maizPrice;
    public GameObject chilePrice;
    public GameObject aguacatePrice;
    public GameObject cafePrice;
    public GameObject tomatePrice;
    public GameObject trigoCantidad;
    public GameObject maizCantidad;
    public GameObject chileCantidad;
    public GameObject aguacateCantidad;
    public GameObject cafeCantidad;
    public GameObject tomateCantidad;

// Mercado Vender 2
    public GameObject MenuVender2;
    public GameObject MenuComprar2;
    public GameObject precioTrigo2;
    public GameObject precioMaiz2;
    public GameObject precioChile2;
    public GameObject precioAguacate2;
    public GameObject precioCafe2;
    public GameObject precioTomate2;
    public GameObject TrigoVcontador2;
    public GameObject MaizVcontador2;
    public GameObject ChileVcontador2;
    public GameObject AguacateVcontador2;
    public GameObject CafeVcontador2;
    public GameObject TomateVcontador2;
    public GameObject trigoPrice2;
    public GameObject maizPrice2;
    public GameObject chilePrice2;
    public GameObject aguacatePrice2;
    public GameObject cafePrice2;
    public GameObject tomatePrice2;
    public GameObject trigoCantidad2;
    public GameObject maizCantidad2;
    public GameObject chileCantidad2;
    public GameObject aguacateCantidad2;
    public GameObject cafeCantidad2;
    public GameObject tomateCantidad2;

    public GameObject Dinero;
    public GameObject Arbol;

    public CropManager cropManager;
    private bool flagUsuario=false;
    private bool flagFinanciamiento=false;
    private bool flagProduccion=false;
    private bool flagRankings=false;
    private bool flagCelular=false;
    private bool flagMercado=false;
    private bool flagInventario=false;
    private bool flagDeuda=false;
    private bool flagMenuPlantar=false;
    public bool flagHerramienta=false;
    public bool flagRegadera=false;

    private bool flagMenuVender=false;
    private bool flagMenuComprar=false;
    private bool flagBigPeriodico=false;
    private bool flagArbol=false;
    private bool flagMenuVender2=false;
    private bool flagMenuComprar2=false;

    private bool flagAjustes=false;

    //Movimiento camára

    public CameraMovement cameraMovement;
    
    
    

    //Al dar click al boton de opciones se abre el panel PanelOpciones
    public void OpenOpciones()
    {
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagHerramienta=false;
        PanelOpciones.SetActive(true);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        Time.timeScale = 0;
        cameraMovement.enabled = false;
    }

    public void CloseOpciones()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        PanelOpciones.SetActive(false);
        flagAjustes=false;
        Ajustes.SetActive(false);
        Time.timeScale = 1;
        cameraMovement.enabled = true;
    }

    public void Usuario()
    {
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagAjustes=false;
        Ajustes.SetActive(false);
        if(flagUsuario){
            flagUsuario=false;
            UsuarioNombre.SetActive(false);
        }else{
            flagUsuario=true;
            UsuarioNombre.SetActive(true);
        }
        
    }
    public void ShowFinanciamiento()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagAjustes=false;
        Ajustes.SetActive(false);
        if(flagFinanciamiento){
            flagFinanciamiento=false;
            Financiamiento.SetActive(false);
        }else{
            flagFinanciamiento=true;
            Financiamiento.SetActive(true);
        }
        
    }
    public void ShowProduccion()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagAjustes=false;
        Ajustes.SetActive(false);
        if(flagProduccion){
            flagProduccion=false;
            Produccion.SetActive(false);
        }else{
            flagProduccion=true;
            Produccion.SetActive(true);
        }
        
    }
    public void ShowRankings()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagAjustes=false;
        Ajustes.SetActive(false);
        if(flagRankings){
            flagRankings=false;
            Rankings.SetActive(false);
        }else{
            flagRankings=true;
            Rankings.SetActive(true);
        }
    }

    public void ShowAjustes()
    {
        flagUsuario=false;
        UsuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        Financiamiento.SetActive(false);
        flagProduccion=false;
        Produccion.SetActive(false);
        flagRankings=false;
        Rankings.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        if(flagAjustes){
            flagAjustes=false;
            Ajustes.SetActive(false);
        }else{
            flagAjustes=true;
            Ajustes.SetActive(true);
        }
    }

    public void SaveGame()
    {
        Debug.Log("Guardando...");
        enviardatos.Guardar();
        Debug.Log("Guardado exitoso.");
        }
    public void CloseGame()
    {
        Debug.Log("Guardando...");
        enviardatos.GuardarySalir();
        Debug.Log("Guardado exitoso.");
    }

    public void ShowCelular()
    {
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        if(flagCelular){
            flagCelular=false;
            Celular.SetActive(false);
        }else{
            flagCelular=true;
            Celular.SetActive(true);
        }
    }
    public void ShowMercado()
    {
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false;
        typecrop=0; 
        flagCelular=false;
        Celular.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        if(flagMercado){
            flagMercado=false;
            Mercado.SetActive(false);
        }else{
            flagMercado=true;
            Mercado.SetActive(true);
        }
    }
    public void ShowInventario()
    {
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        if(flagInventario){
            flagInventario=false;
            Inventario.SetActive(false);
        }else{
            flagInventario=true;
            Inventario.SetActive(true);
            STrigoN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(1).ToString();
            SMaizN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(2).ToString();
            SChileN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(6).ToString();
            SAguacateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(4).ToString();
            SCafeN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(5).ToString();
            STomateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(3).ToString();
            TrigoN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(1).ToString();
            MaizN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(2).ToString();
            ChileN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(6).ToString();
            AguacateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(4).ToString();
            CafeN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(5).ToString();
            TomateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(3).ToString();
        }
    }
    public void ShowDeuda()
    {
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false;
        typecrop=0; 
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        Arbol.SetActive(false);
        if(flagDeuda){
            flagDeuda=false;
            Deuda.SetActive(false);
        }else{
            flagDeuda=true;
            Deuda.SetActive(true);
            DeudaTxt.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetDebt().ToString();
            DeudaTime.GetComponent<TextMeshProUGUI>().text = financeManager.GetTimetoPay().ToString() + " días";
        }
    }

    public void pagarDeuda(){
        if(UserController.GetCapital()>=UserController.GetDebt()){
            UserController.PayDebt(UserController.GetDebt());
            DeudaTxt.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetDebt().ToString();
            Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
        }
    }

    public void hoz(){
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagRegadera=false;
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        if(flagMenuPlantar){
            flagMenuPlantar=false;
            MenuPlantar.SetActive(false);
            flagHerramienta=false; 
            Cursor.visible = true;
            typecrop=0;
        }else{
            flagMenuPlantar=true;
            MenuPlantar.SetActive(true);
            //Cursor.visible = false;
        }
    }

    public void SelectCrop(string cropType){
        switch(cropType){
            case "1":
                if (typecrop==1){
                    Cursor.visible = true;
                    flagHerramienta=false;
                    typecrop=0;
                }
                else{
                    Cursor.visible = false;
                    flagHerramienta=true;
                    typecrop=1;
                    //Añadir la selecion de planta
                }
                break;
            case "2":
                if (typecrop==2){
                    Cursor.visible = true;
                    flagHerramienta=false;
                    typecrop=0;
                }
                else{
                    Cursor.visible = false;
                    flagHerramienta=true;
                    typecrop=2;
                    //Añadir la selecion de planta
                }
                break;
            case "3":
                if (typecrop==3){
                    Cursor.visible = true;
                    flagHerramienta=false;
                    typecrop=0;
                }
                else{
                    Cursor.visible = false;
                    flagHerramienta=true;
                    typecrop=3;
                    //Añadir la selecion de planta
                }
                break;
            case "4":
                if (typecrop==4){
                    Cursor.visible = true;
                    flagHerramienta=false;
                    typecrop=0;
                }
                else{
                    Cursor.visible = false;
                    flagHerramienta=true;
                    typecrop=4;
                    //Añadir la selecion de planta
                }
                break;
            case "5":
                if (typecrop==5){
                    Cursor.visible = true;
                    flagHerramienta=false;
                    typecrop=0;
                }
                else{
                    Cursor.visible = false;
                    flagHerramienta=true;
                    typecrop=5;
                    //Añadir la selecion de planta
                }
                break;
            case "6":
                if (typecrop==6){
                    Cursor.visible = true;
                    flagHerramienta=false;
                    typecrop=0;
                }
                else{
                    Cursor.visible = false;
                    flagHerramienta=true;
                    typecrop=6;
                    //Añadir la selecion de planta
                }
                break;
        }
    }
    public void regadera(){
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagHerramienta=false;
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        if(flagRegadera){
            flagRegadera=false;
            Cursor.visible = true;    
        }else{
            flagRegadera=true;
            Cursor.visible = false;
        }
    }

    // Mercado
    public void ShowMenuVender()
    {
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        if(flagMenuVender){
            flagMenuVender=false;
            MenuVender.SetActive(false);
        }else{
            flagMenuVender=true;
            MenuVender.SetActive(true);
            trigov=0;
            maizv=0;
            chilev=0;
            aguacatev=0;
            cafev=0;
            tomatev=0;
            precioTrigo.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(1).ToString();
            precioMaiz.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(2).ToString();
            precioChile.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(6).ToString();
            precioAguacate.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(4).ToString();
            precioCafe.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(5).ToString();
            precioTomate.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(3).ToString();
            TrigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
            MaizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
            ChileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
            AguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
            CafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
            TomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
        }
    }
    public void ShowMenuVender2(){
        if(flagMenuVender2){
            flagMenuVender2=false;
            MenuVender2.SetActive(false);
        }else{
            flagMenuVender2=true;
            MenuVender2.SetActive(true);
            trigov=0;
            maizv=0;
            chilev=0;
            aguacatev=0;
            cafev=0;
            tomatev=0;
            precioTrigo2.GetComponent<TextMeshProUGUI>().text = "$ " + ((int)(financeManager.GetCropPrice(1)*1.3)).ToString();
            precioMaiz2.GetComponent<TextMeshProUGUI>().text = "$ " + ((int)(financeManager.GetCropPrice(2)*1.3)).ToString();
            precioChile2.GetComponent<TextMeshProUGUI>().text = "$ " + ((int)(financeManager.GetCropPrice(6)*1.3)).ToString();
            precioAguacate2.GetComponent<TextMeshProUGUI>().text = "$ " + ((int)(financeManager.GetCropPrice(4)*1.3)).ToString();
            precioCafe2.GetComponent<TextMeshProUGUI>().text = "$ " + ((int)(financeManager.GetCropPrice(5)*1.3)).ToString();
            precioTomate2.GetComponent<TextMeshProUGUI>().text = "$ " + ((int)(financeManager.GetCropPrice(3)*1.3)).ToString();
            TrigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
            MaizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
            ChileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
            AguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
            CafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
            TomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
        }
    }

    public void Sumar(string cropType){
        switch(cropType){
            case "1":
                if(cropManager.GetCropQuantity(1)>trigov){
                    trigov+=5;
                }else{
                    trigov =cropManager.GetCropQuantity(1);
                }
                TrigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
                break;
            case "2":
                if(cropManager.GetCropQuantity(2)>maizv){
                    maizv+=5;
                }else{
                    maizv =cropManager.GetCropQuantity(2);
                }
                MaizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
                break;
            case "3":
                if(cropManager.GetCropQuantity(3)>tomatev){
                    tomatev+=5;
                }else{
                    tomatev =cropManager.GetCropQuantity(3);
                }
                TomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
                break;
            case "4":
                if(cropManager.GetCropQuantity(4)>aguacatev){
                    aguacatev+=5;
                }else{
                    aguacatev =cropManager.GetCropQuantity(4);
                }
                AguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
                break;
            case "5":
                if(cropManager.GetCropQuantity(5)>cafev){
                    cafev+=5;
                }else{
                    cafev =cropManager.GetCropQuantity(5);
                }
                CafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
                break;
            case "6":
                if(cropManager.GetCropQuantity(6)>chilev){
                    chilev+=5;
                }else{
                    chilev =cropManager.GetCropQuantity(6);
                }
                ChileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
                break;
        }
    }
    public void Restar(string cropType){
        switch(cropType){
            case "1":
                if(trigov>0){
                    trigov-=5;
                } else{
                    trigov = 0;
                }
                TrigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
                break;
            case "2":
                if(maizv>0){
                    maizv-=5;
                } else{
                    maizv = 0;
                }
                MaizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
                break;
            case "3":
                if(tomatev>0){
                    tomatev-=5;
                } else{
                    tomatev = 0;
                }
                TomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
                break;
            case "4":
                if(aguacatev>0){
                    aguacatev-=5;
                }else{  
                    aguacatev = 0;
                }
                AguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
                break;
            case "5":
                if(cafev>0){
                    cafev-=5;
                } else{
                    cafev = 0;
                }
                CafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
                break;
            case "6":
                if(chilev>0){
                    chilev-=5;
                } else{
                    chilev = 0;
                }
                ChileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
                break;
        }
    }
    public void ShowMenuComprar()
    {
        flagMenuVender=false;
        MenuVender.SetActive(false);
        if(flagMenuComprar){
            flagMenuComprar=false;
            MenuComprar.SetActive(false);
        }else{
            flagMenuComprar=true;
            MenuComprar.SetActive(true);
            trigoCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(1).ToString();
            trigoPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(1)).ToString();
            maizCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(2).ToString();
            maizPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(2)).ToString();
            chileCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(6).ToString();
            chilePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(6)).ToString();
            aguacateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(4).ToString();
            aguacatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(4)).ToString();
            cafeCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(5).ToString();
            cafePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(5)).ToString();
            tomateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(3).ToString();
            tomatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(3)).ToString();
        }
    }

    public void ShowMenuComprar2(){
        if(flagMenuComprar2){
            flagMenuComprar2=false;
            MenuComprar2.SetActive(false);
        }else{
            flagMenuComprar2=true;
            MenuComprar2.SetActive(true);
            trigoCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(1).ToString();
            trigoPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(1)).ToString();
            maizCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(2).ToString();
            maizPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(2)).ToString();
            chileCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(6).ToString();
            chilePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(6)).ToString();
            aguacateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(4).ToString();
            aguacatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(4)).ToString();
            cafeCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(5).ToString();
            cafePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(5)).ToString();
            tomateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(3).ToString();
            tomatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(3)).ToString();
        }
    }
    public void SumarTrigoV(){
        if(cropManager.GetCropQuantity(1)>trigov){
            trigov+=5;
        }else{
            trigov =cropManager.GetCropQuantity(1);
        }
        TrigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
    }
    public void RestarTrigoV(){
        if(trigov>0){
            trigov-=5;
        } else{
            trigov = 0;
        }
        TrigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
    }
    public void SumarMaízV(){
        if(cropManager.GetCropQuantity(2)>maizv){
            maizv+=5;
        }else{
            maizv =cropManager.GetCropQuantity(2); 
        }
        MaizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
    }
    public void RestarMaízV(){
        if(maizv>0){
            maizv-=5;
        } else{
            maizv = 0;
        }
        MaizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
    }
    public void SumarChileV(){
        if(cropManager.GetCropQuantity(6)>chilev){
            chilev+=5;
        }else{
            chilev =cropManager.GetCropQuantity(6);
        }
        ChileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
    }
    public void RestarChileV(){
        if(chilev>0){
            chilev-=5;
        } else{
            chilev = 0;
        }
        ChileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
    }
    public void SumarAguacateV(){
        if(cropManager.GetCropQuantity(4)>aguacatev){
            aguacatev+=5;
        }else{
            aguacatev =cropManager.GetCropQuantity(4);
        }
        AguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
    }
    public void RestarAguacateV(){
        if(aguacatev>0){
            aguacatev-=5;
        }else{  
            aguacatev = 0;
        }
        AguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
    }
    public void SumarCafeV(){
        if(cropManager.GetCropQuantity(5)>cafev){
            cafev+=5;
        } else{
            cafev =cropManager.GetCropQuantity(5);
        }
        CafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
    }
    public void RestarCafeV(){
        if(cafev>0){
            cafev-=5;
        } else{
            cafev = 0;
        }
        CafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
    }
    public void SumarTomateV(){
        if(cropManager.GetCropQuantity(3)>tomatev){
            tomatev+=5;
        } else{
            tomatev =cropManager.GetCropQuantity(3);
        }
        TomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
    }
    public void RestarTomateV(){
        if(tomatev>0){
            tomatev-=5;
        } else{
            tomatev = 0;
        }
        TomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
    }

    public void Vender(){
        financeManager.SellItem(1,trigov);
        financeManager.SellItem(2,maizv);
        financeManager.SellItem(6,chilev);
        financeManager.SellItem(4,aguacatev);
        financeManager.SellItem(5,cafev);
        financeManager.SellItem(3,tomatev);
        trigov=0;
        maizv=0;
        chilev=0;
        aguacatev=0;
        cafev=0;
        tomatev=0;
        TrigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
        MaizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
        ChileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
        AguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
        CafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
        TomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }

    public void Vender2(){
        financeManager.SellItem2(1,trigov);
        financeManager.SellItem2(2,maizv);
        financeManager.SellItem2(6,chilev);
        financeManager.SellItem2(4,aguacatev);
        financeManager.SellItem2(5,cafev);
        financeManager.SellItem2(3,tomatev);
        trigov=0;
        maizv=0;
        chilev=0;
        aguacatev=0;
        cafev=0;
        tomatev=0;
        TrigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
        MaizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
        ChileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
        AguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
        CafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
        TomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }
    public void VenderTodo(){
        
        financeManager.SellItem(1,cropManager.GetCropQuantity(1));
        financeManager.SellItem(2,cropManager.GetCropQuantity(2));
        financeManager.SellItem(3,cropManager.GetCropQuantity(3));
        financeManager.SellItem(4,cropManager.GetCropQuantity(4));
        financeManager.SellItem(5,cropManager.GetCropQuantity(5));
        financeManager.SellItem(6,cropManager.GetCropQuantity(6));
        trigov=0;
        maizv=0;
        chilev=0;
        aguacatev=0;
        cafev=0;
        tomatev=0;
        TrigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
        MaizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
        ChileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
        AguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
        CafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
        TomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }
    public void VenderTodo2(){
        financeManager.SellItem2(1,cropManager.GetCropQuantity(1));
        financeManager.SellItem2(2,cropManager.GetCropQuantity(2));
        financeManager.SellItem2(3,cropManager.GetCropQuantity(3));
        financeManager.SellItem2(4,cropManager.GetCropQuantity(4));
        financeManager.SellItem2(5,cropManager.GetCropQuantity(5));
        financeManager.SellItem2(6,cropManager.GetCropQuantity(6));
        trigov=0;
        maizv=0;
        chilev=0;
        aguacatev=0;
        cafev=0;
        tomatev=0;
        TrigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString();
        MaizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString();
        ChileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString();
        AguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString();
        CafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString();
        TomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString();
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }

    public void ComprarSemillas(string cropType){
        if(cropType=="1"){
            if(UserController.GetCapital()>=marketManager.GetTotal(1)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal(1));
                cropManager.UpdateCropSeeds(1,marketManager.GetCantidad(1));
                marketManager.UpdateCropQuantity(1,0);
                trigoCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(1).ToString();
                trigoPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(1)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="2"){
            if(UserController.GetCapital()>=marketManager.GetTotal(2)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal(2));
                cropManager.UpdateCropSeeds(2,marketManager.GetCantidad(2));
                marketManager.UpdateCropQuantity(2,0);
                maizCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(2).ToString();
                maizPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(2)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="3"){ 
            if(UserController.GetCapital()>=marketManager.GetTotal(3)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal(3));
                cropManager.UpdateCropSeeds(3,marketManager.GetCantidad(3));
                marketManager.UpdateCropQuantity(3,0);
                tomateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(3).ToString();
                tomatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(3)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="4"){
            if(UserController.GetCapital()>=marketManager.GetTotal(4)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal(4));
                cropManager.UpdateCropSeeds(4,marketManager.GetCantidad(4));
                marketManager.UpdateCropQuantity(4,0);
                aguacateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(4).ToString();
                aguacatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(4)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="5"){
            if(UserController.GetCapital()>=marketManager.GetTotal(5)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal(5));
                cropManager.UpdateCropSeeds(5,marketManager.GetCantidad(5));
                marketManager.UpdateCropQuantity(5,0);
                cafeCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(5).ToString();
                cafePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(5)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="6"){
            if(UserController.GetCapital()>=marketManager.GetTotal(6)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal(6));
                cropManager.UpdateCropSeeds(6,marketManager.GetCantidad(6));
                marketManager.UpdateCropQuantity(6,0);
                chileCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(6).ToString();
                chilePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(6)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        }
    }
    public void ComprarSemillas2(string cropType){
        if(cropType=="1"){
            if(UserController.GetCapital()>=marketManager.GetTotal2(1)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal2(1));
                cropManager.UpdateCropSeeds(1,marketManager.GetCantidad2(1));
                marketManager.UpdateCropQuantity2(1,0);
                trigoCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(1).ToString();
                trigoPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(1)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="2"){
            if(UserController.GetCapital()>=marketManager.GetTotal2(2)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal2(2));
                cropManager.UpdateCropSeeds(2,marketManager.GetCantidad2(2));
                marketManager.UpdateCropQuantity2(2,0);
                maizCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(2).ToString();
                maizPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(2)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="3"){ 
            if(UserController.GetCapital()>=marketManager.GetTotal2(3)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal2(3));
                cropManager.UpdateCropSeeds(3,marketManager.GetCantidad2(3));
                marketManager.UpdateCropQuantity2(3,0);
                tomateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(3).ToString();
                tomatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(3)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="4"){
            if(UserController.GetCapital()>=marketManager.GetTotal2(4)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal2(4));
                cropManager.UpdateCropSeeds(4,marketManager.GetCantidad2(4));
                marketManager.UpdateCropQuantity2(4,0);
                aguacateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(4).ToString();
                aguacatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(4)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="5"){
            if(UserController.GetCapital()>=marketManager.GetTotal2(5)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal2(5));
                cropManager.UpdateCropSeeds(5,marketManager.GetCantidad2(5));
                marketManager.UpdateCropQuantity2(5,0);
                cafeCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(5).ToString();
                cafePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(5)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        } else if (cropType=="6"){
            if(UserController.GetCapital()>=marketManager.GetTotal2(6)){
                UserController.UpdateCapital(-(int)marketManager.GetTotal2(6));
                cropManager.UpdateCropSeeds(6,marketManager.GetCantidad2(6));
                marketManager.UpdateCropQuantity2(6,0);
                chileCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(6).ToString();
                chilePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(6)).ToString();
                Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
            }
        }
    }

    public void OpenBigPeriodico(){
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagArbol=false;
        Arbol.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        if(flagBigPeriodico){
            flagBigPeriodico=false;
            BigPeriodico.SetActive(false);
        }else{
            flagBigPeriodico=true;
            BigPeriodico.SetActive(true);
        }
    }

    public void ShowArbol(){
        flagMenuVender2=false;
        MenuVender2.SetActive(false);
        flagMenuComprar2=false;
        MenuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagInventario=false;
        Inventario.SetActive(false);
        flagDeuda=false;
        Deuda.SetActive(false);
        flagMenuComprar=false;
        MenuComprar.SetActive(false);
        flagMenuVender=false;
        MenuVender.SetActive(false);
        flagCelular=false;
        Celular.SetActive(false);
        flagMercado=false;
        Mercado.SetActive(false);
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
        flagMenuPlantar=false;
        MenuPlantar.SetActive(false);
        if(flagArbol){
            flagArbol=false;
            Arbol.SetActive(false);
        }else{
            flagArbol=true;
            Arbol.SetActive(true);
        }
    }
    private void Awake()
    {
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }
    public void SlowDownTime(){
        Time.timeScale = 0.5f;
    }
    public void NormalTime(){
        Time.timeScale = 1;
    }
    public void SpeedUpTime(){
        Time.timeScale = 5;
    }

    public void MusicMute(){
        if(musicMute.GetComponent<Image>().sprite == On){
            musicMute.GetComponent<Image>().sprite = Off;
        }else{
            musicMute.GetComponent<Image>().sprite = On;
            //Mutear musica
        }
    }
    public void EfectMute(){
        if(efectMute.GetComponent<Image>().sprite == On){
            efectMute.GetComponent<Image>().sprite = Off;
        }else{
            efectMute.GetComponent<Image>().sprite = On;
            //Mutear efectos
        }
    }
    public void ActualizarDinero(){
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }

    void Update(){
        ActualizarDinero();
    }

}
