using UnityEngine;

namespace PolyStrike
{
    public class EnemyHitpoint : MonoBehaviour
    {
		[SerializeField] private Enemy enemyBase;

		[SerializeField] private int damageOfHit = 1;

		private void OnCollisionEnter(Collision other)
		{
			enemyBase.GetHit(damageOfHit);
		}
    }
}
