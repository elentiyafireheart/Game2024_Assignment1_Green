using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //player hurt
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

}
