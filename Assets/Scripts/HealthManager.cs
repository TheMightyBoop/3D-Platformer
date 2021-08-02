using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public float InvincibilityLength;
    private float InvincibilityCounter;
    private float flashCounter;
    public float flashLength;
    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    public PlayerController player;
    public Renderer playerRenderer;
    public GameObject deathEffect;
    public Image blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        respawnPoint = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(InvincibilityCounter > 0)
        {
            InvincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(InvincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, 
                Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if(blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b,
                Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }
    
    public void HurtPlayer(int damage, Vector3 dir)
    {
        if (InvincibilityCounter <= 0) {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {
                player.KnockBack(dir);

                InvincibilityCounter = InvincibilityLength;
                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        player.gameObject.SetActive(false);
        Instantiate(deathEffect, player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(respawnLength);

        isFadeToBlack = true;
        yield return new WaitForSeconds(waitForFade);
        isFadeToBlack = false;
        isFadeFromBlack = true;

        isRespawning = false;
        player.gameObject.SetActive(true);
        player.transform.position = respawnPoint;
        currentHealth = maxHealth;

        InvincibilityCounter = InvincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;
    }
}
