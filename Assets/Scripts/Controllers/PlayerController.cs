using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace PolyStrike
{
	[RequireComponent(typeof(NavMeshAgent))]

	public class PlayerController : MonoBehaviour
	{
		private Camera cameraObj;
		private NavMeshAgent agent;
		[SerializeField] private Animator animator;

		[SerializeField] private Transform gunHolder;
		[SerializeField] private Transform AIMPoint;

		[SerializeField] private List<Waypoint> waypoints;
		private int currentPoint = 0;

		private bool canShoot = true;
		private const float SHOOTDELAY = 0.5f;
		private float shootDelayTimer = 0f;

		private void Start()
		{
			agent = GetComponent<NavMeshAgent>();
			cameraObj = Camera.main;
			GameObject gunObj = Instantiate(GameplayController.Instance.GetActiveWeapon(), gunHolder.position, gunHolder.rotation, gunHolder);
			AIMPoint = gunObj.transform.GetChild(0);
		}

		private void Update()
		{
			shootDelayTimer += Time.deltaTime;

			if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
			{
				if (waypoints[currentPoint].IsPointClear())
				{
					if (canShoot) currentPoint++;
					agent.SetDestination(waypoints[currentPoint].position);
					animator.SetBool("isRun", true);
				}
				else
				{
					if (canShoot && shootDelayTimer >= SHOOTDELAY)
					{
						Vector3 touchPosition = Input.GetTouch(0).position;
						shootDelayTimer = 0f;

						Bullet bulletObj = PoolContainer.Instance.Pool.Get();
						bulletObj.transform.position = AIMPoint.position;

						if (Physics.Raycast(cameraObj.ScreenPointToRay(touchPosition), out RaycastHit hit, 600, ~(1 << 3)))
						{
							bulletObj.SetTarget(hit.collider.transform.position);
						}
						else
						{
							bulletObj.SetTarget(cameraObj.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, 20)));
						}
					}
				}
			}

			if (agent.velocity == Vector3.zero)
			{
				canShoot = true;
				animator.SetBool("isRun", false);
			}
			else
			{
				canShoot = false;
				animator.SetBool("isRun", true);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Finish"))
			{
				GameplayController.FinishTheStage();
			}
		}
	}
}
