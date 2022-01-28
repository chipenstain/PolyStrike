using UnityEngine;
using System.Collections.Generic;

namespace PolyStrike
{
    public class PoolContainer : MonoBehaviour
    {
		public static PoolContainer Instance;

		[SerializeField] private GameObject poolObj;
		[SerializeField] private int maxInstance = 10;

		private List<Bullet> pool = new List<Bullet>();

		private void Awake()
		{
			Instance = this;
			poolObj = GameplayController.Instance.GetActiveBullet();
		}

		public Bullet GetPoolObj()
		{
			Bullet bulletController;

			if (pool.Count > 0)
			{
				bulletController = pool[pool.Count - 1];
				bulletController.activeTimer = 0f;
				bulletController.gameObject.SetActive(true);
				pool.Remove(bulletController);
				return bulletController;
			}
			else 
			{
				bulletController = Instantiate(poolObj).GetComponent<Bullet>();
				return bulletController;
			}
		}

		public void ReturnPoolObj(Bullet bulletController)
		{
			if (pool.Count < maxInstance)
			{
				bulletController.gameObject.SetActive(false);
				pool.Add(bulletController);
			}
			else
			{
				Destroy(bulletController.gameObject);
			}
		}
    }
}