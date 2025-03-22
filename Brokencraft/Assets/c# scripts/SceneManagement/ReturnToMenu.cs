using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReturnToMenu : MonoBehaviour
{
    public static void Return()
    {
        PlayerInventory.ClearCrafting();
        ScoreDisplay.UpdateHighScores();
        SceneManager.LoadScene(0);
    }
}
