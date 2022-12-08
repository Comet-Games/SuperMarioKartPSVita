using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public int randomNum;
    public int maxNumber;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            randomNum = Random.Range(1, maxNumber);
            other.gameObject.GetComponentInParent<ItemManager>().itemNo = randomNum;
            other.gameObject.GetComponentInParent<ItemManager>().GivePlayerItem();
        }
    }
}
