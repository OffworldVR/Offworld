using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;            // The number of rounds a single player has to win to win the game.
    public float m_StartDelay = 3f;             // The delay between the start of RoundStarting and RoundPlaying phases.
    public float m_EndDelay = 3f;               // The delay between the end of RoundPlaying and RoundEnding phases.
    public Text m_MessageText;                  // Reference to the overlay Text to display winning text, etc.
    public GameObject m_ShipPrefab;             // Reference to the prefab the players will control.
    public ShipManager[] m_Ships;               // A collection of managers for enabling and disabling different aspects of the tanks.


    private int m_RoundNumber;                  // Which round the game is currently on.
    private WaitForSeconds m_StartWait;         // Used to have a delay whilst the round starts.
    private WaitForSeconds m_EndWait;           // Used to have a delay whilst the round or game ends.
    private ShipManager m_RoundWinner;          // Reference to the winner of the current round.  Used to make an announcement of who won.
    private ShipManager m_GameWinner;           // Reference to the winner of the game.  Used to make an announcement of who won.


    private void Start()
    {
        // Create the delays so they only have to be made once.
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllShips();
        SetCameraTargets();

        // Once the tanks have been created and the camera is using them as targets, start the game.
        StartCoroutine(GameLoop());
    }


    private void SpawnAllShips()
    {
        // For all the tanks...
        for (int i = 0; i < m_Ships.Length; i++)
        {
            // ... create them, set their player number and references needed for control.
            m_Ships[i].m_Instance =
                Instantiate(m_ShipPrefab, m_Ships[i].m_SpawnPoint.position, m_Ships[i].m_SpawnPoint.rotation) as GameObject;
            m_Ships[i].m_PlayerNumber = i + 1;
            m_Ships[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        // Create a collection of transforms the same size as the number of tanks.
        Transform[] targets = new Transform[m_Ships.Length];

        // For each of these transforms...
        for (int i = 0; i < targets.Length; i++)
        {
            // ... set it to the appropriate tank transform.
            targets[i] = m_Ships[i].m_Instance.transform;
        }
    }


    // This is called from start and will run each phase of the game one after another.
    private IEnumerator GameLoop()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());
    }


    private IEnumerator RoundStarting()
    {
        // As soon as the round starts reset the tanks and make sure they can't move.
        ResetAllShips();
        DisableShipControl();

        // Increment the round number and display text showing the players what round it is.
        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        // As soon as the round begins playing let the players control the tanks.
        EnableShipControl();

        // Clear the text from the screen.
        m_MessageText.text = string.Empty;

        // While there is not one tank left...
        while (!OneShipLeft())
        {
            // ... return on the next frame.
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        // Stop tanks from moving.
        DisableShipControl();

        // Clear the winner from the previous round.
        m_RoundWinner = null;

        // See if there is a winner now the round is over.
        m_RoundWinner = GetRoundWinner();

        // If there is a winner, increment their score.
        if (m_RoundWinner != null)
            m_RoundWinner.m_Wins++;

        // Now the winner's score has been incremented, see if someone has one the game.
        m_GameWinner = GetGameWinner();

        // Get a message based on the scores and whether or not there is a game winner and display it.
        string message = EndMessage();
        m_MessageText.text = message;

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return m_EndWait;
    }


    // This is used to check if there is one or fewer tanks remaining and thus the round should end.
    private bool OneShipLeft()
    {
        // Start the count of tanks left at zero.
        int numShipsLeft = 0;

        // Go through all the tanks...
        for (int i = 0; i < m_Ships.Length; i++)
        {
            // ... and if they are active, increment the counter.
            if (m_Ships[i].m_Instance.activeSelf)
                numShipsLeft++;
        }

        // If there are one or fewer tanks remaining return true, otherwise return false.
        return numShipsLeft <= 1;
    }


    // This function is to find out if there is a winner of the round.
    // This function is called with the assumption that 1 or fewer tanks are currently active.
    private ShipManager GetRoundWinner()
    {
        // Go through all the tanks...
        for (int i = 0; i < m_Ships.Length; i++)
        {
            // ... and if one of them is active, it is the winner so return it.
            if (m_Ships[i].m_Instance.activeSelf)
                return m_Ships[i];
        }

        // If none of the tanks are active it is a draw so return null.
        return null;
    }


    // This function is to find out if there is a winner of the game.
    private ShipManager GetGameWinner()
    {
        // Go through all the tanks...
        for (int i = 0; i < m_Ships.Length; i++)
        {
            // ... and if one of them has enough rounds to win the game, return it.
            if (m_Ships[i].m_Wins == m_NumRoundsToWin)
                return m_Ships[i];
        }

        // If no tanks have enough rounds to win, return null.
        return null;
    }


    // Returns a string message to display at the end of each round.
    private string EndMessage()
    {
        // By default when a round ends there are no winners so the default end message is a draw.
        string message = "DRAW!";

        // If there is a winner then change the message to reflect that.
        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        // Add some line breaks after the initial message.
        message += "\n\n\n\n";

        // Go through all the tanks and add each of their scores to the message.
        for (int i = 0; i < m_Ships.Length; i++)
        {
            message += m_Ships[i].m_ColoredPlayerText + ": " + m_Ships[i].m_Wins + " WINS\n";
        }

        // If there is a game winner, change the entire message to reflect that.
        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }


    // This function is used to turn all the tanks back on and reset their positions and properties.
    private void ResetAllShips()
    {
        for (int i = 0; i < m_Ships.Length; i++)
        {
            m_Ships[i].Reset();
        }
    }


    private void EnableShipControl()
    {
        for (int i = 0; i < m_Ships.Length; i++)
        {
            m_Ships[i].EnableControl();
        }
    }


    private void DisableShipControl()
    {
        for (int i = 0; i < m_Ships.Length; i++)
        {
            m_Ships[i].DisableControl();
        }
    }
}
