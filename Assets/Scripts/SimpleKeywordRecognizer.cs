using System;
using System.Data;
using System.Text;
using EKR.Editor;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace EKR.Core.Engine
{
    public class SimpleKeywordRecognizer:MonoBehaviour
    {
        [SerializeField]private string[] keywords;
        private KeywordRecognizer _recognizer;
        private VoiceReader _voiceReader;

        void Awake()
        {
            _recognizer = new KeywordRecognizer(keywords);
            _voiceReader = new VoiceReader();
        }

        private void OnEnable()
        {
            _recognizer.OnPhraseRecognized += OnPhraseRecognized;
            _recognizer.Start();
        }

        private void OnDisable()
        {
            _recognizer.OnPhraseRecognized -= OnPhraseRecognized;
            _voiceReader.DeleteHistory();
            _recognizer.Stop();
        }

        private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            Debug.Log(args.text + $", LEVEL:{args.confidence}");
            SpellWord result = null;
            if(args.confidence.ToString() == "High")
                result = _voiceReader.InputVoiceText(args.text);

            if (result != null)
                Debug.Log($"ID:{result.spellWordID}, Word:{result.spellWord}, Function:{result.functionName}");
        }
    }
}
