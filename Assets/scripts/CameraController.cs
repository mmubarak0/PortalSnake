using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    public float offset;
    private Vector3 PlayerPosition;
    public float offsetSmoothing;

    private void Update() {
        PlayerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, this.transform.position.z);

        transform.position = Vector3.Lerp(this.transform.position, PlayerPosition, offsetSmoothing * Time.deltaTime);
    }
}
