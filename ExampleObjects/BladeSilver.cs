using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using UnityEngine;


namespace FTKModLib.Example {
    using ProficiencyManager = FTKAPI.Managers.ProficiencyManager;
    public class BladeSilver : CustomItem {
        public BladeSilver() {
            int customProf = ProficiencyManager.AddProficiency(new ProficiencySilverSmite());

            ID = "CustomBladeSilver";
            Name = new("Silver Longsword");
            Prefab = ExampleMod.assetBundle.LoadAsset<GameObject>("Assets/customBladeSilver.prefab");
            ObjectSlot = FTK_itembase.ObjectSlot.twoHands;
            ObjectType = FTK_itembase.ObjectType.weapon; // This is required for the item to be registered as a weapon
            SkillType = FTK_weaponStats2.SkillType.awareness;
            WeaponType = Weapon.WeaponType.bladed;
            ProficiencyEffects = new() { // these are the weapon attacks/skills for this custom item
                [(FTK_proficiencyTable.ID)customProf] = FTK_hitEffect.ID.bladeHeavyCrit,
                [FTK_proficiencyTable.ID.bladeDamage] = FTK_hitEffect.ID.defaultBlade,
                [FTK_proficiencyTable.ID.fire1] = FTK_hitEffect.ID.defaultBlade,
                [FTK_proficiencyTable.ID.firestorm1] = FTK_hitEffect.ID.defaultBlade,
            };
            AnimationController = AssetManager.GetAnimationControllers<Weapon>().Find(i => i.name == "player_2H_Blunt_Combat");
            Slots = 3;
            MaxDmg = 30;
            DmgType = FTK_weaponStats2.DamageType.magic;
            ShopStock = 1;
            TownMarket = true;
            DungeonMerchant= true;
            ItemRarity = FTK_itemRarityLevel.ID.rare;
            NoRegularAttack = false;
            //WeaponSize = (FTK_ragdollDeath.ID)3; //Causes game to not start
        }
    }
}
