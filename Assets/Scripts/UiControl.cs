using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using Image = UnityEngine.UI.Image;

using UnityEngine.SceneManagement;
public class UiControl : MonoBehaviour
{

    private String url = "http://52.5.57.146:8080/";
    public GameObject datosUsuario;
    public GameObject panelOpciones;
    public GameObject usuarioNombre;
    public GameObject financiamiento;
    public GameObject produccion;
    public GameObject rankings;
    public GameObject ajustes;
    private AudioControl audioControl;
    public GameObject celular;
    public GameObject mercado;
    public GameObject inventario;
    public GameObject deuda;
    public GameObject dinero;
    public GameObject arbol;

//inventario
    public GameObject sTrigoN;
    public GameObject sMaizN;
    public GameObject sChileN;
    public GameObject sAguacateN;
    public GameObject sCafeN;
    public GameObject sTomateN;

    public GameObject trigoN;
    public GameObject maizN;
    public GameObject chileN;
    public GameObject aguacateN;
    public GameObject cafeN;
    public GameObject tomateN;

// Menu Créditos
    public GameObject creditos;
    private bool flagCreditos=false;

//FinanceManager
    public FinanceManager financeManager;

//User Controller
    public UserController userController;

//deuda
    public GameObject deudaTxt;
    public GameObject deudaTime;

// mercado 

    public GameObject menuVender;
    public GameObject menuComprar;
    private int maizv=0;
    public GameObject maizVcontador;
    private int trigov=0;
    public GameObject trigoVcontador;
    private int chilev=0;
    public GameObject chileVcontador;
    private int aguacatev=0;
    public GameObject aguacateVcontador;
    private int cafev=0;
    public GameObject cafeVcontador;
    private int tomatev=0;
    public GameObject tomateVcontador;
    public GameObject precioTrigo;
    public GameObject precioMaiz;
    public GameObject precioChile;
    public GameObject precioAguacate;
    public GameObject precioCafe;
    public GameObject precioTomate;

    public GameObject bigPeriodico;

    public GameObject menuPlantar;
    public int typecrop=0;

// Objeto EviarDatos
    public EnviarDatos enviardatos;

// mercado Comprar

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

// mercado 2
    public GameObject menuVender2;
    public GameObject menuComprar2;
    public GameObject precioTrigo2;
    public GameObject precioMaiz2;
    public GameObject precioChile2;
    public GameObject precioAguacate2;
    public GameObject precioCafe2;
    public GameObject precioTomate2;
    public GameObject trigoVcontador2;
    public GameObject maizVcontador2;
    public GameObject chileVcontador2;
    public GameObject aguacateVcontador2;
    public GameObject cafeVcontador2;
    public GameObject tomateVcontador2;
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


    //rankings
    public GameObject ranking1Name;
    public GameObject ranking1Money;
    public GameObject ranking1Finance;
    public GameObject ranking2Name;
    public GameObject ranking2Money;
    public GameObject ranking2Finance;
    public GameObject ranking3Name;
    public GameObject ranking3Money;
    public GameObject ranking3Finance;
    public GameObject playerName;
    public GameObject playerMoney;
    public GameObject playerFinance;

    public string ranking1NameData;
    public string ranking2NameData;
    public string ranking3NameData;
    public string ranking1MoneyData;
    public string ranking2MoneyData;
    public string ranking3MoneyData;
    public string ranking1FinanceData;
    public string ranking2FinanceData;
    public string ranking3FinanceData;


    //TreeManager
    private TreeManager treeManager;
    //CropManager

    public CropManager cropManager;
    //Banderas
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
    
    
    void Awake(){
        //Establece valores dommy para los rankings y pone el dinero del usuario en su posición
        dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
        ranking1NameData="Jorge";
        ranking2NameData="Pedro";
        ranking3NameData="Juan";
        ranking1MoneyData="$ 1000000";
        ranking2MoneyData="$ 500000";
        ranking3MoneyData="$ 100000";
        ranking1FinanceData="Verqor";
        ranking2FinanceData="Banco";
        ranking3FinanceData="Verqor";
        audioControl = GetComponent<AudioControl>();
        treeManager = GetComponent<TreeManager>();
    }

    //Al dar click al boton de opciones se abre el panel PanelOpciones
    public void OpenOpciones()
    {
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagHerramienta=false;
        panelOpciones.SetActive(true);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        arbol.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        Time.timeScale = 0;
        cameraMovement.enabled = false;
    }
    // Al dar click al boton de opciones que esta en el PanelOpciones se cierra el panel PanelOpciones
    public void CloseOpciones()
    {
        flagUsuario=false;
        usuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        financiamiento.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagRankings=false;
        rankings.SetActive(false);
        panelOpciones.SetActive(false);
        flagAjustes=false;
        ajustes.SetActive(false);
        flagCreditos=false;
        creditos.SetActive(false);
        Time.timeScale = 1;
        cameraMovement.enabled = true;
    }
    //Al dar click al boton de usuario se abre el menú con el Nombre de Usuario
    public void Usuario()
    {
        flagFinanciamiento=false;
        financiamiento.SetActive(false);
        flagRankings=false;
        rankings.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagAjustes=false;
        ajustes.SetActive(false);
        flagCreditos=false;
        creditos.SetActive(false);
        if(flagUsuario){
            flagUsuario=false;
            usuarioNombre.SetActive(false);
        }else{
            flagUsuario=true;
            usuarioNombre.SetActive(true);
        }
        
    }
    //Al dar click al boton de crédito se abre el menú de financiamiento
    public void ShowFinanciamiento()
    {
        flagUsuario=false;
        usuarioNombre.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagRankings=false;
        rankings.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagAjustes=false;
        flagCreditos=false;
        creditos.SetActive(false);
        ajustes.SetActive(false);
        if(flagFinanciamiento){
            flagFinanciamiento=false;
            financiamiento.SetActive(false);
        }else{
            flagFinanciamiento=true;
            financiamiento.SetActive(true);
        }
        
    }
    //Al dar click al boton de producción se abre el menú de producción
    public void ShowProduccion()
    {
        flagUsuario=false;
        usuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        financiamiento.SetActive(false);
        flagRankings=false;
        rankings.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagAjustes=false;
        ajustes.SetActive(false);
        flagCreditos=false;
        creditos.SetActive(false);
        if(flagProduccion){
            flagProduccion=false;
            produccion.SetActive(false);
        }else{
            flagProduccion=true;
            produccion.SetActive(true);
        }
        
    }
    //Al dar click al boton de rankings se abre el menú de rankings
    public void ShowRankings()
    {
        flagUsuario=false;
        usuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        financiamiento.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagAjustes=false;
        ajustes.SetActive(false);
        flagCreditos=false;
        creditos.SetActive(false);
        if(flagRankings){
            flagRankings=false;
            rankings.SetActive(false);
        }else{
            flagRankings=true;
            rankings.SetActive(true);
            Writerankings();
        }
    }

    //Escribe los rankings en el menú de rankings
    private void Writerankings(){
        ranking1Name.GetComponent<TextMeshProUGUI>().text = ranking1NameData;
        ranking1Money.GetComponent<TextMeshProUGUI>().text = "$" +ranking1MoneyData;
        ranking1Finance.GetComponent<TextMeshProUGUI>().text = ranking1FinanceData;
        ranking2Name.GetComponent<TextMeshProUGUI>().text = ranking2NameData;
        ranking2Money.GetComponent<TextMeshProUGUI>().text = "$"+ranking2MoneyData;
        ranking2Finance.GetComponent<TextMeshProUGUI>().text = ranking2FinanceData;
        ranking3Name.GetComponent<TextMeshProUGUI>().text = ranking3NameData;
        ranking3Money.GetComponent<TextMeshProUGUI>().text = "$"+ranking3MoneyData;
        ranking3Finance.GetComponent<TextMeshProUGUI>().text = ranking3FinanceData;
        playerName.GetComponent<TextMeshProUGUI>().text = datosUsuario.GetComponent<DatosUsurio>().GetNombreUsuario();
        playerMoney.GetComponent<TextMeshProUGUI>().text = "$" +userController.GetCapital().ToString();
        playerFinance.GetComponent<TextMeshProUGUI>().text = datosUsuario.GetComponent<DatosUsurio>().GetTipoFinanciamiento();
    }

    //Al dar click al boton de ajustes se abre el menú de ajustes
    public void ShowAjustes()
    {
        flagUsuario=false;
        usuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        financiamiento.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagRankings=false;
        rankings.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagCreditos=false;
        creditos.SetActive(false);
        if(flagAjustes){
            flagAjustes=false;
            ajustes.SetActive(false);
        }else{
            flagAjustes=true;
            ajustes.SetActive(true);
        }
    }
    //Al dar click al boton de guardar se guarda la partida
    public void SaveGame()
    {
        Debug.Log("Guardando...");
        enviardatos.Guardar();
        Debug.Log("Guardado exitoso.");
    }
    //Al dar click al boton de salir se guarda la partida y se cierra el juego
    public void CloseGame()
    {
        audioControl.Save();
        Debug.Log("Guardando...");
        enviardatos.GuardarySalir();
        Debug.Log("Guardado exitoso.");
        Application.OpenURL(url);
        SceneManager.LoadScene("Exit");
        

    }
    //Al dar click al boton de créditos se abre el menú de créditos
    public void ShowCreditos(){
        flagUsuario=false;
        usuarioNombre.SetActive(false);
        flagFinanciamiento=false;
        financiamiento.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagProduccion=false;
        produccion.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagAjustes=false;
        ajustes.SetActive(false);
        flagRankings=false;
        rankings.SetActive(false);
        if(flagCreditos){
            flagCreditos=false;
            creditos.SetActive(false);
        }else{
            flagCreditos=true;
            creditos.SetActive(true);
        }
    }

    //Al dar click al boton de celular se abre el menú de celular
    public void ShowCelular()
    {
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagMercado=false;
        mercado.SetActive(false);
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        arbol.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        if(flagCelular){
            flagCelular=false;
            celular.SetActive(false);
        }else{
            flagCelular=true;
            celular.SetActive(true);
        }
    }
    //Al dar click al boton de mercado se abre el menú de mercado
    public void ShowMercado()
    {
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false;
        typecrop=0; 
        flagCelular=false;
        celular.SetActive(false);
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        arbol.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        if(flagMercado){
            flagMercado=false;
            mercado.SetActive(false);
        }else{
            flagMercado=true;
            mercado.SetActive(true);
        }
    }
    //Al dar click al boton de inventario se abre el menú de inventario
    public void ShowInventario()
    {
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        arbol.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        if(flagInventario){
            flagInventario=false;
            inventario.SetActive(false);
        }else{
            flagInventario=true;
            inventario.SetActive(true);
            sTrigoN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(1).ToString()+" kg";
            sMaizN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(2).ToString()+" kg";
            sChileN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(6).ToString()+" kg";
            sAguacateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(4).ToString()+" kg";
            sCafeN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(5).ToString()+" kg";
            sTomateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropSeeds(3).ToString()+" kg";
            trigoN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(1).ToString()+" kg";
            maizN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(2).ToString()+" kg";
            chileN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(6).ToString()+" kg";
            aguacateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(4).ToString()+" kg";
            cafeN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(5).ToString()+" kg";
            tomateN.GetComponent<TextMeshProUGUI>().text = cropManager.GetCropQuantity(3).ToString()+" kg";
        }
    }
    //Al dar click al boton de deuda se abre el menú de deuda
    public void ShowDeuda()
    {
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false;
        typecrop=0; 
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagInventario=false;
        inventario.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        arbol.SetActive(false);
        if(flagDeuda){
            flagDeuda=false;
            deuda.SetActive(false);
        }else{
            flagDeuda=true;
            deuda.SetActive(true);
            deudaTxt.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetDebt().ToString();
            deudaTime.GetComponent<TextMeshProUGUI>().text = financeManager.GetTimetoPay().ToString() + " días";
        }
    }

    //Al dar click al boton de plantar se abre el menú de plantar
    public void PagarDeuda(){
        if(userController.GetCapital()>=userController.GetDebt()){
            userController.PayDebt(userController.GetDebt());
            deudaTxt.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetDebt().ToString();
            dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
        }
    }
    
    //Al dar click al boton de plantar se abre el menú de plantar
    public void Hoz(){
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagRegadera=false;
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        Cursor.visible = true;
        arbol.SetActive(false);
        if(flagMenuPlantar){
            flagMenuPlantar=false;
            menuPlantar.SetActive(false);
            flagHerramienta=false; 
            Cursor.visible = true;
            typecrop=0;
        }else{
            flagMenuPlantar=true;
            menuPlantar.SetActive(true);
            //Cursor.visible = false;
        }
    }

    //Al dar click al boton de plantar se aparece la hoz con el cultivo seleccionado
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

    //Al dar click al boton de plantar aparece la regadera
    public void Regadera(){
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagHerramienta=false;
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagArbol=false;
        arbol.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        if(flagRegadera){
            flagRegadera=false;
            Cursor.visible = true;    
        }else{
            flagRegadera=true;
            Cursor.visible = false;
        }
    }

    // Al dar click al boton de plantar se abre el menú de vender
    public void ShowMenuVender()
    {
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        if(flagMenuVender){
            flagMenuVender=false;
            menuVender.SetActive(false);
        }else{
            flagMenuVender=true;
            menuVender.SetActive(true);
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
            trigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
            maizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
            chileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
            aguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
            cafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
            tomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
        }
    }
    // Al dar click al boton de plantar se abre el menú de vender del celular
    public void ShowMenuVender2(){
        if(flagMenuVender2){
            flagMenuVender2=false;
            menuVender2.SetActive(false);
        }else{
            flagMenuVender2=true;
            menuVender2.SetActive(true);
            flagCelular=false;
            celular.SetActive(false);
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
            trigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
            maizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
            chileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
            aguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
            cafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
            tomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
        }
    }
    //Al dar click se suma en 5 la cantidad de cultivo a vender
    public void Sumar(string cropType){
        switch(cropType){
            case "1":
                if(cropManager.GetCropQuantity(1)>trigov){
                    trigov+=5;
                }else{
                    trigov =cropManager.GetCropQuantity(1);
                }
                trigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString() +" kg";
                break;
            case "2":
                if(cropManager.GetCropQuantity(2)>maizv){
                    maizv+=5;
                }else{
                    maizv =cropManager.GetCropQuantity(2);
                }
                maizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
                break;
            case "3":
                if(cropManager.GetCropQuantity(3)>tomatev){
                    tomatev+=5;
                }else{
                    tomatev =cropManager.GetCropQuantity(3);
                }
                tomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
                break;
            case "4":
                if(cropManager.GetCropQuantity(4)>aguacatev){
                    aguacatev+=5;
                }else{
                    aguacatev =cropManager.GetCropQuantity(4);
                }
                aguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
                break;
            case "5":
                if(cropManager.GetCropQuantity(5)>cafev){
                    cafev+=5;
                }else{
                    cafev =cropManager.GetCropQuantity(5);
                }
                cafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
                break;
            case "6":
                if(cropManager.GetCropQuantity(6)>chilev){
                    chilev+=5;
                }else{
                    chilev =cropManager.GetCropQuantity(6);
                }
                chileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
                break;
        }
    }
    //Al dar click se resta en 5 la cantidad de cultivo a vender
    public void Restar(string cropType){
        switch(cropType){
            case "1":
                if(trigov>0){
                    trigov-=5;
                } else{
                    trigov = 0;
                }
                trigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
                break;
            case "2":
                if(maizv>0){
                    maizv-=5;
                } else{
                    maizv = 0;
                }
                maizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
                break;
            case "3":
                if(tomatev>0){
                    tomatev-=5;
                } else{
                    tomatev = 0;
                }
                tomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
                break;
            case "4":
                if(aguacatev>0){
                    aguacatev-=5;
                }else{  
                    aguacatev = 0;
                }
                aguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
                break;
            case "5":
                if(cafev>0){
                    cafev-=5;
                } else{
                    cafev = 0;
                }
                cafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
                break;
            case "6":
                if(chilev>0){
                    chilev-=5;
                } else{
                    chilev = 0;
                }
                chileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
                break;
        }
    }
    //Al dar click muestra el menú de comprar
    public void ShowMenuComprar()
    {
        flagMenuVender=false;
        menuVender.SetActive(false);
        if(flagMenuComprar){
            flagMenuComprar=false;
            menuComprar.SetActive(false);
        }else{
            flagMenuComprar=true;
            menuComprar.SetActive(true);
            trigoCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(1).ToString()+" kg";
            trigoPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(1)).ToString();
            maizCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(2).ToString()+" kg";
            maizPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(2)).ToString();
            chileCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(6).ToString()+" kg";
            chilePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(6)).ToString();
            aguacateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(4).ToString()+" kg";
            aguacatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(4)).ToString();
            cafeCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(5).ToString()+" kg";
            cafePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(5)).ToString();
            tomateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(3).ToString()+" kg";
            tomatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(3)).ToString();
        }
    }
    //Al dar click muestra el menú de comprar del celular
    public void ShowMenuComprar2(){
        if(flagMenuComprar2){
            flagMenuComprar2=false;
            menuComprar2.SetActive(false);
        }else{
            flagMenuComprar2=true;
            menuComprar2.SetActive(true);
            flagCelular=false;
            celular.SetActive(false);
            trigoCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(1).ToString() + " kg";
            trigoPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(1)).ToString();
            maizCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(2).ToString() +" kg";
            maizPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(2)).ToString();
            chileCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(6).ToString() +" kg";
            chilePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(6)).ToString();
            aguacateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(4).ToString() +" kg";
            aguacatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(4)).ToString();
            cafeCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(5).ToString() +" kg";
            cafePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(5)).ToString();
            tomateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(3).ToString()+" kg";
            tomatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(3)).ToString();
        }
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void SumarTrigoV(){
        if(cropManager.GetCropQuantity(1)>trigov){
            trigov+=5;
        }else{
            trigov =cropManager.GetCropQuantity(1);
        }
        trigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void RestarTrigoV(){
        if(trigov>0){
            trigov-=5;
        } else{
            trigov = 0;
        }
        trigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void SumarMaízV(){
        if(cropManager.GetCropQuantity(2)>maizv){
            maizv+=5;
        }else{
            maizv =cropManager.GetCropQuantity(2); 
        }
        maizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void RestarMaízV(){
        if(maizv>0){
            maizv-=5;
        } else{
            maizv = 0;
        }
        maizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void SumarChileV(){
        if(cropManager.GetCropQuantity(6)>chilev){
            chilev+=5;
        }else{
            chilev =cropManager.GetCropQuantity(6);
        }
        chileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void RestarChileV(){
        if(chilev>0){
            chilev-=5;
        } else{
            chilev = 0;
        }
        chileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void SumarAguacateV(){
        if(cropManager.GetCropQuantity(4)>aguacatev){
            aguacatev+=5;
        }else{
            aguacatev =cropManager.GetCropQuantity(4);
        }
        aguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void RestarAguacateV(){
        if(aguacatev>0){
            aguacatev-=5;
        }else{  
            aguacatev = 0;
        }
        aguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void SumarCafeV(){
        if(cropManager.GetCropQuantity(5)>cafev){
            cafev+=5;
        } else{
            cafev =cropManager.GetCropQuantity(5);
        }
        cafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void RestarCafeV(){
        if(cafev>0){
            cafev-=5;
        } else{
            cafev = 0;
        }
        cafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void SumarTomateV(){
        if(cropManager.GetCropQuantity(3)>tomatev){
            tomatev+=5;
        } else{
            tomatev =cropManager.GetCropQuantity(3);
        }
        tomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
    }
    //Al dar click suma 5 kg de cultivo a comprar
    public void RestarTomateV(){
        if(tomatev>0){
            tomatev-=5;
        } else{
            tomatev = 0;
        }
        tomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
    }
    //Al dar click se venden los cultivos seleccionados en las cantidades seleccionadas
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
        trigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
        maizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
        chileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
        aguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
        cafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
        tomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
        dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString()+" kg";
    }

    //Al dar click se venden los cultivos seleccionados en las cantidades seleccionadas en el celular
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
        trigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
        maizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
        chileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
        aguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
        cafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
        tomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
        dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString()+" kg";
    }
    //Al dar click se venden todos los cultivos en tu inventario
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
        trigoVcontador.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
        maizVcontador.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
        chileVcontador.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
        aguacateVcontador.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
        cafeVcontador.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
        tomateVcontador.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
        dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString()+" kg";
    }
    //Al dar click se venden todos los cultivos en tu inventario en el celular
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
        trigoVcontador2.GetComponent<TextMeshProUGUI>().text = trigov.ToString()+" kg";
        maizVcontador2.GetComponent<TextMeshProUGUI>().text = maizv.ToString()+" kg";
        chileVcontador2.GetComponent<TextMeshProUGUI>().text = chilev.ToString()+" kg";
        aguacateVcontador2.GetComponent<TextMeshProUGUI>().text = aguacatev.ToString()+" kg";
        cafeVcontador2.GetComponent<TextMeshProUGUI>().text = cafev.ToString()+" kg";
        tomateVcontador2.GetComponent<TextMeshProUGUI>().text = tomatev.ToString()+" kg";
        dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString()+" kg";
    }

    //Al dar click se compran los cultivos seleccionados en las cantidades que aparecen en el mercado
    public void ComprarSemillas(string cropType){
        if(cropType=="1"){
            if(userController.GetCapital()>=marketManager.GetTotal(1)){
                userController.UpdateCapital(-(int)marketManager.GetTotal(1));
                cropManager.UpdateCropSeeds(1,marketManager.GetCantidad(1));
                marketManager.UpdateCropQuantity(1,0);
                trigoCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(1).ToString() +" kg";
                trigoPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(1)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="2"){
            if(userController.GetCapital()>=marketManager.GetTotal(2)){
                userController.UpdateCapital(-(int)marketManager.GetTotal(2));
                cropManager.UpdateCropSeeds(2,marketManager.GetCantidad(2));
                marketManager.UpdateCropQuantity(2,0);
                maizCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(2).ToString()+" kg";
                maizPrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(2)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="3"){ 
            if(userController.GetCapital()>=marketManager.GetTotal(3)){
                userController.UpdateCapital(-(int)marketManager.GetTotal(3));
                cropManager.UpdateCropSeeds(3,marketManager.GetCantidad(3));
                marketManager.UpdateCropQuantity(3,0);
                tomateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(3).ToString()+" kg";
                tomatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(3)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="4"){
            if(userController.GetCapital()>=marketManager.GetTotal(4)){
                userController.UpdateCapital(-(int)marketManager.GetTotal(4));
                cropManager.UpdateCropSeeds(4,marketManager.GetCantidad(4));
                marketManager.UpdateCropQuantity(4,0);
                aguacateCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(4).ToString()+" kg";
                aguacatePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(4)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="5"){
            if(userController.GetCapital()>=marketManager.GetTotal(5)){
                userController.UpdateCapital(-(int)marketManager.GetTotal(5));
                cropManager.UpdateCropSeeds(5,marketManager.GetCantidad(5));
                marketManager.UpdateCropQuantity(5,0);
                cafeCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(5).ToString()+" kg";
                cafePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(5)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="6"){
            if(userController.GetCapital()>=marketManager.GetTotal(6)){
                userController.UpdateCapital(-(int)marketManager.GetTotal(6));
                cropManager.UpdateCropSeeds(6,marketManager.GetCantidad(6));
                marketManager.UpdateCropQuantity(6,0);
                chileCantidad.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad(6).ToString()+" kg";
                chilePrice.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal(6)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        }
    }
    //Al dar click se compran los cultivos seleccionados en las cantidades que aparecen en el mercado del celular
    public void ComprarSemillas2(string cropType){
        if(cropType=="1"){
            if(userController.GetCapital()>=marketManager.GetTotal2(1)){
                userController.UpdateCapital(-(int)marketManager.GetTotal2(1));
                cropManager.UpdateCropSeeds(1,marketManager.GetCantidad2(1));
                marketManager.UpdateCropQuantity2(1,0);
                trigoCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(1).ToString()+" kg";
                trigoPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(1)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="2"){
            if(userController.GetCapital()>=marketManager.GetTotal2(2)){
                userController.UpdateCapital(-(int)marketManager.GetTotal2(2));
                cropManager.UpdateCropSeeds(2,marketManager.GetCantidad2(2));
                marketManager.UpdateCropQuantity2(2,0);
                maizCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(2).ToString()+" kg";
                maizPrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(2)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="3"){ 
            if(userController.GetCapital()>=marketManager.GetTotal2(3)){
                userController.UpdateCapital(-(int)marketManager.GetTotal2(3));
                cropManager.UpdateCropSeeds(3,marketManager.GetCantidad2(3));
                marketManager.UpdateCropQuantity2(3,0);
                tomateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(3).ToString()+" kg";
                tomatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(3)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="4"){
            if(userController.GetCapital()>=marketManager.GetTotal2(4)){
                userController.UpdateCapital(-(int)marketManager.GetTotal2(4));
                cropManager.UpdateCropSeeds(4,marketManager.GetCantidad2(4));
                marketManager.UpdateCropQuantity2(4,0);
                aguacateCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(4).ToString()+" kg";
                aguacatePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(4)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="5"){
            if(userController.GetCapital()>=marketManager.GetTotal2(5)){
                userController.UpdateCapital(-(int)marketManager.GetTotal2(5));
                cropManager.UpdateCropSeeds(5,marketManager.GetCantidad2(5));
                marketManager.UpdateCropQuantity2(5,0);
                cafeCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(5).ToString()+" kg";
                cafePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(5)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        } else if (cropType=="6"){
            if(userController.GetCapital()>=marketManager.GetTotal2(6)){
                userController.UpdateCapital(-(int)marketManager.GetTotal2(6));
                cropManager.UpdateCropSeeds(6,marketManager.GetCantidad2(6));
                marketManager.UpdateCropQuantity2(6,0);
                chileCantidad2.GetComponent<TextMeshProUGUI>().text = marketManager.GetCantidad2(6).ToString()+" kg";
                chilePrice2.GetComponent<TextMeshProUGUI>().text = "$ " + Math.Round(marketManager.GetTotal2(6)).ToString();
                dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
            }
        }
    }
    //Al dar click se muestra el periódico 
    public void OpenBigPeriodico(){
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagArbol=false;
        arbol.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        if(flagBigPeriodico){
            flagBigPeriodico=false;
            bigPeriodico.SetActive(false);
        }else{
            flagBigPeriodico=true;
            bigPeriodico.SetActive(true);
        }
    }

    //Se muestra el árbol de mejoras
    public void ShowArbol(){
        flagMenuVender2=false;
        menuVender2.SetActive(false);
        flagMenuComprar2=false;
        menuComprar2.SetActive(false);
        Cursor.visible = true;
        flagHerramienta=false; 
        typecrop=0;
        flagInventario=false;
        inventario.SetActive(false);
        flagDeuda=false;
        deuda.SetActive(false);
        flagMenuComprar=false;
        menuComprar.SetActive(false);
        flagMenuVender=false;
        menuVender.SetActive(false);
        flagCelular=false;
        celular.SetActive(false);
        flagMercado=false;
        mercado.SetActive(false);
        flagBigPeriodico=false;
        bigPeriodico.SetActive(false);
        flagMenuPlantar=false;
        menuPlantar.SetActive(false);
        if(flagArbol){
            flagArbol=false;
            arbol.SetActive(false);
        }else{
            flagArbol=true;
            arbol.SetActive(true);
            treeManager.UpdateColors();
        }
    }
    
    //Se relantiza el tiempo
    public void SlowDownTime(){
        Time.timeScale = 0.5f;
    }
    //Se regresa al tiempo normal
    public void NormalTime(){
        Time.timeScale = 1;
    }
    //Se acelera el tiempo
    public void SpeedUpTime(){
        Time.timeScale = 5;
    }
    //Se actualiza el dinero mostrado
    public void ActualizarDinero(){
        dinero.GetComponent<TextMeshProUGUI>().text = "$ " + userController.GetCapital().ToString();
    }
    //Te redirige a la página del tutuorial del juego
    public void ShowTutorial(){
        Application.OpenURL(url + "tutorialjuego");
    }

    //Se llama a actualizar el dinero todo el tiempo
    void Update(){
        ActualizarDinero();
    }

}
