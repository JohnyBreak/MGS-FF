using InventorySystem;

namespace DoorsSystem
{
    public class DoorCard : IItem
    {
        public readonly int Lvl;

        public DoorCard(int lvl)
        {
            Lvl = lvl;
        }

        public int GetKey()
        {
            return ItemKeys.DoorCard;
        }
    }
}