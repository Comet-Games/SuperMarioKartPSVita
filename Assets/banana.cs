using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banana : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            Destroy(gameObject);
        }
    }
}
