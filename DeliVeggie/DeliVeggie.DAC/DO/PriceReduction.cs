using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliVeggie.DAC.DO
{
    public class PriceReduction
    {
        public ObjectId Id { get; set; }
        public int DayOfWeek { get; set; }
        public double Reduction { get; set; }
    }
}
