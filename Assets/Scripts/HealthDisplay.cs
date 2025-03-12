using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{   
    public int health;

    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = playerController.playerHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
