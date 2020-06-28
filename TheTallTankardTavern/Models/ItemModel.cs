using Newtonsoft.Json;
using System.ComponentModel;
using System.Text;
using TheTallTankardTavern.Configuration;
using TheTallTankardTavern.Helpers;
using TTT.Common.Abstractions;
using TTT.Enumerator;
using static TheTallTankardTavern.Configuration.Constants;
using static TheTallTankardTavern.Configuration.Constants.WEAPON_TRAIT;

namespace TheTallTankardTavern.Models
{
	public class ItemModel : BaseModel
	{
		public string _WeightClass { get; set; } = "";

		[DisplayName("Description")]
		public string Desc { get; set; }

		[JsonIgnore]
		public string Weapon_Trait_Desc
		{
			get
			{
				if (Traits != null)
				{
					StringBuilder sb = new StringBuilder();
					for (int i = 0; i < Traits.Count; i++)
					{
						if (Traits[i])
						{
							WEAPON_TRAIT trait = (WEAPON_TRAIT)i;
							switch (trait)
							{
								case Defensive: sb.Append($"\n{trait.EnumToString()} ({this.Defensive_Value})"); break;
								case Ranged: sb.Append($"\n{trait.EnumToString()} ({this.Ranged_Value})"); break;
								case Reach: sb.Append($"\n{trait.EnumToString()} ({this.Reach_Value})"); break;
								case Thrown: sb.Append($"\n{trait.EnumToString()} ({this.Thrown_Value})"); break;
								case Versitile: sb.Append($"\n{trait.EnumToString()} ({this.Versitile_Value})"); break;
								default: sb.Append($"\n{trait.EnumToString()}"); break;
							}
						}
					}
					return sb.ToString();
				}
				return "";
			}
		}

        [JsonIgnore]
        [DisplayName("Type")]
        public string Type
        {
            get
            {
                if (this.Item_Type.Equals(ITEM_TYPES.WEAPON))
                {
                    return $"{this.Item_Type} ({this.Size.Substring(0, 1)})";
                }
                return this.Item_Type;
            }
            set
            {
                Item_Type = value;
            }
        }

		[DisplayName("Item Type")]
		public string Item_Type { get; set; } = ITEM_TYPES.WEAPON;

		[DisplayName("Material")]
		public string Material_ID { get; set; }

		[DisplayName("Damage Die")]
		public string Damage_Die { get; set; } = DIE.NONE.EnumToString();

		[DisplayName("Units to Craft")]
		public int Units_To_Craft { get; set; } = 1;

		[DisplayName("Traits")]
		public CheckBoxListModel<WEAPON_TRAIT> Traits { get; set; }

		#region Trait Values
		public int Defensive_Value { get; set; } = 0;
		public string Ranged_Value { get; set; } = "";
		public string Reach_Value { get; set; } = "";
		public string Thrown_Value { get; set; } = "";
		public string Versitile_Value { get; set; } = "";
		#endregion

        [DisplayName("Damage Types")]
		public CheckBoxListModel<DAMAGE_TYPE> Damage_Types { get; set; }

		[JsonIgnore]
		public string Size
		{
			get
			{
				return (Traits[Large] ? WEAPON_SIZE.LARGE : (Traits[Small] ? WEAPON_SIZE.SMALL : WEAPON_SIZE.MEDIUM));
			}
		}

		[JsonIgnore]
		private MaterialModel Material
		{
			get
			{
				return ApplicationSettings.MaterialDataContext.GetModelFromID(Material_ID) ??
					ApplicationSettings.MaterialDataContext.GetModelFromName("None");
			}
		}

		public string Material_Name => Material.Name;

		[JsonIgnore]
		public int Attack_Value { get { return (Item_Type.Equals(ITEM_TYPES.WEAPON)) ? Material.Attack : 0; } }

		[JsonIgnore]
		public int Damage_Value { get { return (Item_Type.Equals(ITEM_TYPES.WEAPON)) ? Material.Damage : 0; } }

		[JsonIgnore]
		public int Armour_Class_Value { get { return (Item_Type.Equals(ITEM_TYPES.ARMOUR)) ? Material.Armour_Class : 0; } }

		[JsonIgnore]
		[DisplayName("Attack")]
		public string Attack { get { return (Item_Type.Equals(ITEM_TYPES.WEAPON)) ? Material.Attack.ToString() : ""; } }

		[JsonIgnore]
		[DisplayName("Damage")]
		public string Damage { get { return (Item_Type.Equals(ITEM_TYPES.WEAPON)) ? $"{this.Damage_Die}+{Material.Damage}" : ""; } }

		[JsonIgnore]
		[DisplayName("Armour Class")]
		public string Armour_Class { get { return (Item_Type.Equals(ITEM_TYPES.ARMOUR)) ? Material.Armour_Class.ToString() : ""; } }

		[JsonIgnore]
		[DisplayName("Enchantment Slots")]
		public int Enchantment_Slots { get { return (Item_Type.Equals(ITEM_TYPES.MISC)) ? Material.Enchantment_Slots : 0; } }

		[JsonIgnore]
		[DisplayName("Weight Class")]
		public string Weight_Class
		{
			get
			{
				return _WeightClass;
			}
			set
			{
				switch (Item_Type)
				{
					case ITEM_TYPES.WEAPON: _WeightClass = value; break;
					case ITEM_TYPES.ARMOUR: _WeightClass = Material.Weight_Class; break;
					default: _WeightClass = ""; break;
				}
			}
		}
			

		[JsonIgnore]
		[DisplayName("Value")]
		public int Value => Units_To_Craft * Material.Cost_Per_Unit;
	}
}
