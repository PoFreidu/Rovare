using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool IsDead { get; private set; }

    [SerializeField] private AudioSource damageTakeSound;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    
    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private Knockback knockback;
    private Flash flash;
    private const string HEALTH_SLIDER_TEXT = "HealthSlider";
    private const string TOWN_TEXT = "SampleScene";
    private readonly int DEATH_HASH = Animator.StringToHash("Death");
    
    protected override void Awake() {
        base.Awake();
        
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        IsDead = false;
        
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy && canTakeDamage) 
        {
            TakeDamage(5, other.transform);
        }
    }
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }
    
    public void HealPlayer() 
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 3;
            UpdateHealthSlider();
        }
    }
    
    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage || IsDead)
        {
            return;
        }
        
        currentHealth -= damageAmount;
        UpdateHealthSlider();
        CheckIfPlayerDeath();
        
        if(!IsDead)
        {
            knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
            canTakeDamage = false;
            StartCoroutine(DamageRecoveryRoutine());
            damageTakeSound.Play();
        }
    }
    
    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= 0 && !IsDead)
        {
            IsDead = true;
            currentHealth = 0;
            Debug.Log("PlayerDeath");
            GetComponent<Animator>().SetTrigger(DEATH_HASH);
            
            DisableColliders();
            
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }
    
    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(TOWN_TEXT);
    }
    
    private IEnumerator DamageRecoveryRoutine() 
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
    
    private void UpdateHealthSlider() 
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }
        
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    private void DisableColliders()
    {
        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }
    }
}
