using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;
using EKR.Editor;

namespace EKR.Core
{
    public class VoiceReader
    {
        private string _inputVoiceText = "";
        private List<string> _inputVoiceTextHistory = new List<string>();
        public List<string> InputVoiceTextHistory
        {
            get => _inputVoiceTextHistory;
        }
        
        private DataTable _keywordSetAll = new DataTable("Spel Word Table");
        private readonly DataRow[] _keywordSetHead;
        private DataRow[] _keywordSetNext;

        // ScriptableObjectの読み込み
        public VoiceReader()
        {
            _keywordSetAll.Columns.Add("SpellWordID");
            _keywordSetAll.Columns.Add("SpellWord");
            _keywordSetAll.Columns.Add("SpellWordKana");
            _keywordSetAll.Columns.Add("FunctionName");
            _keywordSetAll.Columns.Add("isHead");
            _keywordSetAll.Columns.Add("isEnd");
            _keywordSetAll.Columns.Add("NextWordId");

            var spellWord = Resources.Load<SpellWordObject>("SpellWord");
            foreach (var val in spellWord.SpellWordList)
            {
                _keywordSetAll.Rows.Add(val.spellWordID, 
                    val.spellWord,
                    val.spellWordKana,
                    val.functionName,
                    val.isHead,
                    val.isEnd,
                    val.nextWordId);
            }
            _keywordSetHead = _keywordSetAll.Select("isHead = 'True'");
            InitNextKeywordList();
        }

        // 入力呪文受け取り
        public SpellWord InputVoiceText(string inputVoiceText, Boolean returnViewWord=false)
        {
            if (string.IsNullOrEmpty(inputVoiceText))
                return null;
            _inputVoiceText = inputVoiceText;
            InputVoiceHistory(inputVoiceText);
            var resultSpellWord = InputVoiceTextCheck();
            
            return resultSpellWord;
        }

        // 呪文詠唱チェック
        private SpellWord InputVoiceTextCheck()
        {
            foreach (var checkWord in _keywordSetNext)
            {
                if (_inputVoiceText == (string) checkWord["SpellWordKana"])
                {
                    // 戻り値作成
                    SpellWord result = new SpellWord()
                    {
                        spellWordID = (int) checkWord["SpellWordID"],
                        spellWord = (string) checkWord["SpellWord"],
                        spellWordKana = (string) checkWord["SpellWordKana"],
                        functionName = (string) checkWord["FunctionName"],
                        isHead = (bool) checkWord["isHead"],
                        isEnd = (bool) checkWord["isEnd"],
                        nextWordId = (int) checkWord["NextWordId"]
                    };

                    // 呪文終了処理
                    if ((string) checkWord["isEnd"] == "True")
                    {
                        SuccessSpell();
                    }
                    else
                    {
                        CreateNextKeywordList();
                    }
                    return result;
                }
            }
            
            // 詠唱失敗処理
            MisstakeSpell();
            return null;
        }

        // 入力呪文履歴保存
        private void InputVoiceHistory(string newInputText)
        {
            _inputVoiceTextHistory.Add(newInputText);
        }

        // 呪文詠唱成功時処理
        private void SuccessSpell()
        {
            DeleteHistory();
        }

        // 呪文ミス時処理
        private void MisstakeSpell()
        {
            DeleteHistory();
        }

        // 履歴削除
        public void DeleteHistory()
        {
            _inputVoiceTextHistory = null;
            _inputVoiceTextHistory = new List<string>();
            InitNextKeywordList();
        }

        // 次のキーワードリストを初期化
        private void InitNextKeywordList()
        {
            _keywordSetNext = _keywordSetHead;
        }

        // 次のキーワードリストの作成
        private void CreateNextKeywordList()
        {
            List<string> idList = new List<string>();
            foreach (var val in _keywordSetNext)
            {
                if ((string) val["SpellWord"] == _inputVoiceText)
                {
                    Debug.Log(val["NextWordId"].ToString());
                    idList.Add(val["NextWordId"].ToString());
                }
            }

            _keywordSetNext = _keywordSetAll.Select(
                $"SpellWordID IN ({String.Join(",", idList)})");
        }
    }
}

