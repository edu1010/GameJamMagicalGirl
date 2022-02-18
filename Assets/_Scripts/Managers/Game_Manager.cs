using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    List<IRestartGameElements> restartGameElements;
    Player_Manager player;
    static Game_Manager gameManager = null;
    public Level_Manager levelManager;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }

        restartGameElements = new List<IRestartGameElements>();
    }

    public void AddRestartGameElements(IRestartGameElements RestartGameElement)
    {
        restartGameElements.Add(RestartGameElement);
    }

    public void RestartGame()
    {
        foreach (IRestartGameElements l_RestartGameElement in restartGameElements)
        {
            l_RestartGameElement.RestartGame();
        }
    }

    static public Game_Manager GetGameController()
    {
        return gameManager;
    }

    public void SetPlayer(Player_Manager Player)
    {
        player = Player;
    }

    public Player_Manager GetPlayer()
    {
        return player;
    }

    public void SetLevelData(Level_Manager l_LevelData)
    {
        levelManager = l_LevelData;
    }

    public Level_Manager GetLevelData()
    {
        return levelManager;
    }

    public void ClearIRestartGameElementsList()
    {
        restartGameElements.Clear();
    }
}
