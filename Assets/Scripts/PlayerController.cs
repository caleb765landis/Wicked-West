using UnityEngine;
using UnityEngine.SceneManagement;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	
	// Public movement variables to tweak character movement
	public float speed = .1f;
	public float dashSpeed = 10f;
	public float dashForce = 5f;
	public float jumpForce = 5f;
	
	// Public gameplay variables
	public Vector3 spawnPosition = new Vector3(0f, 0.5f, 0f);
	public string nextLevel = "";

	// Public weapon variables
	public GameObject currentWeapon;
	public GameObject pistol;
	public GameObject rifle;

	// Public UI variables
	public Camera cam;

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
		rb = GetComponent<Rigidbody>();

		currentWeapon = pistol;

		// get AudioSource component
		pickupSound = GetComponent<AudioSource>();
	}

	void FixedUpdate ()
	{
		checkMovement();
		checkRotation();
		checkWeaponInput();
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
		Weapon cw = currentWeapon.GetComponent<Weapon>();
		
		if (Input.GetButtonDown("Fire1"))
		{
			cw.fire();
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			cw.reload();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			currentWeapon.SetActive(false);
			pistol.SetActive(true);
			currentWeapon = pistol;
			cw.SetAmmoText();
		} 
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			currentWeapon.SetActive(false);
			rifle.SetActive(true);
			currentWeapon = rifle;
			cw.SetAmmoText();
		}

	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("PickUp"))
		{
			other.gameObject.SetActive (false);

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