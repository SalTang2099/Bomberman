using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startgame : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene("Bomberman");
    }

    public void PlayAILevel()
    {
        SceneManager.LoadScene("BombermanTest");
    }

}
