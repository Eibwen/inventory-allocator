using NUnit.Framework;
using InventoryAllocator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAllocatorTests
{
    [TestFixture]
    public class InventoryAllocatorTests
    {
        [Test]
        public void When_exact_inventory_matches()
        {
            //Arrange
            var orderInput = "";
            var warehouseInput = "";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act
            var result = allocator.Allocate(order, warehouse);

            //Assert
            
        }


    }
}