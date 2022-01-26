using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;

public class Speech : MonoBehaviour
{
	private Dictionary<string, string> grammar;
	private AudioSource saying;
	private KeywordRecognizer keywordRecognizer;
    
    // Use this for initialization
    void Awake()
	{
		saying = GetComponent<AudioSource>();
		Datas.Grammars g = JsonHelper.Read<Datas.Grammars>("Grammars");
		grammar = g.ToDictionary();
		// instantiate keyword recognizer, pass keyword array in the constructor
		keywordRecognizer = new KeywordRecognizer(g.ToArray());
		keywordRecognizer.OnPhraseRecognized += OnKeywordsRecognized;
		// start keyword recognizer
		keywordRecognizer.Start();
	}
	private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		string str = grammar[args.text];
		if(str=="AddScale")
        {
			ChangeSize(true);
        }
		else if(str=="SubScale")
        {
			ChangeSize(false);
        }
		else if(str.IndexOf("http")!=-1)
		{
			OpenWebsite(args.text);
        }
		else
        {
			Says(args.text);
        }
	}
	private void Says(string text)
    {
		if (grammar[text].IndexOf('|') == -1)
		{
			saying.Stop();
			saying.clip = Resources.Load<AudioClip>(string.Format("Audio\\Keli\\{0}", grammar[text]));
			saying.Play();
		}
		else
		{
			string[] target = grammar[text].Split('|');
			saying.Stop();
			if (RoleManger.emotions.GetStatus() >= 2)
			{
				saying.clip = Resources.Load<AudioClip>("Audio\\Keli\\" + target[0]);
			}
			else
            {
				saying.clip = Resources.Load<AudioClip>("Audio\\Keli\\" + target[1]);
            }
			saying.Play();
		}
	}
	private const float MAX_SIZE = 8.0f;
	private const float MIN_SIZE = 1.0f;
	private void ChangeSize(bool add)
    {
		float x = transform.localScale.x;
		float targetSize = 0f;
		if(add)
        {
			targetSize = x + 1 > MAX_SIZE ? MAX_SIZE : x + 1;
        }
		else
        {
			targetSize = x - 1 < MIN_SIZE ? MIN_SIZE : x - 1;
        }
		transform.localScale = new Vector3(targetSize, targetSize, targetSize);
    }
	private void OpenWebsite(string text)
	{
		string[] ProcessAndUrl = grammar[text].Split('|');
		System.Diagnostics.Process.Start(ProcessAndUrl[0], ProcessAndUrl[1]);
	}
}
