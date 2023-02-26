using UnityEngine;
using UnityEngine.SceneManagement;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Public movement variables to tweak character movement
	public float speed = .1f;
	public float dashSpeed = 10f;
	public float turnSpeed = 15f;
	public float jumpForce = 5f;
	public float dashForce = 5f;
	
	// Public gameplay variables
	public Vector3 spawnPosition = new Vector3(0f, 0.5f, 0f);
	public string nextLevel = "";
	public int ammo = 12;
	public int maxAmmo = 12;
	public GameObject pistol;

	// Public UI variables
	public Camera cam;
	public TextMeshProUGUI ammoText;

	// Private movement variables
	private Vector3 movement;
	private float movementSpeed;
	private Quaternion rotation = Quaternion.identity;
	private bool isOnGround;
	//private bool dashing = false;

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
		checkMovement();
		checkRotation();
		checkWeaponInput();

		//checkWin();
	}

	void checkMovement()
	{
		float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");
		
		movementSpeed = speed;
		movement.Set(horizontal, 0f, vertical);
        movement.Normalize ();

		if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
		{
			rb.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			rb.AddForce (movement * dashForce, ForceMode.Impulse);
		}

		rb.MovePosition (rb.position + movement * movementSpeed);

		if (transform.position[1] <= -20)
		{
			transform.position = spawnPosition;
		}
	}

	void checkRotation()
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
		}
	}

	void checkWeaponInput()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			pistol.GetComponent<Weapon>().fire();
		}
	}

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