using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

//Difference between List and Collection
//When we add neuron layer, which is collection of neurons, and we want to make it connectable as well.
//If class : parent we can say that parent for class is already occupied
//Topic to refresh - IEnumerable!!!

namespace DesignPatterns.StructuralPatterns.Composite
{
    class NeuralNetwork
    {
        //public static void Main (string [] args)
        //{
        //    //neuron to neuron
        //    var neuron1 = new Neuron();
        //    var neuron2 = new Neuron();
        //    neuron2.ConnectTo(neuron1);

        //    var layer1 = new NeuronLayer();
        //    var layer2 = new NeuronLayer();
        //    //TODO
        //    //neuron to layer
        //    neuron1.ConnectTo(layer1);

        //    //layer to neuron
        //    //layer2.ConnectTo(neuron2);

        //    //layer to layer
        //    //layer1.ConnectTo(layer2);
        //}
    }

    public class Neuron:IEnumerable<Neuron>
    {
        public float Value;
        public List<Neuron> In, Out;

        public Neuron()
        {
            In = new List<Neuron>();
            Out = new List<Neuron>();
        }
        public IEnumerator<Neuron> GetEnumerator()
        {
            //????
            yield return this;
        }
        //public void ConnectTo(Neuron other)
        //{
        //    Out.Add(other);
        //    other.In.Add(this);
        //}
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    
    public class NeuronLayer : Collection<Neuron>
    {

    }

    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other)) return;
            foreach(var from in self)
            {
                foreach(var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
            }
        }
    }
}
