using UnityEngine;
using System.Collections;

public class Eventthrower : MonoBehaviour {

    public Player player1;
    public Player player2;
    public Timer timer;
	// Use this for initialization
	void Start () {
        timer = new Timer(6);
        timer.AddCallback(2, P1vsP2);
        timer.AddCallback(4, P2vsP1);
        timer.AddCallback(6, P1Suicide);
        timer.Start();
	}

    void P1vsP2()
    {
        Event.dispatch(new PlayerDeathEvent(player1, player2));
    }

    void P2vsP1()
    {
        Event.dispatch(new PlayerDeathEvent(player2, player1));
    }

    void P1Suicide()
    {
        Event.dispatch(new PlayerDeathEvent(player1, player1));
    }
	// Update is called once per frame
	void Update () {
        timer.Update();
	}
}
