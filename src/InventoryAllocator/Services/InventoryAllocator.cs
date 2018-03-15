using System;
using System.Collections.Generic;

namespace InventoryAllocator
{
    public class InventoryAllocator
    {
        public OrderFulfillment Allocate(Order order, WarehouseNetwork warehouseNetwork)
        {
            if (order == null) throw new ArgumentNullException("order");
            if (warehouseNetwork == null) throw new ArgumentNullException("warehouseNetwork");

            // <comment for reviewers only:> the inline out variable is a newer C# feature, and makes the TryX pattern much nicer to read

            //Ideally we can fulfill an order from a single warehouse, if we can, this function will find that.
            //  TODO review this pattern of multiple TryX methods if/when there are any performance concerns (this pattern is the most readable/extendable one I can think of, which was mentioned, where performance was not a target on this exercise),
            //    but it would just be O(W*I*C) where C is a constant of how many ever TryX cases I have, W=warehouses, I=items being ordered
            if (TryFulfillCompleteOrder(order, warehouseNetwork, out var fulfillment))
            {
                return fulfillment;
            }
            
            throw new NotImplementedException();
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
    }
}