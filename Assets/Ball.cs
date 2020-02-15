using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody2D rigidbody;
    private float speed = 4000;

    [SerializeField] private Text savedText;
    [SerializeField] private Text missedText;
    private int savedCount = 0;
    private int missedCount = 0;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        ResetBall();
    }

    private void ResetBall()
    {
        transform.position = startPos;
        rigidbody.velocity = Vector3.zero;
        Vector3 dir = new Vector3(Random.Range(100,200),Random.Range(100,200),0).normalized;
        rigidbody.AddForce(dir * speed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetBall();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("CornerWall"))
        {
            missedCount++;
            missedText.text = missedCount.ToString();
        }         
        else if (other.gameObject.tag.Equals("Player"))
        {
            savedCount++;
            savedText.text = savedCount.ToString();
        }
    }
}
