using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour, IRestartGameElements
{
    //public List<IEnemy> Enemies;

    private void Awake()
    {
        //Enemies = new List<IEnemy>();
    }

    void Start()
    {
        Game_Manager.GetGameController().SetLevelData(this);
        Game_Manager.GetGameController().AddRestartGameElements(this);
    }

    public void RestartGame()
    {
        //foreach (Enemy_Basic E_B in Enemies)
        //{
        //    E_B.gameObject.SetActive(true);
        //}
    }
}
