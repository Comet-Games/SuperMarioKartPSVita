using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenShell : MonoBehaviour {
	public float Speed;
	private Rigidbody rb;
	public int hits;
	public bool isBouncing;
	public float bounce;
    public AudioClip bounceSound;
    AudioSource audioSource;

    // Use this for initialization
    void Awake () 
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();

		//rb.AddForce(0, 0, Speed, ForceMode.Impulse);
	}

	void Start()
	{
        //rb.AddForce(transform.forward, ForceMode.Impulse);
    }

	void Update()
	{
    }

	void OnCollisionEnter(Collision collision)
	{
		hits--;
		if (hits == 0)
		{
			Destroy(gameObject);
		}
		if(collision.gameObject.tag == "Hazard")
		{
			Destroy(gameObject);
		}
        audioSource.PlayOneShot(bounceSound);
        rb.AddForce(collision.contacts[0].normal * bounce);
	}
}
