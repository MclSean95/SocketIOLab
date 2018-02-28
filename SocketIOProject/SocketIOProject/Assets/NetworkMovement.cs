using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkMovement : MonoBehaviour {

    Vector3 position;
    public SocketIOComponent socket;

    // Use this for initialization
    void Start () {
        //socket = GameObject.Find("Server").GetComponent<SocketIOComponent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        position = transform.position;
        OnMove(position, h, v);
	}

    public void OnMove(Vector3 pos, float h, float v) {
        //Debug.Log("Sending position to server: " + VectorToJSON(pos, h, v));
        socket.Emit("move", new JSONObject(VectorToJSON(pos, h, v)));
    }

    string VectorToJSON(Vector3 vector, float h, float v) {
        return string.Format(@"{{""x"":""{0}"", ""y"":""{1}"", ""h"":""{2}"", ""v"":""{3}""}}", vector.x, vector.z, h, v);
    }
}
