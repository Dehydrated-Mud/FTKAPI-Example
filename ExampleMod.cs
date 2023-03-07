using BepInEx;
using FTKAPI.Managers;
using FTKAPI.Objects;
using GridEditor;
using HarmonyLib;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Logger = FTKAPI.Utils.Logger;

namespace FTKModLib.Example {
    [BepInPlugin("FTKAPIExample", "FTKAPIExample", "0.1.0")]
    [BepInDependency("FTKAPI")]
    //[BepInProcess("FTK.exe")]
    public class ExampleMod : BaseUnityPlugin {
        public static AssetBundle assetBundle;
        public static AssetBundle assetBundleSkins;
        public static BaseUnityPlugin Instance;

        private void Awake() {
            Instance = this;
            Logger.LogInfo($"Plugin {Info.Metadata.GUID} is loaded!");

            // When adding another asset bundle, be sure to edit FTKModLib.Example and add the ItemGroup flags for your bundle
            assetBundle = AssetManager.LoadAssetBundleFromResources("customitemsbundle", Assembly.GetExecutingAssembly());
            assetBundleSkins = AssetManager.LoadAssetBundleFromResources("customskinsbundle", Assembly.GetExecutingAssembly());
            Harmony harmony = new Harmony(Info.Metadata.GUID);
            harmony.PatchAll();
        }

        class HarmonyPatches {
            /// <summary>
            /// Most calls to managers will most likely require to be called after TableManager.Initialize.
            /// So just make all your changes in this postfix patch unless you know what you're doing.
            /// </summary>
            [HarmonyPatch(typeof(TableManager), "Initialize")]
            class TableManager_Initialize_Patch {
                static void Postfix() {
                    FTKAPI_CharacterSkill skill = new FocusHealer();
                    FTKAPI_CharacterSkill[] skills = { skill };
                    FTKAPI.Utils.Logger.LogWarning(skill);
                    FTKAPI.Utils.Logger.LogWarning(skills[0]?.Name);
                    FTK_playerGameStartDB playerGameStartDB = TableManager.Instance.Get<FTK_playerGameStartDB>();
                    FTK_playerGameStart baseSkills = playerGameStartDB.m_Array[(int)FTK_playerGameStart.ID.busker];
                    FTKAPI.Utils.Logger.LogWarning(baseSkills is null);
                    FTKAPI.Utils.Logger.LogWarning("Attempting to Make CustomCharacterSkills Object");
                    CustomCharacterSkills customCharacterSkills = new CustomCharacterSkills(baseSkills.m_CharacterSkills)
                    {
                        Skills = skills
                    };
                    FTKAPI.Utils.Logger.LogWarning("Made it!");
                    int customSkinset = SkinsetManager.AddSkinset(new ExampleSkinset(), Instance);
                    FTK_skinset.ID[] customSkinsets = new FTK_skinset.ID[] { (FTK_skinset.ID)customSkinset };
                    int customHerb = ItemManager.AddItem(new ExampleHerb(), Instance);

                    int bladeSilver = ItemManager.AddItem(new BladeSilver(), Instance);
                    int hammerLightning = ItemManager.AddItem(new HammerLightning(), Instance);
                    ItemManager.ModifyItem(
                        FTK_itembase.ID.herbGodsbeard1, 
                        new CustomItem(FTK_itembase.ID.herbGodsbeard1) {
                            ShopStock = 420
                        }
                    );

                    //SkinsetManager.ModifySkinset(FTK_skinset.ID.busker_Male, new ExampleSkinset());
                    ClassManager.AddClass(new ExampleClass() { Skinsets =  customSkinsets}, Instance) ; //Adds our leprechaun class
                    FTKAPI.Utils.Logger.LogWarning("Attempting to Modify Blacksmith with CustomCharacterSkills Object");
                    ClassManager.ModifyClass(//Modifies the blacksmith to give them our new weapons and custom herb
                        FTK_playerGameStart.ID.blacksmith,
                        new CustomClass(FTK_playerGameStart.ID.blacksmith) {
                            StartWeapon = (FTK_itembase.ID)hammerLightning,
                            CharacterSkills = customCharacterSkills
                        }
                        .AddToStartItems(new FTK_itembase.ID[] {
                            (FTK_itembase.ID)customHerb
                        })
                    );
                    FTKAPI.Utils.Logger.LogWarning("Did it!");
                    ClassManager.ModifyClass(//Modifies the hunter
                        FTK_playerGameStart.ID.hunter,
                        new CustomClass(FTK_playerGameStart.ID.hunter)
                        {
                            StartWeapon = (FTK_itembase.ID)bladeSilver
                        }
                    ); 

                    ClassManager.ModifyClass(//Modifies the hobo
                        FTK_playerGameStart.ID.hobo,
                        new CustomClass(FTK_playerGameStart.ID.hobo)
                        {
                            StartWeapon = (FTK_itembase.ID)bladeSilver
                        }
                    );
                }
            }
        }
    }
}
