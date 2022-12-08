using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoost : MonoBehaviour {
    public KartController kart;
    public int amount;
    public int boost;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            kart.StartBoost(amount, boost);
        }
    }
}
