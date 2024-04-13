using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provides function to scene manager buttons
/// </summary>
public class PlayScene : MonoBehaviour
{
    /// <summary>
    /// Loads the scene for two players
    /// </summary>
    public void PlayGameForTwo()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Loads the scene for three players
    /// </summary>
    public void PlayGameForThree()
    {
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Loads the scene for four players
    /// </summary>
    public void PlayGameForFour()
    {
        SceneManager.LoadScene(3);
    }
}
