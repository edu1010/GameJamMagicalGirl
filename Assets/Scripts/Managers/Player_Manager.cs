using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class Player_Manager : MonoBehaviour, IRestartGameElements
{
    Game_Manager gameManager;
    Vector3 initialPos;
    HealthComponent playerHealth;

    void Awake()
    {
        gameManager = Game_Manager.GetGameController();
        gameManager.SetPlayer(this);
    }

    private void Start()
    {
        gameManager.AddRestartGameElements(this);

        playerHealth = GetComponent<HealthComponent>();
        initialPos = transform.position;
    }

    private void Update()
    {
        if (playerHealth.IsDead())
        {
            gameManager.RestartGame();
        }
    }

    public void RestartGame()
    {
        playerHealth.Resurrect();
        transform.position = initialPos;
    }
}
