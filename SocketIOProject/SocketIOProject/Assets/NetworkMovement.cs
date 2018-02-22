using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkMovement : MonoBehaviour {

    Vector3 position;
    SocketIOComponent socket;

    // Use this for initialization
    void Start () {
        socket = GameObject.Find("Server").GetComponent<SocketIOComponent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        position = transform.position;
        OnMove(position);
	}

    public void OnMove(Vector3 pos) {
        //Debug.Log("Sending position to server: " + VectorToJSON(pos));
        socket.Emit("move", new JSONObject(VectorToJSON(pos)));
    }

    string VectorToJSON(Vector3 vector) {
        return string.Format(@"{{""x"":""{0}"", ""y"":""{1} ""}}", vector.x, vector.z);
    }
}
