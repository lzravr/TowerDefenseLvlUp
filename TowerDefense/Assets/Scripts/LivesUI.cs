using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour {

    public Text lives;
		
	// Update is called once per frame
	void Update () {
        lives.text = PlayerStats.lives.ToString();
	}
}
