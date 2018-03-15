using System.Collections.Generic;

namespace InventoryAllocator
{
    public class WarehouseNetwork : List<Warehouse>
    {
    }

    public class Warehouse
    {
        public string Name { get; set; }
        public Inventory Inventory { get; set; }
    }

    // For anything but the most basic sub-objects I would tend to NOT put them in the same file
    public class Inventory : Dictionary<string, int>
    {
    }
}