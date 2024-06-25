using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float moveSpeed = 22f;
	[SerializeField] private GameObject particleOnHitPrefabVFX;
	[SerializeField] private bool isEnemyProjectile = false;
	[SerializeField] private float projectileRange = 10f;
	[SerializeField] private int damageAmount = 4;
	[SerializeField] private LayerMask hitLayers;

	private Vector3 startPosition;
	private Vector3 direction = Vector3.right; // Направление движения снаряда

	private void Start() {
		startPosition = transform.position;
	}

	private void Update()
	{
		MoveProjectile();
		DetectFireDistance();
	}

	public void UpdateProjectileRange(float newProjectileRange){
		projectileRange = newProjectileRange;
	}

	public void UpdateMoveSpeed(float newMoveSpeed)
	{
		moveSpeed = newMoveSpeed;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if ((hitLayers.value & (1 << other.gameObject.layer)) > 0) {
			ProcessCollision(other);
		}
	}

	private void ProcessCollision(Collider2D other) 
	{
		// Проверяем, есть ли у объекта скрипт Indestructible
		Indestructible indestructibleComponent = other.GetComponent<Indestructible>();

		if (indestructibleComponent != null) 
		{
		// Обработка столкновения с неразрушаемым объектом
		IndestructibleHit();
		} 
		
		else if (other.gameObject.CompareTag("Enemy") && !isEnemyProjectile) 
		{
		// Обработка столкновения с врагом
		EnemyHit(other);
		}
			
		else if (other.gameObject.CompareTag("Player") && isEnemyProjectile) 
		{
		// Обработка столкновения с игроком
		PlayerHit(other);
		}
	}

	private void EnemyHit(Collider2D enemy) {
		enemy.GetComponent<EnemyEntity>().TakeDamage(damageAmount);
		Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	private void PlayerHit(Collider2D player) {
		player.GetComponent<PlayerHealth>().TakeDamage(damageAmount, transform);
		Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	private void IndestructibleHit() {
		Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
		Destroy(gameObject);
	}

	private void DetectFireDistance() {
		if (Vector3.Distance(transform.position, startPosition) > projectileRange) {
			Destroy(gameObject);
		}
	}

	private void MoveProjectile()
	{
		transform.Translate(direction * Time.deltaTime * moveSpeed);
	}
}
