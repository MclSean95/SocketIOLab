using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour {
    static SocketIOComponent socket;
    public GameObject playerPrefab;

    Dictionary<string, GameObject> players;

	// Use this for initialization
	void Start () {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("spawn player", OnSpawned);
        socket.On("disconnected", OnDisconnected);
        players = new Dictionary<string, GameObject>();


    }
	// Tell us we are connected
	void OnConnected (SocketIOEvent e) {
        Debug.Log("We are connected");
        socket.Emit("playerhere");
	}

    void OnSpawned(SocketIOEvent e){
        Debug.Log("Player spawned" + e.data);
        var player = Instantiate(playerPrefab);
        players.Add(e.data["id"].ToString(), player);
        Debug.Log("count " + players.Count);
    }

    void OnDisconnected(SocketIOEvent e) {
        Debug.Log("PLayer disconnected" + e.data);

        var id = e.data["id"].ToString();
        var player = players[id];

        Destroy(player);
        players.Remove(id);
    }
}
