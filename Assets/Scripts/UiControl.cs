using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class UiControl : MonoBehaviour
{
    public GameObject PanelOpciones;
    public GameObject UsuarioNombre;
    public GameObject Financiamiento;
    public GameObject Produccion;
    public GameObject Rankings;
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

    public GameObject Dinero;


    public CropManager cropManager;
    private bool flagUsuario=false;
    private bool flagFinanciamiento=false;
    private bool flagProduccion=false;
    private bool flagRankings=false;
    private bool flagCelular=false;
    private bool flagMercado=false;
    private bool flagInventario=false;
    private bool flagDeuda=false;
    public bool flagHerramienta=false;
    public bool flagRegadera=false;

    public bool flagMenuVender=false;
    public bool flagMenuComprar=false;
    public bool flagBigPeriodico=false;
    
    

    //Al dar click al boton de opciones se abre el panel PanelOpciones
    public void OpenOpciones()
    {
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
        Time.timeScale = 0;
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
        Time.timeScale = 1;
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
        if(flagRankings){
            flagRankings=false;
            Rankings.SetActive(false);
        }else{
            flagRankings=true;
            Rankings.SetActive(true);
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void ShowCelular()
    {
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
        if(flagDeuda){
            flagDeuda=false;
            Deuda.SetActive(false);
        }else{
            flagDeuda=true;
            Deuda.SetActive(true);
        }
    }

    public void hoz(){
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
        if(flagHerramienta){
            flagHerramienta=false;
            Cursor.visible = true;    
        }else{
            flagHerramienta=true;
            Cursor.visible = false;
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
            precioTrigo.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(1).ToString();
            precioMaiz.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(2).ToString();
            precioChile.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(6).ToString();
            precioAguacate.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(4).ToString();
            precioCafe.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(5).ToString();
            precioTomate.GetComponent<TextMeshProUGUI>().text = "$ " + financeManager.GetCropPrice(3).ToString();
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

    public void OpenBigPeriodico(){
        flagBigPeriodico=false;
        BigPeriodico.SetActive(false);
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

        if(flagBigPeriodico){
            flagBigPeriodico=false;
            BigPeriodico.SetActive(false);
        }else{
            flagBigPeriodico=true;
            BigPeriodico.SetActive(true);
        }
    }


    private void Awake()
    {
        Dinero.GetComponent<TextMeshProUGUI>().text = "$ " + UserController.GetCapital().ToString();
    }





}
