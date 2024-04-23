using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TecManager : MonoBehaviour
{
    public MapManager mapManager;
    //Declarar un vector de GameObjects para los medidores
    public GameObject[] medidor1;
    public GameObject medidores1;
    public GameObject medidores2;
    public GameObject medidores3;
    public Sprite  medidor1_5;
    public Sprite  medidor2_5;
    public Sprite  medidor3_5;
    public Sprite  medidor4_5;
    public Sprite  medidor5_5;
    public TreeManager treeManager;
    public GameObject empleados1;
    public GameObject empleados2;
    public GameObject empleados3;
    public GameObject tractor1;
    public GameObject tractor2;
    public GameObject tractor3;
    public GameObject tanque;
    public GameObject aspersores1;
    public GameObject aspersores2;
    public GameObject prodtrigo;
    public GameObject prodmaiz;
    public GameObject prodtomate;
    public GameObject prodchile;
     public GameObject prodaguacate;
    public GameObject prodfrijol;
   

    public FinanceManager financeManager;
    private void ChangeSprite(int parcela){
        int water = mapManager.GetAverageWaterAtLand(parcela);
        if(water < 20){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor1_5;
        }else if(water < 40){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor2_5;
        }else if(water < 60){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor3_5;
        }else if(water < 80){
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor4_5;
        }else{
            medidor1[parcela].GetComponent<SpriteRenderer>().sprite = medidor5_5;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(treeManager.update){
            for(int i = 0; i < 20; i++){
                medidor1[i].SetActive(treeManager.Mejoras[1]);    
                ChangeSprite(i);
            }
            medidores1.SetActive(treeManager.Mejoras[2]);
            medidores2.SetActive(treeManager.Mejoras[13]);
            medidores3.SetActive(treeManager.Mejoras[20]);
            empleados1.SetActive(treeManager.Mejoras[3]);
            empleados2.SetActive(treeManager.Mejoras[9]);
            empleados3.SetActive(treeManager.Mejoras[16]);
            tractor1.SetActive(treeManager.Mejoras[4]);
            tractor2.SetActive(treeManager.Mejoras[10]);
            tractor3.SetActive(treeManager.Mejoras[17]);
            tanque.SetActive(treeManager.Mejoras[5]);
            aspersores1.SetActive(treeManager.Mejoras[7]);
            aspersores2.SetActive(treeManager.Mejoras[14]);
            if(treeManager.Mejoras[6] && !(treeManager.Mejoras[12] || treeManager.Mejoras[19])){
                financeManager.UpdateSeguro(6);
            }
            else if(treeManager.Mejoras[12] && treeManager.Mejoras[6] && !treeManager.Mejoras[19]){
                financeManager.UpdateSeguro(12);
            }
            else if(treeManager.Mejoras[19] && treeManager.Mejoras[6] && treeManager.Mejoras[12]){
                financeManager.UpdateSeguro(19);
            }
            if(treeManager.Mejoras[8] && treeManager.Mejoras[15] && treeManager.Mejoras[21]){
                financeManager.dinero = 1.2f;
            }
            if(treeManager.Mejoras[2]){
                mapManager.UpdateUnlockedLands(new int[]{11,12,16,17});
            }
            if(treeManager.Mejoras[13]){
                mapManager.UpdateUnlockedLands(new int[]{0, 2, 4, 6, 9,13,18});
            }
            if(treeManager.Mejoras[20]){
                mapManager.UpdateUnlockedLands(new int[]{1,3,5,7,8,10,14,15,19});
            }
            treeManager.update = false;
            
        }
        for(int i = 0; i < 20; i++){
                ChangeSprite(i);
            }
        if(!treeManager.Mejoras[2] & !treeManager.Mejoras[13] &!treeManager.Mejoras[20]){
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodchile.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
            } else if (treeManager.Mejoras[2] & !treeManager.Mejoras[13] & !treeManager.Mejoras[20]){
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodchile.GetComponent<TextMeshProUGUI>().text = "1 parcela";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "1 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "0 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "1 parcelas";
            } else if (treeManager.Mejoras[2] & treeManager.Mejoras[13] & !treeManager.Mejoras[20]){
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodchile.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "1 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "2 parcelas";
            } else{
                prodtrigo.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodmaiz.GetComponent<TextMeshProUGUI>().text = "4 parcelas";
                prodchile.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodtomate.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
                prodfrijol.GetComponent<TextMeshProUGUI>().text = "4 parcelas";
                prodaguacate.GetComponent<TextMeshProUGUI>().text = "3 parcelas";
            }
       
    }
}
