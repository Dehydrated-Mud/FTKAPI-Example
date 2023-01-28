using FTKAPI.Objects;
using GridEditor;
using FTKAPI.Managers;
using FTKAPI.Utils;
using UnityEngine;

namespace FTKModLib.Example
{
    public class ExampleAvatar : CustomAvatar
    {
        public ExampleAvatar()
        {
            Prefab = ExampleMod.assetBundle.LoadAsset<GameObject>("Assets/CustomItems/player_lep.prefab");
            //Mesh = Resources.Load<Mesh>("Assets/CustomAvatars/player_lep.obj");
        }
    }
}