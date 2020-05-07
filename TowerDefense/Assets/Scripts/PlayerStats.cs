using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public static int money;
    public static int lives;
    public int startLives = 10;
    public int startMoney = 400;
    public static int rounds;

    private void Start()
    {
        rounds = 0;
        money = startMoney;
        lives = startLives;
    }


}
