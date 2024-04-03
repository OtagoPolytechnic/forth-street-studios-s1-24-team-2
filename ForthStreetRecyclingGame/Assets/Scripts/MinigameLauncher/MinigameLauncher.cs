/*
 * File: MinigameLauncher.cs
 * Purpose: Switch to minigame and back to main scene after minigame success
 * Author: Johnathan
 * Contributions: Assisted by GitHub Copilot
 */

using UnityEngine;

/// <summary>
/// This class is used to launch the minigame and return to the main scene after the minigame is completed.
/// </summary>
/// <remarks>
/// Currently a lot of the behaviour that should be in this class is in the RotateMonitor class.
/// As we add more minigames, I'll try to find a more elegant solution to this.
/// </remarks>
public class MinigameLauncher : MonoBehaviour
{
    public RotateMonitor rotateMonitor;
    public TestMinigameManager testMinigameManager;

    /// <summary>
    /// Check if the minigame is complete and rotate the monitor back to the start
    /// </summary>
    void Update()
    {
        if (!rotateMonitor.isRotating && testMinigameManager.success)
        {
            testMinigameManager.success = false;
            rotateMonitor.RotateToStart();
        }
    }
}
