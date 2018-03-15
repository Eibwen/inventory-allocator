using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAllocator
{
    public class InventoryAllocator
    {
        public OrderFulfillment Allocate(Order order, WarehouseNetwork warehouseNetwork)
        {
            if (order == null) throw new ArgumentNullException("order");
            if (warehouseNetwork == null) throw new ArgumentNullException("warehouseNetwork");

            if (!CanFulfillOrder(order, warehouseNetwork))
            {
                //None of the attempts resulted in a fulfilled order (DesignChoice: return null for this)
                return null;
            }

            // <comment for reviewers only:> the inline out variable is a newer C# feature, and makes the TryX pattern much nicer to read

            //Ideally we can fulfill an order from a single warehouse, if we can, this function will find that.
            //  TODO review this pattern of multiple TryX methods if/when there are any performance concerns (this pattern is the most readable/extendable one I can think of, which was mentioned, where performance was not a target on this exercise),
            //    but it would just be O(W*I*C) where C is a constant of how many ever TryX cases I have, W=warehouses, I=items being ordered
            if (TryFulfillCompleteOrder(order, warehouseNetwork, out var fulfillment))
            {
                return fulfillment;
            }

            return FulfillWithGreedyMultipleShipments(order, warehouseNetwork);
            
            //throw new Exception("Should be impossible to get here, global inventory was checked very first and thats the only valid case that would hit this");
        }

        private bool CanFulfillOrder(Order order, WarehouseNetwork warehouseNetwork)
        {
            //This could be pre-computed, or cached, but the set operations are quite quick for even very large networks
            var globalInventory = warehouseNetwork.SelectMany(x => x.Inventory)
									.GroupBy(x => x.Key)
									.Select(x => new
									{
										Item = x.Key,
										GlobalCount = x.Sum(i => i.Value)
									}).ToDictionary(k => k.Item, v => v.GlobalCount);
            
            foreach (var orderItem in order)
            {
                if (!globalInventory.ContainsKey(orderItem.Key)
                    || globalInventory[orderItem.Key] < orderItem.Value)
                {
                    return false;
                }
            }

            return true;
        }

        private bool TryFulfillCompleteOrder(Order order, WarehouseNetwork warehouseNetwork, out OrderFulfillment orderFulfillment)
        {
            foreach (var warehouse in warehouseNetwork)
            {
                var canFulfill = true;

                foreach (var orderItem in order)
                {
                    if (!warehouse.Inventory.ContainsKey(orderItem.Key)
                        || warehouse.Inventory[orderItem.Key] < orderItem.Value)
                    {
                        canFulfill = false;
                        break;
                    }
                }
                
                if (canFulfill)
                {
                    orderFulfillment = new OrderFulfillment
                    {
                        { warehouse.Name, order }
                    };
                    return true;
                }
            }

            orderFulfillment = null;
            return false;
        }

        ///Use a greedy algorithm to fulfill the order
        private OrderFulfillment FulfillWithGreedyMultipleShipments(Order order, WarehouseNetwork warehouseNetwork)
        {
            var orderFulfillment = new OrderFulfillment();

            // <comment for reviewers only:> This is a semi-hacky way to get around "Collection was modified; enumeration operation may not execute." exceptions.  It is safe here as long as I know this is the only code modifying the collection
            var orderKeys = new List<string>(order.Keys);

            foreach (var warehouse in warehouseNetwork)
            {
                var partialOrder = new Order();

                foreach (var itemName in orderKeys)
                {
                    if (!warehouse.Inventory.ContainsKey(itemName))
                    {
                        continue;
                    }

                    var orderItemQuantity = order[itemName];
                    var warehouseItemCount = warehouse.Inventory[itemName];

                    var amountCanFullfill = Math.Min(orderItemQuantity, warehouseItemCount);

                    if (amountCanFullfill > 0)
                    {
                        partialOrder.Add(itemName, amountCanFullfill);

                        //No need to update warehouse inventory directly here
                        //Do need to update order quantity remaining to fulfill, BUT the KeyValuePair object in C# is immutable... need to update the order dictionary itself
                        order[itemName] -= amountCanFullfill;
                    }
                }

                if (partialOrder.Count > 0)
                {
                    orderFulfillment.Add(warehouse.Name, partialOrder);
                }
            }

            return orderFulfillment;
        }
    }
}