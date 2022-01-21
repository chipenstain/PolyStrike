using UnityEngine;
using UnityEngine.Pool;

namespace PolyStrike
{
    public class PoolContainer : MonoBehaviour
    {
		public static PoolContainer Instance;

		[SerializeField] private GameObject poolObj;
		private IObjectPool<Bullet> pool;
		public IObjectPool<Bullet> Pool
		{
			get
			{
				if (pool == null) pool = new ObjectPool<Bullet>(CreateItem, TakeItem, ReturnItem, DestroyItem, false, 10, 10);
				return pool;
			}
		}

		private void Awake()
		{
			Instance = this;
			poolObj = GameplayController.Instance.GetActiveBullet();
		}

		private  Bullet CreateItem()
		{
			GameObject gameObject = Instantiate(poolObj);

			Bullet bulletController = gameObject.GetComponent<Bullet>();
			bulletController.pool = Pool;

			return bulletController;
		}

		private void ReturnItem(Bullet bulletController)
		{
			bulletController.gameObject.SetActive(false);
		}

		private void TakeItem(Bullet bulletController)
		{
			bulletController.gameObject.SetActive(true);
			bulletController.activeTimer = 0f;
		}
		
		private void DestroyItem(Bullet bulletController)
		{
			Destroy(bulletController.gameObject);
		}
    }
}