using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN
{
    public int numInputs; 
    public int numOutputs;
    public int numHidden;
    public int numNPerHidden;
    public double alpha;
    private List<Layer> layers = new List<Layer>();

    public ANN(int nI, int nO, int nH, int nPH, double a)
    {
        numInputs = nI;
        numOutputs = nO;
        numHidden = nH;
        numNPerHidden = nPH;
        alpha = a;

        if (numHidden > 0)
        {
            layers.Add(new Layer(numNPerHidden,numInputs));

            for (int i = 0; i < numHidden - 1; i++)
            {
                layers.Add(new Layer(numNPerHidden,numNPerHidden));
            }
            
            layers.Add(new Layer(numOutputs,numNPerHidden));
        }
        else
        {
            layers.Add(new Layer(numOutputs,numNPerHidden));
        }
    }

    public List<double> Train(List<double> inputValues, List<double> desiredOutput)
    {
        List<double> outputValues = new List<double>();
        outputValues = Go(inputValues, desiredOutput);
        UpdateWeights(outputValues, desiredOutput);
        return outputValues;
    }

    public List<double> Go(List<double> inputValues, List<double> desiredOutput)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();

        if (inputValues.Count != numInputs)
        {
            Debug.Log("AN ERROR OCCURES: INPUT VALUES ");
            return outputs;
        }
        inputs = new List<double>(inputValues);
        for (int i = 0; i < numHidden + 1; i++)
        {
            if (i > 0)
            {
                inputs = new List<double>(outputs);
            }
            outputs.Clear();

            for (int j = 0; j < layers[i].numNeurons; j++)
            {
                double N = 0;
                layers[i].neurons[j].inputs.Clear();

                for (int k = 0; k < layers[i].neurons[j].numInputs; k++)
                {
                    layers[i].neurons[j].inputs.Add(inputs[k]);
                    N += layers[i].neurons[j].weights[k] * inputs[k];
                }

                N -= layers[i].neurons[j].bias;
                if (i == numHidden)
                {
                    layers[i].neurons[j].output = ActivationFunctionOutput(N);
                }
                else
                {
                    layers[i].neurons[j].output = ActivationFunction(N);
                }
                outputs.Add(layers[i].neurons[j].output);
            }
        }

        return outputs;
    }

    private void UpdateWeights(List<double> outputs, List<double> desiredOutput)
    {
        double error;
        for (int i = numHidden; i >= 0; i--)
        {
            for (int j = 0; j < layers[i].numNeurons; j++)
            {
                if (i == numHidden)
                {
                    error = desiredOutput[j] - outputs[j];
                    layers[i].neurons[j].errorGradient = outputs[j] * (1 - outputs[j]) * error;
                }
                else
                {
                    layers[i].neurons[j].errorGradient =
                        layers[i].neurons[j].output * (1 - layers[i].neurons[j].output);
                    double errorGradSum = 0;
                    for (int p = 0; p < layers[i + 1].numNeurons; p++)
                    {
                        errorGradSum += layers[i + 1].neurons[p].errorGradient * layers[i + 1].neurons[p].weights[j];
                    }

                    layers[i].neurons[j].errorGradient *= errorGradSum;
                }

                for (int k = 0; k < layers[i].neurons[j].numInputs; k++)
                {
                    if (i == numHidden)
                    {
                        error = desiredOutput[j] - outputs[j];
                        layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * error;
                    }
                    else
                    {
                        layers[i].neurons[j].weights[k] +=
                            alpha * layers[i].neurons[j].inputs[k] * layers[i].neurons[j].errorGradient;
                    }

                    layers[i].neurons[j].bias += alpha * -1 * layers[i].neurons[j].errorGradient;
                }
                
            }
        }
    }

    private double ActivationFunction(double value)
    {
        return TanH(value);
    }
    
    private double ActivationFunctionOutput(double value)
    {
        return TanH(value);
    }

    private double LeakyRelu(double value)
    {
        if (value < 0) return value * 0.01;
        else return value;
    }

    private double Sigmoid(double value)
    {
        double k = (double) System.Math.Exp(value);
        return k / (1.0f + k);
    }

    private double TanH(double value)
    {
        double k = Math.Exp(-2 * value);
        return 2 / (1.0f + k) - 1;
    }

    private double Sinusoid(double value)
    {
        return Mathf.Sin((float) value);
    }
}
