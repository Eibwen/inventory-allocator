using NUnit.Framework;
using InventoryAllocator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAllocatorTests
{
    /// <summary>
    /// These are tests to just confirm my setup is working.
    /// Do not expect them to be full-featured unit tests
    /// </summary>
    public class HelloWorldTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void When_deserializing_input_an_object_inheriting_dictionary_should_work_as_the_target_object()
        {
            //Arrange & Act
            var result = JsonConvert.DeserializeObject<Order>("{ apple: 5, banana: 5, orange: 5 }");
            
            //Assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result["apple"], Is.EqualTo(5));
        }

        [Test]
        public void When_deseriazling_warehouse_data()
        {
            //Arrange & Act
            var result = JsonConvert.DeserializeObject<List<Warehouse>>(@"[ { name: ""owd"", inventory: { apple: 5, orange: 10 } }, { name: ""dm"", inventory: { banana: 5, orange: 10 } } ]");

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("owd"));
        }

        [Test]
        public void When_deseriazling_warehouse_data_to_my_wrapper_class()
        {
            //Arrange & Act
            var result = JsonConvert.DeserializeObject<WarehouseNetwork>(@"[ { name: ""owd"", inventory: { apple: 5, orange: 10 } }, { name: ""dm"", inventory: { banana: 5, orange: 10 } } ]");

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("owd"));
        }
    }
}