using System;
using System.Collections.Generic;
using System.Linq;
using common;
using common.resources;
using log4net;
using wServer.realm.terrain;

namespace wServer.realm.entities.vendors
{
    public class ShopItem : ISellableItem
    {
        public ushort ItemId { get; private set; }
        public int Price { get; }
        public int Count { get; }
        public string Name { get; }

        public ShopItem(string name, ushort price, int count = -1)
        {
            ItemId = ushort.MaxValue;
            Price = price;
            Count = count;
            Name = name;
        }

        public void SetItem(ushort item)
        {
            if (ItemId != ushort.MaxValue)
                throw new AccessViolationException("Can't change item after it has been set.");

            ItemId = item;
        }
    }
    
    internal static class MerchantLists
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MerchantLists));

        private static readonly List<ISellableItem> Weapons = new List<ISellableItem>
        {
            new ShopItem("Dagger of Foul Malevolence", 1000),
            new ShopItem("Bow of Covert Havens", 1000),
            new ShopItem("Staff of the Cosmic Whole", 1000),
            new ShopItem("Wand of Recompense", 900), 
            new ShopItem("Sword of Acclaim", 1250),
            new ShopItem("Masamune", 950) 
        };

        private static readonly List<ISellableItem> Abilities = new List<ISellableItem>
        {
            new ShopItem("Cloak of Ghostly Concealment", 700),
            new ShopItem("Quiver of Elvish Mastery", 700),  
            new ShopItem("Elemental Detonation Spell", 700),
            new ShopItem("Tome of Holy Guidance", 700),
            new ShopItem("Helm of the Great General", 700),
            new ShopItem("Colossus Shield", 700), 
            new ShopItem("Seal of the Blessed Champion", 700),
            new ShopItem("Baneserpent Poison", 700),
            new ShopItem("Bloodsucker Skull", 700),
            new ShopItem("Giantcatcher Trap", 700),
            new ShopItem("Planefetter Orb", 700),
            new ShopItem("Prism of Apparitions", 700),
            new ShopItem("Scepter of Storms", 700),
            new ShopItem("Doom Circle", 700)
        };

        private static readonly List<ISellableItem> Armor = new List<ISellableItem>
        {
            new ShopItem("Robe of the Grand Sorcerer", 850),
            new ShopItem("Hydra Skin Armor", 850),
            new ShopItem("Acropolis Armor", 850)
        };

        private static readonly List<ISellableItem> Rings = new List<ISellableItem>
        {
            new ShopItem("Ring of Exalted Attack", 250),
            new ShopItem("Ring of Exalted Defense", 350),
            new ShopItem("Ring of Exalted Speed", 200),
            new ShopItem("Ring of Exalted Dexterity", 250),
            new ShopItem("Ring of Exalted Vitality", 100),
            new ShopItem("Ring of Exalted Wisdom", 100),
            new ShopItem("Ring of Exalted Health", 500),
            new ShopItem("Ring of Exalted Magic", 200),
            new ShopItem("Ring of Unbound Attack", 750),
            new ShopItem("Ring of Unbound Defense", 850),
            new ShopItem("Ring of Unbound Speed", 700),
            new ShopItem("Ring of Unbound Dexterity", 750),
            new ShopItem("Ring of Unbound Vitality", 400),
            new ShopItem("Ring of Unbound Wisdom", 400),
            new ShopItem("Ring of Unbound Health", 800),
            new ShopItem("Ring of Unbound Magic", 700)
        };

        private static readonly List<ISellableItem> Keys = new List<ISellableItem>
        {
            new ShopItem("Undead Lair Key", 100),
            new ShopItem("Sprite World Key", 100),
            new ShopItem("Davy's Key", 150),
            new ShopItem("The Crawling Depths Key", 200),
            new ShopItem("Candy Key", 250),
            new ShopItem("Abyss of Demons Key", 40),
            new ShopItem("Totem Key", 50),
            new ShopItem("Pirate Cave Key", 25),
            new ShopItem("Shatters Key", 200),
            new ShopItem("Beachzone Key", 150),
            new ShopItem("Ivory Wyvern Key", 150),
            new ShopItem("Lab Key", 100),
            new ShopItem("Manor Key", 100),
            new ShopItem("Cemetery Key", 100),
            new ShopItem("Ocean Trench Key", 200),
            new ShopItem("Snake Pit Key", 30),
            new ShopItem("Bella's Key", 150),
            new ShopItem("Shaitan's Key", 200),
            new ShopItem("Tomb of the Ancients Key", 200),
            new ShopItem("Battle Nexus Key", 150),
            new ShopItem("Deadwater Docks Key", 150),
            new ShopItem("Woodland Labyrinth Key", 200),
            new ShopItem("Theatre Key", 80),
            new ShopItem("Ice Cave Key", 150)
        };

        private static readonly List<ISellableItem> PurchasableFame = new List<ISellableItem>
        {
            new ShopItem("50 Fame", 50),
            new ShopItem("100 Fame", 100),
            new ShopItem("500 Fame", 500),
            new ShopItem("1000 Fame", 1000),
            new ShopItem("5000 Fame", 5000)
        };

        private static readonly List<ISellableItem> Consumables = new List<ISellableItem>
        {
            new ShopItem("XP Booster", 35),
            new ShopItem("Backpack", 300),
            new ShopItem("Maxy Potion", 7500)
        };

        private static readonly List<ISellableItem> Special = new List<ISellableItem>
        {
            new ShopItem("Amulet of Resurrection", 50000) 
        };
        
        public static readonly Dictionary<TileRegion, Tuple<List<ISellableItem>, CurrencyType, /*Rank Req*/int>> Shops = 
            new Dictionary<TileRegion, Tuple<List<ISellableItem>, CurrencyType, int>>()
        {
            { TileRegion.Store_1, new Tuple<List<ISellableItem>, CurrencyType, int>(Weapons, CurrencyType.Fame, 0) },
            { TileRegion.Store_2, new Tuple<List<ISellableItem>, CurrencyType, int>(Abilities, CurrencyType.Fame, 0) },
            { TileRegion.Store_3, new Tuple<List<ISellableItem>, CurrencyType, int>(Armor, CurrencyType.Fame, 0) },
            { TileRegion.Store_4, new Tuple<List<ISellableItem>, CurrencyType, int>(Rings, CurrencyType.Fame, 0) },
            { TileRegion.Store_5, new Tuple<List<ISellableItem>, CurrencyType, int>(Keys, CurrencyType.Fame, 0) },
            { TileRegion.Store_6, new Tuple<List<ISellableItem>, CurrencyType, int>(PurchasableFame, CurrencyType.Fame, 5) },
            { TileRegion.Store_7, new Tuple<List<ISellableItem>, CurrencyType, int>(Consumables, CurrencyType.Fame, 0) },
            { TileRegion.Store_8, new Tuple<List<ISellableItem>, CurrencyType, int>(Special, CurrencyType.Fame, 0) },
        };
        
        public static void Init(RealmManager manager)
        {
            foreach (var shop in Shops)
                foreach (var shopItem in shop.Value.Item1.OfType<ShopItem>())
                {
                    ushort id;
                    if (!manager.Resources.GameData.IdToObjectType.TryGetValue(shopItem.Name, out id))
                        Log.WarnFormat("Item name: {0}, not found.", shopItem.Name);
                    else
                        shopItem.SetItem(id);
                }
        }
    }
}