using System.Collections.Generic;
using UnityEngine;

namespace PolyStrike
{
    public class Waypoint : MonoBehaviour
    {
		public Vector3 position;
		[SerializeField] private List<Enemy> enemys;

		private void Awake()
		{
			position = transform.position;
			for (int i = 0; i < enemys.Count; i++)
			{
				enemys[i].waypoint = this;
			}
		}

		public bool IsPointClear()
		{
			return enemys.Count == 0 ? true : false;
		}

		public void RemoveEnemy(Enemy enemy)
		{
			enemys.Remove(enemy);
		}
    }
}
