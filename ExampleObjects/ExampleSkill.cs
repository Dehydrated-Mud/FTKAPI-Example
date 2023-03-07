using FTKAPI.Objects;
using GridEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Logger = FTKAPI.Utils.Logger;

namespace FTKModLib.Example
{
    public class ExampleSkill : FTKAPI_CharacterSkill
    {
        public ExampleSkill() 
        {
            this.Trigger = TriggerType.EndTurn;
            this.Name = "Mega Refocus";
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            switch (_trig) {
                case TriggerType.EndTurn:
                    Logger.LogWarning("I am the real function!");
                    int focus = _player.m_CharacterStats.MaxFocus - _player.m_CharacterStats.m_FocusPoints;
                    Logger.LogWarning(_player.m_CharacterStats.MaxFocus);
                    Logger.LogWarning(_player.m_CharacterStats.m_FocusPoints);
                    Logger.LogWarning(focus);
                    if (focus > 0)
                    {
                        _player.m_CharacterStats.UpdateFocusPoints(focus);
                        if ((bool)_player.GetCurrentDummy())
                        {
                            _player.GetCurrentDummy().PlayCharacterAbilityEvent(FTK_characterSkill.ID.Refocus);
                        }
                        else
                        {
                            _player.PlayCharacterAbilityEvent(FTK_characterSkill.ID.Refocus);
                        }
                    }
                    break;
            }
        }
    }
}
