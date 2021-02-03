using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliVeggie.DAC.DO
{
    public class Product
    {

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public double Price { get; set; }




    }
}
