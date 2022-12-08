using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public int itemNo;
    public Item item;

    public void GivePlayerItem()
    {
        item.itemNo = itemNo;
        item.UpdateUI();

    }
}
