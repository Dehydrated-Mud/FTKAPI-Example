﻿using FTKAPI.Objects;
using GridEditor;
using HarmonyLib;
using SimpleBindDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Logger = FTKAPI.Utils.Logger;

namespace FTKModLib.Example
{
    public class FocusHealer : FTKAPI_CharacterSkill
    {
        public FocusHealer() 
        {
            this.Trigger = TriggerType.AnyLandedAttack | TriggerType.EndTurn;
            this.Name = "Mega Refocus";
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig, AttackAttempt _atk)
        {
            Logger.LogWarning("Attempting combat focus heal");
            switch (_trig)
            {
                case TriggerType.AnyLandedAttack:
                    if (_atk.m_AttackFocused > 0)
                    {
                        int _newHealth = 0;
                        List<CharacterDummy> CombatPlayerMembers = EncounterSession.Instance.GetOtherCombatPlayerMembers(_player.m_CurrentDummy);
                        CombatPlayerMembers.AddItem(_player.m_CurrentDummy);
                        foreach(CharacterDummy characterDummy in CombatPlayerMembers) 
                        {
                            _newHealth = GetNewHealth(characterDummy, 0.1f);
                            characterDummy.m_CharacterOverworld.m_CharacterStats.SetSpecificHealth(_newHealth, false);
                            characterDummy.SpawnHudTextRPC("Focus Healing +" + (_newHealth - characterDummy.GetCurrentHealth()) + "HP", string.Empty);
                            characterDummy.PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.None);
                        }
                    }
                    break;
            }
        }
        public override void Skill(CharacterOverworld _player, TriggerType _trig)
        {
            Logger.LogWarning("Attempting end turn focus heal");
            float _conv = 2.89f;
            switch (_trig) 
            {
                case TriggerType.EndTurn:
                    if(_player.m_CharacterStats.SpentFocus > 0 && !_player.m_CharacterStats.m_IsInCombat && !_player.IsInDungeon())
                    {
                        foreach (CharacterOverworld characterOverworld in FTKHub.Instance.m_CharacterOverworlds)
                        {
                            float magnitude = (_player.transform.position - characterOverworld.transform.position).magnitude;
                            Logger.LogWarning("Distance to target is: " + magnitude);
                            if (magnitude <= 2f * _conv)
                            {
                                int _newHealth = GetNewHealth(characterOverworld.m_CurrentDummy, 0.2f);
                                if (_newHealth - characterOverworld.m_CurrentDummy.GetCurrentHealth() > 0)
                                {
                                    characterOverworld.SpawnHudTextRPC("Focus Healed +" + (_newHealth - characterOverworld.m_CurrentDummy.GetCurrentHealth()) + "HP", string.Empty);
                                    characterOverworld.m_CharacterStats.SetSpecificHealthRPC(_newHealth);
                                    characterOverworld.PlayCharacterAbilityEventRPC(FTK_characterSkill.ID.None);
                                }
                            }
                        }
                    }
                    break;
            }
        }
        internal int GetNewHealth(CharacterDummy _dum, float _fac)
        {
            return Math.Min((int)((float)_dum.GetCurrentHealth() + (float)_dum.m_CharacterOverworld.m_CharacterStats.MaxHealth * _fac), _dum.m_CharacterOverworld.m_CharacterStats.MaxHealth);
        }
    }
}
