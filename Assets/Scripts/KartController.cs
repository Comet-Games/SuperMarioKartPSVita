using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour {

	[SerializeField] float acceleration, gravity, steering, drifting;
	[SerializeField] Rigidbody sphere;
	[SerializeField] Animator spriteVisual;
	public bool testing;

	private float speed, currentSpeed;
	private float rotate, currentRotation;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = sphere.transform.position;

        if (Input.GetButton("Fire1") || (Input.GetButton("Cross")))
		{
			speed = acceleration;
		}

		if(testing == false)
		{
			if((Input.GetAxis("Left Stick Horizontal") != 0f))
			{
				int dir = Input.GetAxis("Left Stick Horizontal") > 0f ? 1 : -1;
				float amount = Mathf.Abs(Input.GetAxis("Left Stick Horizontal"));
				Steer(dir, amount);
			}
            spriteVisual.SetFloat("Horizontal", Input.GetAxis("Left Stick Horizontal"));
        }
        if(testing == true)
		{
            if ((Input.GetAxis("Horizontal") != 0f))
            {
                int dir = Input.GetAxis("Horizontal") > 0f ? 1 : -1;
                float amount = Mathf.Abs(Input.GetAxis("Horizontal"));
                Steer(dir, amount);
            }

            spriteVisual.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        }

		currentSpeed = Mathf.SmoothStep(currentSpeed, speed, 12f * Time.deltaTime);
		speed = 0f;
		currentRotation = Mathf.Lerp(currentRotation, rotate, 4f * Time.deltaTime);
		rotate = 0f;
	}

	private void FixedUpdate()
	{
		sphere.AddForce(transform.forward * currentSpeed, ForceMode.Acceleration);

		sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

		transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, transform.eulerAngles.y + currentRotation, 0f), 5f * Time.deltaTime);
	}

	void Steer(int dir, float amount)
	{
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetButton("Right Shoulder")))
        {
            rotate = (drifting * dir) * amount;
        }
		else
		{
			rotate = (steering * dir) * amount;

		}
	}
}
