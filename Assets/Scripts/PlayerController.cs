using UnityEngine;
using UnityEngine.SceneManagement;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Public movement variables to tweak character movement
	public float speed = .1f;
	public float jumpforce = 5f;
	public float dashSpeed = 5f;
	public float turnSpeed = 15f;
	
	// Public gameplay variables
	public Vector3 spawnPosition = new Vector3(0f, 0.5f, 0f);
	public string nextLevel = "";
	public int ammo = 12;
	public int maxAmmo = 12;

	// Public UI variables
	public TextMeshProUGUI ammoText;

	// Private movement variables
	private Vector3 movement;
	private Quaternion rotation = Quaternion.identity;
	private bool isOnGround;

	// Player component references
	private AudioSource pickupSound;
	private Rigidbody rb;

	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// get AudioSource component
		pickupSound = GetComponent<AudioSource>();

		// Set initial ammo
		SetAmmoText ();
	}

	void FixedUpdate ()
	{
		float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
		
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		//Vector3 movement = new Vector3 (horizontal, 0.0f, vertical);
		movement.Set(horizontal, 0f, vertical);
        movement.Normalize ();

		//rb.AddForce (movement * speed);

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			rb.AddForce (movement * dashSpeed, ForceMode.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
		{
			rb.AddForce(Vector3.up * jumpforce,ForceMode.Impulse);
		}

		if (transform.position[1] <= -20)
		{
			transform.position = spawnPosition;
		}

		Vector3 desiredForward = Vector3.RotateTowards (transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
        rotation = Quaternion.LookRotation (desiredForward);

		rb.MovePosition (rb.position + movement * speed);
        rb.MoveRotation (rotation);

		//checkWin();
	}

/*
	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
		
	}
*/

	void OnTriggerEnter(Collider other) 
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

			//SetAmmoText ();

			// Play pickup sound.
			pickupSound.Play();
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			isOnGround = true;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			isOnGround = false;
		}
	}

	public void changeScene()
	{
		SceneManager.LoadScene(nextLevel);
	}

    void SetAmmoText()
	{
		ammoText.text = "Ammo: " + ammo.ToString() + "/" + maxAmmo.ToString();
	}

	/* commented out for now to adjust later during gameplay programming
	void checkWin()
	{
		if (count >= winCount) 
		{
			// Set the text value of your 'winText'
			winTextObject.SetActive(true);
			win = true;

			if (nextLevel != "")
			{
				// set next level button to active
				nextLevelButtonObject.SetActive(true);

			}
		}
	}
	*/

}