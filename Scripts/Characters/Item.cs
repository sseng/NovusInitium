using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {


    public string _Name;
    public string _Desc;
    public float _PhysicalPower;
    public bool _IsEquip;
    public EquipType BodyLocation;

}

public enum EquipType
{
    Helmet,
    Shoulder,
    Chest,
    Pant,
    Glove,
    Bracer,
    LHand,
    RHand,
    Boot,
    LEar,
    REar,
    Neck,
    LRing,
    RRing
    
}