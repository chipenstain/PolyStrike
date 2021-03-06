using UnityEngine;

namespace PolyStrike
{
	[RequireComponent (typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
	{ 
		private Rigidbody rigidBody;

		[SerializeField] private float speed = 1f;

		private const float ACTIVETIME = 4f;
		public float activeTimer = 0f;

		private void Awake()
		{
			rigidBody = GetComponent<Rigidbody>();	
		}

		private void Update()
		{
			activeTimer += Time.deltaTime;
			if(activeTimer >= ACTIVETIME)
			{
				PoolContainer.Instance.ReturnPoolObj(this);
			}
		}

		public void SetTarget(Vector3 target)
		{
			transform.LookAt(target, Vector3.up);
			rigidBody.velocity = (target - transform.position).normalized * speed;
		}

		private void OnCollisionEnter(Collision other)
		{
			PoolContainer.Instance.ReturnPoolObj(this);
		}
    }
}
