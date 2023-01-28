using FTKAPI.Objects;
using HutongGames.PlayMaker.Actions;

namespace FTKModLib.Example {
    public class ProficiencySilverSmite : CustomProficiency {
        public ProficiencySilverSmite() {
            ID = "silversmite";
            Name = new("Silver Burn");
            SlotOverride = 4;
            IgnoresArmor = true;
            DmgTypeOverride = GridEditor.FTK_weaponStats2.DamageType.magic;
            ProficiencyPrefab = null;
            Category = Category.None;
            FullSlots = false;
            IsEndOnTurn = true;
            PerSlotSkillRoll = 0f;
            DmgMultiplier= 1f;
            Tint = new UnityEngine.Color(.7f, .7f, .8f, 1f);
        }
    }
}
