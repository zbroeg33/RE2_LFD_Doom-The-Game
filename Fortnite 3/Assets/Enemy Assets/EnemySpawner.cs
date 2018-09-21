using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

	public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public int numberOfEnemies;

    public int numberOfBullets;

    public override void OnStartServer()
    {
        for (int i=0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(transform.position.x - 10.0f, transform.position.x + 10.0f),
                0.0f,
                Random.Range(transform.position.z -10.0f, transform.position.z + 10.0f));

            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                Random.Range(0,180), 
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }

        for (int i=0; i < numberOfBullets; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(transform.position.x - 10.0f, transform.position.x + 10.0f),
                0.0f,
                Random.Range(transform.position.z -10.0f, transform.position.z + 10.0f));

            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                Random.Range(0,180), 
                0.0f);

            var bullet = (GameObject)Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(bullet);
        }
    }
}
