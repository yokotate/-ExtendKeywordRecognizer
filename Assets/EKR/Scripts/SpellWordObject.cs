using System;
using System.Collections.Generic;
using UnityEngine;

namespace EKR.Editor
{
    [CreateAssetMenu(fileName = "SpellWord", menuName = "ScriptableObject/SpellWord", order = 0)]
    public class SpellWordObject : ScriptableObject
    {
        public List<SpellWord> SpellWordList = new List<SpellWord>();

        public SpellWord FindWithSpellWordID(int spellWordID)
        {
            foreach (var val in SpellWordList)
            {
                if (val.spellWordID == spellWordID)
                    return val;
            }
            return null;
        }
    }
    
    [Serializable]
    public class SpellWord
    {
        public int spellWordID;
        public string spellWord;
        public string spellWordKana;
        public string functionName;
        public bool isHead;
        public bool isEnd;
        public int nextWordId;
    }
    
}
