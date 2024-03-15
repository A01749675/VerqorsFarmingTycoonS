using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public GameObject herramienta;
    public GameObject regadera;
    
    public UiControl ui;

    public MapManager mapManager;
    private int contadorAgua = 0;

    private bool startedWatering = false;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ShowTool(ui.flagHerramienta,mousePos);
        ShowRegadera(ui.flagRegadera,mousePos);  
    }
       private void ShowTool(bool tool,Vector2 mousePos){
        if(tool){
            herramienta.SetActive(true);
            herramienta.transform.position = mousePos;
        }
        else{
            herramienta.SetActive(false);
        }
    }
    private void ShowRegadera(bool tool,Vector2 mousePos){
        if(tool){
            regadera.SetActive(true);
            regadera.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0)){
                if(!startedWatering){
                    startedWatering = true;
                    contadorAgua = mapManager.GetCurrentCycle();
                    
                }
                mapManager.WaterAll();
            }
            if(mapManager.GetCurrentCycle()-contadorAgua<1){
                regadera.GetComponent<Animator>().SetBool("Regando",true);
            }
            else{
                regadera.GetComponent<Animator>().SetBool("Regando",false);
                if(mapManager.GetCurrentCycle()-contadorAgua>2 && contadorAgua!=0){
                    ui.flagRegadera = false;
                    Cursor.visible = true;
                }
            }
        }
        else{
            contadorAgua=0;
            regadera.SetActive(false);
            startedWatering = false;
        }
    }
}
