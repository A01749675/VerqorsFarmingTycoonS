using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public Sprite sprite;
    public TileBase[] tiles; //lista de tiles

    public int crop_type, quantity; //valores de cantidad y tipo de cultivo
    


}
