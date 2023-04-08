using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptBot : MonoBehaviour
{
    public GameObject player1;
    public GameObject bot;
    public Transform Transform;
    public Camera cam;
    public Vector3 camera_target_position;
    public float step, camsize, distance_to_target_pos, distance_btw_players;
    void Start()
    {
        player1 = GameObject.Find("Player1");
        bot = GameObject.Find("Bot");
        Transform = GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        camera_target_position = new Vector3((player1.transform.position.x + bot.transform.position.x) / 2, transform.position.y, transform.position.z);
        Transform.position = Vector3.MoveTowards(Transform.position, camera_target_position, step);
        distance_to_target_pos = Vector2.Distance(Transform.position, camera_target_position);
        distance_btw_players = Vector2.Distance(player1.transform.position, bot.transform.position);

        if (distance_to_target_pos > 7)
            step += 0.0005f;

        if (distance_to_target_pos < 6)
            step -= 0.0005f;

        if (step < 0)
            step = 0;


        cam.orthographicSize = distance_btw_players / 3;

        if (cam.orthographicSize < 11.3f)
            cam.orthographicSize = 11.3f;
    }
}
