using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PolyStrike
{
    public class GameplayController : MonoBehaviour
    {
		public static GameplayController Instance;

		[SerializeField] private List<GameObject> weaponObj;
		[SerializeField] private List<GameObject> bulletObj;
		private int activeWeapon = 0;

		public int ActiveWeapon { get => activeWeapon; set => activeWeapon = value; }

		private void Awake()
		{
			Instance = this;
		}

		public GameObject GetActiveWeapon()
		{
			return weaponObj[activeWeapon];
		}
		
		public GameObject GetActiveBullet()
		{
			return bulletObj[activeWeapon];
		}

        public static void FinishTheStage()
		{
			SceneManager.LoadScene(0);
		}
    }
}
