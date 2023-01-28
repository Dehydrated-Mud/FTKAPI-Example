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
            Armor = MakeArmor(FTK_skinset.ID.monk_Male);
            Boot = MakeBoots(FTK_skinset.ID.hunter_Male);
            Helmet = MakeHelmet(FTK_skinset.ID.scholar_Female);
            Backpack = MakeBackpack(FTK_skinset.ID.woodcutter_Male);
        }

    }
}