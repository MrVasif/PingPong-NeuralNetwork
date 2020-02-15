using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongManager : MonoBehaviour
{
   [SerializeField] private GameObject paddle;
   [SerializeField] private GameObject ball;
   private Rigidbody2D rigidbodyOfBall;
   private float yVelocity;
   private float paddleMinY = -13.0f;
   private float paddleMaxY =  6.0f;
   private float paddleMaxSpeed = 30.0f;
   
   private ANN ann;
   

   private void Start()
   {
      ann = new ANN(6,1,1,4,0.11f);
      rigidbodyOfBall = ball.GetComponent<Rigidbody2D>();
   }

   private List<double> Run(double bx, double by, double bvx, double bvy, double px, double py, double pv, bool train)
   {
      List<double> inputs = new List<double>();
      List<double> outputs = new List<double>();
      inputs.Add(bx);
      inputs.Add(by);
      inputs.Add(bvx);
      inputs.Add(bvy);
      inputs.Add(px);
      inputs.Add(py);
      outputs.Add(pv);

      if (train)
         return ann.Train(inputs, outputs);
      else
         return ann.Go(inputs, outputs);
   }

   private void Update()
   {
      float yPos = Mathf.Clamp(paddle.transform.position.y + (yVelocity * paddleMaxSpeed * Time.deltaTime),
         paddleMinY, paddleMaxY);
      paddle.transform.position = new Vector3(paddle.transform.position.x,yPos,paddle.transform.position.z);
      
      List<double> output = new List<double>();
      int layerMask = 1 << 8;
      RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, rigidbodyOfBall.velocity, 200,layerMask);
      Debug.DrawLine(ball.transform.position,rigidbodyOfBall.velocity * 200.0f,Color.cyan);

      
      if (hit.collider != null && hit.transform.gameObject.tag == "TopWalls")
      {
         Vector3 reflectionVec = Vector3.Reflect(rigidbodyOfBall.velocity, hit.normal);
         hit = Physics2D.Raycast(hit.point, reflectionVec, 200,layerMask);
      }
      
      
      if (hit.collider != null && hit.transform.gameObject.tag == "CornerWall")
      {
         Debug.Log(yVelocity);
         float distance = (hit.point.y - paddle.transform.position.y);
         output = Run(ball.transform.position.x, ball.transform.position.y,
            rigidbodyOfBall.velocity.x, rigidbodyOfBall.velocity.y,
            paddle.transform.position.x, paddle.transform.position.y,
            distance, true);
         yVelocity = (float) output[0];
      }
      else
      {
         yVelocity = 0.0f;
      }
      
   }
}
