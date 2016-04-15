using UnityEngine;
using System.Collections;

namespace Supernova {

	/*
	 * Controls the creation of items
	 */ 
	public class ItemSpawner : MonoBehaviour {

		public Player player;

		// Configures the enemies prefabs (blackhole)
		public GameObject[] enemies;

		// Configures the collectibles prefabs (planets)
		public GameObject[] collects;

		// Configures empty spaces (fillers)
		public GameObject filler;

		void Start() {
			InitialConfig();
		}

		// Populates the screen before the start of the game, allowing the star to start eating planets
		void InitialConfig() {
			for(float i = player.transform.position.y+2; i < transform.position.y+2; i += 2) {
				GameObject collect = SpawnCollect();
				Vector2 pos = collect.transform.position;
				pos.y = i;
				collect.transform.position = pos;

				GameObject fill = SpawnFiller(-pos.x);
				pos = fill.transform.position;
				pos.y = i;
				fill.transform.position = pos;
			}
		}

		// Creates planets
		GameObject SpawnCollect() {
			Vector2 position = transform.position;
			position.x += RandomSide();

			GameObject collect = RandomSpawn(collects, position);
			return collect;
		}

		// Creates fillers
		GameObject SpawnFiller(float positionx) {
			Vector2 position = transform.position;
			position.x = positionx;

			return (GameObject) Instantiate(filler, position, filler.transform.rotation); 
		}

		// Creates anything
		void Spawn() {
			GameObject collect = SpawnCollect();
			SpawnEnemy(-collect.transform.position.x);
		}

		// Creates new objects when making a move
		void OnTouch() {
			if(player.IsDead())
				return;

			Spawn();
		}

		// Creates enemies or fillers, depending on probability
		GameObject SpawnEnemy(float positionx) {
			int should = Random.Range(0, 10);
			if(should > 5)
				return SpawnFiller(positionx);

			Vector2 position = transform.position;
			position.x = positionx;

			return RandomSpawn(enemies, position);
		}

		GameObject RandomSpawn(GameObject[] array, Vector2 position) {
			int index = Random.Range(0, array.Length);
			GameObject prefab = array[index];
			GameObject instance = (GameObject) Instantiate(prefab, position, prefab.transform.rotation);
			Vector2 localScale = instance.transform.localScale;
			float scale = Random.Range(0.3f, 1.0f);
			localScale.x = scale;
			localScale.y = scale;
			instance.transform.localScale = localScale;
			return instance;
		}

		int RandomSide() {
			int rand = Random.Range(-10, 9);
			return rand >= 0 ? 2 : -2;
		}
	}

}