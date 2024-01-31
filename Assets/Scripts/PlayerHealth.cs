using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{

    private float health;
    [Header("Health Bar")]
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    [Header("Damage Overlay")]
    public Image overlay;
    public float fadeSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        health = Mathf.Clamp(health,0,maxHealth); //clamps health between 0 and 100
        //Debug.Log(health);
        
        if (health <= 30)
        {
            if (overlay.color.a != 0.8)//set overlay a value to 0.8 otherwise fade to 0
            {
                float tempAlpha = overlay.color.a;
                tempAlpha = Mathf.Clamp(tempAlpha,0,0.8f);
                tempAlpha += Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }

        } else if (overlay.color.a > 0)
        {
            //fade image
            float tempAlpha = overlay.color.a;
            tempAlpha -= Time.deltaTime * fadeSpeed;
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha); //takes alpha, sets to alpha - time passed in delta and sets again
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    public void RestoreHealth(float restored)
    {
        health += restored;
    }
}
