using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Brain : MonoBehaviour
{
    /*
    private ANN ann;
    private double sumSquareError = 0;

    private void Start()
    {
        ann = new ANN(2,1,1,2,0.8);

        List<double> result;

        for (int i = 0; i < 10000; i++)
        {
            sumSquareError = 0;
            result = Train(1, 1, 0);
            sumSquareError += Math.Pow((float) result[0] - 0, 2);
            result = Train(1, 0, 1);
            sumSquareError += Math.Pow((float) result[0] - 1, 2);
            result = Train(0, 1, 1);
            sumSquareError += Math.Pow((float) result[0] - 1, 2);
            result = Train(0, 0, 0);
            sumSquareError += Math.Pow((float) result[0] - 0, 2);
        }
        
        Debug.Log("SSE: " + sumSquareError);

        result = Train(1, 1, 0);
        Debug.Log("1 and 1 " + result[0]);
        result = Train(1, 0, 1);
        Debug.Log("1 and 0 " + result[0]);
        result = Train(0, 1, 1);
        Debug.Log("0 and 1 " + result[0]);
        result = Train(0, 0, 0);
        Debug.Log("0 and 0 " + result[0]);
    }

    private List<double> Train(double i1, double i2, double o)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(i1);
        inputs.Add(i2);
        outputs.Add(o);
        return (ann.Go(inputs, outputs));
    }
    */
}
