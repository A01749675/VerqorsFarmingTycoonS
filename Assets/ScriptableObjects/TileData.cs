using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public Sprite sprite;
    public TileBase[] tiles;

    public float crop_type, crop_growth;
    public bool isPlanted;


}
