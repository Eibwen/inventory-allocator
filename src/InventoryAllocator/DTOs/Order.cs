using System.Collections.Generic;

namespace InventoryAllocator
{
    ///.Net isn't great for such dynamic objects, so just having his inherit from Dictionary to allow for the given object structure
    public class Order : Dictionary<string, int>
    {

    }
}