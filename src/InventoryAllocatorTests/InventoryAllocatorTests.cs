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
            var orderInput = "{ apple: 1 }";
            var warehouseInput = @"[{ name: ""owd"", inventory: { apple: 1 } }]";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act
            var result = allocator.Allocate(order, warehouse);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Key, Is.EqualTo("owd"));
            Assert.That(result.First().Value["apple"], Is.EqualTo(1));
        }

        [Test]
        public void When_input_is_null_should_throw_exception()
        {
            //Arrange
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => allocator.Allocate(null, null));
        }

        [Test]
        public void When_inventory_does_not_exist()
        {
            //Arrange
            var orderInput = "{ apple: 1 }";
            var warehouseInput = @"[{ name: ""owd"", inventory: { apple: 0 } }]";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act
            var result = allocator.Allocate(order, warehouse);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void When_item_name_does_not_exist_in_inventory()
        {
            //Arrange
            var orderInput = "{ apple: 1 }";
            var warehouseInput = @"[{ name: ""owd"", inventory: { } }]";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act
            var result = allocator.Allocate(order, warehouse);

            //Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void When_item_is_split_between_warehouses()
        {
            //Arrange
            var orderInput = "{ apple: 10 }";
            var warehouseInput = @"[{ name: ""owd"", inventory: { apple: 5 } }, { name: ""dm"", inventory: { apple: 5 }}]";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act
            var result = allocator.Allocate(order, warehouse);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2), "Needs 2 shipments to complete this order");
            // <comment for reviewers only:> In C# is uncommon to have dynamic properties like this, so normally would not treat a dictionary like this using `Skip.First`, but yeah... too late to change to a more dynamic friendly language at this point :)
            Assert.That(result.First().Key, Is.EqualTo("owd"));
            Assert.That(result.First().Value.Count, Is.EqualTo(1), "from owd, expect one package of apples");
            Assert.That(result.First().Value["apple"], Is.EqualTo(5));
            Assert.That(result.Skip(1).First().Key, Is.EqualTo("dm"));
            Assert.That(result.Skip(1).First().Value.Count, Is.EqualTo(1), "from dm, expect one package of apples");
            Assert.That(result.Skip(1).First().Value["apple"], Is.EqualTo(5));
        }

        [Test]
        public void When_multiple_shipments_optimize_for_fastest_partial_orders()
        {
            //This is the test case illustrating that my greedy algorithm does NOT optimize for shipment count
            //  It might be more optimal to have one shipment of 4 pears from 'two', and one shipment of 4 apples from 'three'
            //  But I'd imagine in real life, the distance and other factors that are not available to me from this data would help decide which is optimal overall

            //Arrange
            var orderInput = "{ apple: 4, pear: 4 }";
            var warehouseInput = @"[{ name: ""one"", inventory: { grape: 50, pear: 2 } },
{ name: ""two"", inventory: { apple: 1, pear: 10 } },
{ name: ""three"", inventory: { apple: 10, pear: 1 } }]";

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act
            var result = allocator.Allocate(order, warehouse);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(3), "Needs 3 shipments to complete this order");

            var shipment1 = result.First();
            var shipment2 = result.Skip(1).First();
            var shipment3 = result.Skip(2).First();


            //TODO remove this debugging (don't have a proper debugger setup on this IDE either):
            System.Console.WriteLine(JsonConvert.SerializeObject(result));
            
            Assert.That(shipment1.Key, Is.EqualTo("one"));
            Assert.That(shipment1.Value.Count, Is.EqualTo(1), "from warehouse 'one', expect only one package of pears");
            Assert.That(shipment1.Value["pear"], Is.EqualTo(2));

            Assert.That(shipment2.Key, Is.EqualTo("two"));
            Assert.That(shipment2.Value.Count, Is.EqualTo(2), "from warehouse 'two', expect two items in the shipment");
            Assert.That(shipment2.Value["apple"], Is.EqualTo(1));
            Assert.That(shipment2.Value["pear"], Is.EqualTo(2));

            Assert.That(shipment3.Key, Is.EqualTo("three"));
            Assert.That(shipment3.Value.Count, Is.EqualTo(1), "from warehouse 'three', only need to complete apples");
            Assert.That(shipment3.Value["apple"], Is.EqualTo(3));
        }

        [Test]
        public void When_warehous_names_are_duplicated_should_throw_exception()
        {
            //Arrange
            var orderInput = "{ apple: 10 }";
            var warehouseInput = @"[{ name: ""mywarehouse"", inventory: { apple: 5 } }, { name: ""mywarehouse"", inventory: { apple: 5 }}]";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => allocator.Allocate(order, warehouse));
        }

        [Test]
        public void When_order_quantity_is_invalid()
        {
            //Arrange
            var orderInput = "{ apple: -1 }";
            var warehouseInput = @"[{ name: ""mywarehouse"", inventory: { apple: 5 } }, { name: ""mywarehouse"", inventory: { apple: 5 }}]";
            // <comment for reviewers only:> structuring it this way so that if I wanted to refactor to use [TestCase(string, string)] for various happy cases, it is easier to do that

            var order = JsonConvert.DeserializeObject<Order>(orderInput);
            var warehouse = JsonConvert.DeserializeObject<WarehouseNetwork>(warehouseInput);

            // <comment for reviewers only:> I am having to do `InventoryAllocator.InventoryAllocator` because its recommended to NOT name a class the same name as a namespace, but with the limited context in this exercise, I can't come up with a better name for either of the concepts (I do not like InventoryAllocatorService)
            var allocator = new InventoryAllocator.InventoryAllocator();

            //Act & Assert
            Assert.Throws<ArgumentException>(() => allocator.Allocate(order, warehouse));
        }
    }
}