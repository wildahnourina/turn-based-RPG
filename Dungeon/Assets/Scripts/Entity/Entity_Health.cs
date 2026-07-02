using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour
{
    public event Action OnHealthUpdate;

    private Entity entity;
    private Entity_Stats stats;
    private Slider healthBar;
    public bool isDead { get; private set; }

    [SerializeField] private TMP_Text healthText;
    [SerializeField] protected float currentHealth;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        stats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        currentHealth = stats.maxHealth;

        OnHealthUpdate += UpdateHealthBar;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        if (stats.isDefending)
        {
            damage /= 2;
            stats.isDefending = false;
        }

        currentHealth -= damage;
        ShowHealthChange(damage, false);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        
        OnHealthUpdate?.Invoke();
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
            return;

        float newHealth = currentHealth + healAmount;
        float maxHealth = stats.maxHealth;

        currentHealth = Mathf.Min(newHealth, maxHealth);

        ShowHealthChange(healAmount, true);
        OnHealthUpdate?.Invoke();
    }

    public void RestoreFullHealth()
    {
        currentHealth = stats.maxHealth;
        OnHealthUpdate?.Invoke();
    }

    private  void Die()
    {
        isDead = true;
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth / stats.maxHealth;
    }

    public void ShowHealthChange(float value, bool heal)
    {
        healthText.text = heal ? $"Heal +{value}" : $"Damage -{value}";
        healthText.gameObject.SetActive(true);

        Invoke(nameof(HideHealthText), 1f);
    }
    private void HideHealthText() =>  healthText.gameObject.SetActive(false);

    public float GetCurrentHealth() => currentHealth;
}
