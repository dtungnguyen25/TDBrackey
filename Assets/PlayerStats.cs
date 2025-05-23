using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    // Static variable to keep track of the player's money
    public int startMoney = 400;
    // Initial amount of money the player starts with

    private void Start()
    {
        money = startMoney;
        // Initialize the player's money at the start of the game
    }
}
