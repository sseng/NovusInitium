using UnityEngine;
using System.Collections.Generic;

public class Bag : MonoBehaviour {

    #region properties
    public int _SlotHorizontalPixel;
    public int _SlotVerticalPixel;
    public int _SlotPixelOffset;
    public int _BorderPixelOffset;
    public int _TitleBarPixelOffset;

    public int _MaxRow;
    public int _MaxColumn;

    private int _Width;
    private int _Height;
    private int _TotalSlotSize; //derive from maxrow 8 maxcolum

    private bool _OpenInvBag=false;
    private Rect InvBagRect=new Rect(0, 0, 0, 0); //will be redefine after in the openinvbag
    public List<Item> BagItem=new List<Item>();
    #endregion

    void OnGUI()
    {
        if (_OpenInvBag)
            OpenInvBag();
    }
    void OpenInvBag()
    {
        _TotalSlotSize = _MaxRow * _MaxColumn;
        _Width = ((_SlotHorizontalPixel+_SlotPixelOffset) * _MaxRow) -_SlotPixelOffset; //not including borders ..take one offset off for border location
        _Height =(( _SlotVerticalPixel+_SlotPixelOffset) * _MaxColumn)-_SlotPixelOffset;//not including title
        _Width +=  (_BorderPixelOffset*2) ;//include offset
        _Height+=  _BorderPixelOffset + _TitleBarPixelOffset; //include offset
        InvBagRect.width = _Width;
        InvBagRect.height = _Height;
        InvBagRect = GUI.Window(0, InvBagRect, BagFunc, "Inventory");
    }

    void BagFunc(int winID)
    {
        //add boxes for now

        int x = _BorderPixelOffset;
        int y = _TitleBarPixelOffset;
        Rect[] itemSlots = new Rect[_TotalSlotSize];
        for (int i=0; i<_TotalSlotSize; i++)
        {
            if(x+_SlotHorizontalPixel+_SlotPixelOffset>=_Width-_BorderPixelOffset)  //if the next box' x coordinate is higher than teh max width move below to the next row.
            {
                x=_BorderPixelOffset;
                y+=_SlotPixelOffset+_SlotVerticalPixel;//add teh offset and the pixel fo the box size setup in teh spetor to the next y coordinate
            }
            else
            {
                if(i!=0) //not the first item
                    x+=_SlotPixelOffset+_SlotHorizontalPixel; //add the pixel size slot and the offset
            }
            itemSlots[i]=new Rect(x,y,_SlotHorizontalPixel,_SlotVerticalPixel);
            if(GUI.Button(itemSlots[i],(i+1).ToString()))
            {
                //if the button is click add to charcter equip.

            }
              
        }
        //drag location
        GUI.DragWindow(new Rect(0,0,_Width,_BorderPixelOffset));
        var dragItem = new Rect();
        if (Event.current.type == EventType.MouseDown)
        {

            foreach (Rect r in itemSlots)
            {
                if(r.Contains(Event.current.mousePosition))
                {
                    dragItem=r;
                    break;

                }
                   

            }
        }
        var RepItem = new Rect();
        if (Event.current.type== EventType.MouseUp && dragItem!=null)
        {
            for(int i=0; i<_TotalSlotSize;i++)
            {
                if(itemSlots[i].Contains(Event.current.mousePosition))
                {
                    RepItem=itemSlots[i];
                    itemSlots[i]=dragItem;
                    dragItem=RepItem;
                    break;
                }

            }
        }

    }
    void Start()
    {}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(_OpenInvBag)
                _OpenInvBag=false;
            else
                _OpenInvBag=true;
        }
    }
}
