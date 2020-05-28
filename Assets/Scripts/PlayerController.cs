using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	private const string Tag = "Pick Up";
	public float speed;
	public TextMeshProUGUI countText;
	public TextMeshProUGUI winText;
	private string winTextDefault;
	public float jumpForce; 
	private Vector3 playerStartPosition;
	private Vector3 cameraStartPosition;
	private Rigidbody rb;
	private int count;
	private GameObject[] gems;
	Vector2 movementInput;
	InputMaster controls;
	private GameObject mainCamera;
	
	void Awake() {
		controls = new InputMaster();
		if (controls != null) {
			controls.Gameplay.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
			controls.Gameplay.Move.canceled += ctx => movementInput = Vector2.zero;
			controls.Gameplay.Select.performed += ctx => Jump();
		}
	}
	
	void Start()
	{
		gems = GameObject.FindGameObjectsWithTag("Pick Up");
		mainCamera = GameObject.FindWithTag("MainCamera");
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText();
		playerStartPosition = rb.position;
		cameraStartPosition = mainCamera.transform.position;
		//winTextDefault = winText.text;
		winText.text = "";
	}
	
	void Jump() {
		rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
		
		FindObjectOfType<AudioManager>().Play("Ting");
	}
	
	void OnEnable() {
		if (controls != null) controls.Gameplay.Enable();
	}
	
	void OnDisable() {
		if (controls != null) controls.Gameplay.Disable();
	}

	void FixedUpdate() {
		
		// Vector3 travelDirection = new 
		float fallingVelocity = Physics.gravity.y * (rb.mass * rb.mass) / 10;
		
		Vector3 cameraForward = mainCamera.transform.forward.normalized;
		Vector3 cameraRight = mainCamera.transform.right.normalized;
		cameraForward.y = 0;
		cameraRight.y = 0;
		
		//Vector3 movement = new Vector3(movementInput.x, fallingVelocity, movementInput.y);
		Vector3 movement = cameraForward * movementInput.y + cameraRight * movementInput.x;
		movement.y = fallingVelocity;
		
		if (movement.magnitude < 0) movement = Vector3.zero;
		
		rb.AddForce(movement * speed);
		
		if (transform.position.y < -1000) {
			ResetState();
		}
	}

	void OnTriggerEnter(Collider collision) 
	{
		if (collision.gameObject.CompareTag("Pick Up"))
		{
			collision.gameObject.SetActive(false);
			count = count + 1;
			SetCountText();
			FindObjectOfType<AudioManager>().Play("PickUpSound");
		}
	}

	void SetCountText()
	{
		countText.text = "Score: " + count.ToString();
		if (count > 22) {
			winText.text = winTextDefault;
		}
	}
	
	void ResetState() {
		transform.position = playerStartPosition;
		count = 0;
		SetCountText();
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		mainCamera.transform.position = cameraStartPosition;
		
		foreach (var gem in gems)
		{
			gem.gameObject.SetActive(true);
		}
	}
}