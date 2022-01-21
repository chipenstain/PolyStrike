using UnityEngine;

namespace PolyStrike
{
    public class Enemy : MonoBehaviour
	{
		public Waypoint waypoint;
		[SerializeField] private GameObject ragDoll;

		private bool isKilled = false;
		
		private int hp = 3;

		public void GetHit(int dmg)
		{
			hp -= dmg;

			if (hp <= 0 && !isKilled)
			{
				isKilled = true;
				waypoint.RemoveEnemy(this);
				Destroy(Instantiate(ragDoll, transform.position,transform.rotation,null), 3f);
				Destroy(gameObject);
			}
		}
	}
}
