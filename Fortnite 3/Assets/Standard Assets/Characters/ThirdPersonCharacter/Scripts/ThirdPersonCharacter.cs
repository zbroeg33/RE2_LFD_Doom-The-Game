using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		[SerializeField] float m_MovingTurnSpeed = 360;
		[SerializeField] float m_StationaryTurnSpeed = 180;
		[SerializeField] float m_JumpPower = 12f;
		[Range(1f, 4f)][SerializeField] float m_GravityMultiplier = 2f;
		[SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
		[SerializeField] float m_MoveSpeedMultiplier = 1f;
		[SerializeField] float m_AnimSpeedMultiplier = 1f;
		[SerializeField] float m_GroundCheckDistance = 0.1f;

		Rigidbody m_Rigidbody;
		Animator m_Animator;
		bool m_IsGrounded;
		float m_OrigGroundCheckDistance;
		const float k_Half = 0.5f;
		float m_TurnAmount;
		float m_ForwardAmount;
		Vector3 m_GroundNormal;
		float m_CapsuleHeight;
		Vector3 m_CapsuleCenter;
		CapsuleCollider m_Capsule;
		bool m_Crouching;

		private GameObject player = null;

		private Animator playerCharAnimator;

		private Animator playerAnimator;

		void Start()
		{
			m_Animator = GetComponent<Animator>();
			m_Rigidbody = GetComponent<Rigidbody>();
			m_Capsule = GetComponent<CapsuleCollider>();
			m_CapsuleHeight = m_Capsule.height;
			m_CapsuleCenter = m_Capsule.center;
			player = GameObject.FindWithTag("Player");
			m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
			m_OrigGroundCheckDistance = m_GroundCheckDistance;
		}


		public void Move(Vector3 move, bool crouch, bool jump)
		{

			// convert the world relative moveInput vector into a local-relative
			// turn amount and forward amount required to head in the desired
			// direction.
			if (move.magnitude > 1f) move.Normalize();
			move = transform.InverseTransformDirection(move);
			CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, m_GroundNormal);
			m_TurnAmount = Mathf.Atan2(move.x, move.z);
			m_ForwardAmount = move.z;

			ApplyExtraTurnRotation();

			// control and velocity handling is different when grounded and airborne:
			if (m_IsGrounded)
			{
				//HandleGroundedMovement(crouch, jump);
			}
			else
			{
				//HandleAirborneMovement();
			}

			//ScaleCapsuleForCrouching(crouch);
			//PreventStandingInLowHeadroom();

			// send input and other state parameters to the animator
			UpdateAnimator(move);
		}



		void UpdateAnimator(Vector3 move)
		{


			if (player == null) {
				player = GameObject.FindWithTag("Player");
			} else {
				if (playerCharAnimator == null || playerAnimator == null) {
					playerAnimator = player.GetComponent<Animator>();
					foreach (Transform child in player.transform) {
						if (child.ToString() == "Character_Apocalypse (UnityEngine.Transform)") {
							playerCharAnimator = child.GetComponent<Animator>();
							//Debug.Log("found animator: " + playerCharAnimator);	
						}
					
					}
				}

				/*if (playerAnimator == null) {
					Debug.Log("player animator null... searching...");
					playerAnimator = player.GetComponent<Animator>();
				}*/
			}

			if (m_Animator != null) { 
				if (move.x != 0 || move.z != 0) {
					m_Animator.SetBool("isIdle", false);
					m_Animator.SetBool("isAttacking", false);
					m_Animator.SetBool("isWalking", true);
				} else {
					m_Animator.SetBool("isWalking", false);
					m_Animator.SetBool("isAttacking", false);
					m_Animator.SetBool("isIdle", true);
				}

				if (m_Animator != null && player != null) {
					if ((player.transform.position - transform.position).magnitude <= 1.5f) {
						m_Animator.SetBool("isWalking", false);
						m_Animator.SetBool("isIdle", false);
						m_Animator.SetBool("isAttacking", true);
					} else {
						m_Animator.SetBool("isAttacking", false);
						m_Animator.SetBool("isWalking", true);
						m_Animator.SetBool("isIdle", false);
					}
				}

			}
			
		}

		public void AttemptToDamagePlayer() {
			if ((player.transform.position - transform.position).magnitude <= 1.5f) {
				
				//Animator playerAnimator = player.GetComponent<Animator>();
				if (playerCharAnimator != null && playerAnimator != null) {
					playerCharAnimator.SetBool("walkBool", false);
					playerCharAnimator.SetBool("runBool", false);
					playerCharAnimator.SetBool("walkLeftBool", false);
					playerCharAnimator.SetBool("walkRightBool", false);
					playerCharAnimator.SetBool("walkBackBool", false);
					
					playerCharAnimator.SetTrigger("hitTrigger");

					playerAnimator.SetTrigger("hitTrigger");
				} else {
					Debug.Log("one or both animators null");
				}
				//Debug.Log("damage player");
			    
			} else {
				//Debug.Log("player escaped");
			}
		}


	
		void ApplyExtraTurnRotation()
		{
			// help the character turn faster (this is in addition to root rotation in the animation)
			float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
			transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
		}


		void CheckGroundStatus()
		{
			RaycastHit hitInfo;
#if UNITY_EDITOR
			// helper to visualise the ground check ray in the scene view
			Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
			// 0.1f is a small offset to start the ray from inside the character
			// it is also good to note that the transform position in the sample assets is at the base of the character
			if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
			{
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				//m_Animator.applyRootMotion = true;
			}
			else
			{
				m_IsGrounded = false;
				m_GroundNormal = Vector3.up;
				//m_Animator.applyRootMotion = false;
			}
		}
	}
}
