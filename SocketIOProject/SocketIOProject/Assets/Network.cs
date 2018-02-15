using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;
    public GameObject playerPrefab;


	// Use this for initialization
	void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn player", onSpawned);

        
	}
	// Tell us we are connected
	void OnConnected (SocketIOEvent e) {
        Debug.Log("We are connected");
        socket.Emit("playerhere");
	}

    void onSpawned(SocketIOEvent e){
        Debug.Log("Player spawned");
        Instantiate(playerPrefab);
    }
}
