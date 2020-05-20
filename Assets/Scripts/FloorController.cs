using UnityEngine;
using System.Collections;

public class FloorController : MonoBehaviour {

	public float rotationSpeed = 100.0f;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	void Update()
	{
		float hRotation = Input.GetAxis("Horizontal") * rotationSpeed;
		float vRotation = Input.GetAxis("Vertical") * rotationSpeed;
		hRotation *= Time.deltaTime;
		vRotation *= Time.deltaTime;
		transform.Rotate(0, 0, hRotation);
		transform.Rotate(vRotation, 0, 0);
	}

	// void FixedUpdate ()
	// {
	// 	float moveHorizontal = Input.GetAxis("Horizontal");
	// 	float moveVertical = Input.GetAxis("Vertical");

	// 	Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
	// 	rb.AddForce(movement * speed);
	// }
}