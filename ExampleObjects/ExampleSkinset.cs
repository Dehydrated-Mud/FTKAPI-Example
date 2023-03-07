using FTKAPI.Objects;
using GridEditor;
using FTKAPI.Managers;
using System.Reflection;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;
using FullInspector;
using FTKAPI.Utils;

namespace FTKModLib.Example
{
    public class ExampleSkinset : CustomSkinset
    {
        public ExampleSkinset()
        {
            ID = "leprechaun_Male";
            // All skinset items below can be set via a custom prefab, or with a skinset ID like:
            // Avatar = MakeAvatar(FTK_skinset.ID.blacksmith_Male); // This will set the male blacksmith as the avatar for this skinset (the avatar will be shared between the two classes)
            
            Avatar = MakeAvatar(ExampleMod.assetBundleSkins.LoadAsset<GameObject>("Assets/player_Test.prefab"));
            Armor = MakeArmor(ExampleMod.assetBundleSkins.LoadAsset<GameObject>("Assets/armorPaladin1.prefab"));
            Boot = MakeBoots(ExampleMod.assetBundleSkins.LoadAsset<GameObject>("Assets/bootsPaladin1.prefab"));
            Helmet = MakeHelmet(ExampleMod.assetBundleSkins.LoadAsset<GameObject>("Assets/helmLeprechaun.prefab"));
            Backpack = MakeBackpack(ExampleMod.assetBundleSkins.LoadAsset<GameObject>("Assets/backpackLeprechaun.prefab"));
        }
    }
}