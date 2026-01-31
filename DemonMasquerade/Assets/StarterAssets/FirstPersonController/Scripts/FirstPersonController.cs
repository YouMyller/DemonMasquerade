using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 4.0f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;
		[Tooltip("GameManager")]
		public GameObject GameManagerO;
		public float GM_SpeedMul = 0;
		public float GM_reverseMovementMultiplier = 1;
		public float GM_JumpHeightMultiplier;
		public GameObject MaskGreen;
		public GameObject bullet;
		public float bulletForce;
		public float firerate;
		public float reloadTime;
		public bool spreadshot;
		public GameObject rSpread;
		public GameObject lSpread;
		public int hp;
		public int maxHP;
		public AudioSource JukeBox;
		public AudioSource ShootSFX;
		public AudioSource MaskPickupSFX;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

        private UIManager uiManager;


#if ENABLE_INPUT_SYSTEM
        private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;

        private const float _threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
				#if ENABLE_INPUT_SYSTEM
				return _playerInput.currentControlScheme == "KeyboardMouse";
				#else
				return false;
				#endif
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}
		}

		private void Start()
		{
            if(GameObject.Find("UIManager") != null)
                uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

            _controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();
			GM_SpeedMul = GameManagerO.GetComponent<GameManagerScript>().speedMultiplier;
			GM_reverseMovementMultiplier = GameManagerO.GetComponent<GameManagerScript>().reverseMovementMultiplier;
			GM_JumpHeightMultiplier = GameManagerO.GetComponent<GameManagerScript>().JumpHeightMultiplier;
			hp = maxHP;
			


#if ENABLE_INPUT_SYSTEM
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
		}



		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();

			firerate += Time.deltaTime;

			if (_input.test1 && firerate > reloadTime)
			{
				int randInt = UnityEngine.Random.Range(1, 6);
				Debug.Log(randInt);

				if(randInt < 1)
                {
					randInt = 1;
                }
				else if(randInt > 5)
                {
					randInt = 5;
                }

				PickedMask(randInt);
				firerate = 0;
			}

            if (_input.Shoot && firerate > reloadTime)
            {

				if(spreadshot == false)
                {
					/*GameObject bulletInstance;
					bulletInstance = Instantiate(bullet, MaskGreen.transform.position, MaskGreen.transform.rotation);
					bulletInstance.GetComponent<Rigidbody>().AddForce(MaskGreen.transform.forward * bulletForce);*/

					GameObject Ammo = ObjectPool.SharedInstance.GetAmmo();
					Ammo.transform.position = MaskGreen.transform.position;
					Ammo.transform.rotation = MaskGreen.transform.rotation;
					
					//Ammo.transform.localScale = transform.localScale / 2;
					Ammo.SetActive(true);
					Ammo.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
					Ammo.GetComponent<Rigidbody>().AddForce(MaskGreen.transform.forward * bulletForce);

					//Destroy(bulletInstance, 5);
					//Invoke("RemoveBullet(Ammo)", 3.0f);
					firerate = 0;
				}

                else
                {
					/*GameObject bulletInstance;
					bulletInstance = Instantiate(bullet, MaskGreen.transform.position, MaskGreen.transform.rotation);
					bulletInstance.GetComponent<Rigidbody>().AddForce(MaskGreen.transform.forward * bulletForce);

					GameObject bulletInstance2;
					bulletInstance2 = Instantiate(bullet, rSpread.transform.position, rSpread.transform.rotation);
					bulletInstance2.GetComponent<Rigidbody>().AddForce(rSpread.transform.forward * bulletForce);

					GameObject bulletInstance3;
					bulletInstance3 = Instantiate(bullet, lSpread.transform.position, lSpread.transform.rotation);
					bulletInstance3.GetComponent<Rigidbody>().AddForce(lSpread.transform.forward * bulletForce);

					Destroy(bulletInstance, 5);
					Destroy(bulletInstance2, 5);
					Destroy(bulletInstance3, 5);*/

					GameObject Ammo = ObjectPool.SharedInstance.GetAmmo();
					Ammo.transform.position = MaskGreen.transform.position;
					Ammo.transform.rotation = MaskGreen.transform.rotation;
					//Ammo.transform.localScale = transform.localScale / 2;
					Ammo.SetActive(true);
					Ammo.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
					Ammo.GetComponent<Rigidbody>().AddForce(MaskGreen.transform.forward * bulletForce);

					GameObject Ammo2 = ObjectPool.SharedInstance.GetAmmo();
					Ammo2.transform.position = MaskGreen.transform.position;
					Ammo2.transform.rotation = MaskGreen.transform.rotation;
					//Ammo.transform.localScale = transform.localScale / 2;
					Ammo2.SetActive(true);
					Ammo2.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
					Ammo2.GetComponent<Rigidbody>().AddForce(rSpread.transform.forward * bulletForce);

					GameObject Ammo3 = ObjectPool.SharedInstance.GetAmmo();
					Ammo3.transform.position = MaskGreen.transform.position;
					Ammo3.transform.rotation = MaskGreen.transform.rotation;
					//Ammo.transform.localScale = transform.localScale / 2;
					Ammo3.SetActive(true);
					Ammo3.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
					Ammo3.GetComponent<Rigidbody>().AddForce(lSpread.transform.forward * bulletForce);

					firerate = 0;
				}

				ShootSFX.Play();
				
			}
            


		}

		private void LateUpdate()
		{
			CameraRotation();
		}

		private void GroundedCheck()
		{
			// set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
			Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
		}

		private void CameraRotation()
		{
			// if there is an input
			if (_input.look.sqrMagnitude >= _threshold)
			{
				//Don't multiply mouse input by Time.deltaTime
				float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
				_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
				_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

				// clamp our pitch rotation
				_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

				// Update Cinemachine camera target pitch
				CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

				// rotate the player left and right
				transform.Rotate(Vector3.up * _rotationVelocity);
			}
		}

		private void Move()
		{
			// set target speed based on move speed, sprint speed and if sprint is pressed
			float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x * GM_reverseMovementMultiplier + transform.forward * _input.move.y * GM_reverseMovementMultiplier;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime ) * GM_SpeedMul + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt((JumpHeight * GM_JumpHeightMultiplier) * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}


		public void PickedMask(int num)
        {
			switch(num) {


				case 1:    //Reverse controls + Reload speed
					GM_reverseMovementMultiplier = -1;
					reloadTime = 0.5f;
					GM_SpeedMul = 1;
					bulletForce = 750;
					GM_JumpHeightMultiplier = 1;
					spreadshot = false;
					maxHP = 5; //test number
                    uiManager.UpdateHealth(maxHP);
                    JukeBox.pitch = -1;
					break;
				case 2:     //Spreadshot + Movement speed down
					spreadshot = true;
					GM_SpeedMul = 0.95f;
					GM_reverseMovementMultiplier = 1;
					reloadTime = 2;
					bulletForce = 750;
					GM_JumpHeightMultiplier = 1;
					maxHP = 5; //test number
                    uiManager.UpdateHealth(maxHP);
                    JukeBox.pitch = 1;
					break;
				case 3:     //Super Jump + slow bullets
					GM_JumpHeightMultiplier = 3;
					bulletForce = 250;
					GM_reverseMovementMultiplier = 1;
					GM_SpeedMul = 1;
					reloadTime = 2;
					spreadshot = false;
					maxHP = 5; //test number
                    uiManager.UpdateHealth(maxHP);
                    JukeBox.pitch = 1;
					break;
				case 4:   // 1 HP + Fully powered
					GM_JumpHeightMultiplier = 3;
					spreadshot = true;
					reloadTime = 0.5f;
					GM_reverseMovementMultiplier = 1;
					GM_SpeedMul = 1.05f;
					maxHP = 1;
					bulletForce = 1250;
					hp = maxHP;
                    uiManager.UpdateHealth(hp);
					JukeBox.pitch = 1;
					break;
				case 5:  //Move speed down + reload speed up;
					GM_SpeedMul = 0.95f;
					reloadTime = 1;
					GM_reverseMovementMultiplier = 1;
					bulletForce = 750;
					GM_JumpHeightMultiplier = 1;
					spreadshot = false;
					maxHP = 5; //test number
                    uiManager.UpdateHealth(maxHP);
                    JukeBox.pitch = 1;
					break;

			}


			MaskPickupSFX.Play();


		}

		void OnTriggerEnter(Collider other)
		{
			if (other.tag == "MaskCollectible")
			{
				int randInt = UnityEngine.Random.Range(1, 6);
				Debug.Log(randInt);

				if (randInt < 1)
				{
					randInt = 1;
				}
				else if (randInt > 5)
				{
					randInt = 5;
				}

				PickedMask(randInt);

                uiManager.AddScore();

				//Destroy(other.gameObject);
				other.gameObject.SetActive(false);
			}
		}

        

    }
}