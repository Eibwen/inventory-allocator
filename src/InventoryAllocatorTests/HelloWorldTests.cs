using NUnit.Framework;
using InventoryAllocator;
using Newtonsoft.Json;
using System;

namespace Tests
{
    public class Tests
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
    }
}