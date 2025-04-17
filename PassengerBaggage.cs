using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public struct BaggageItem
{
    public string Name;
    public int Weight;
    
    public BaggageItem(string name, int weight)
    {
        Name = name;
        Weight = weight;
    }
}

[Serializable]
public class PassengerData
{
    public List<BaggageItem> Baggage;
    
    public PassengerData()
    {
        Baggage = new List<BaggageItem>();
    }
}