using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace HexTecGames.SoundSystem
{
    [CustomEditor(typeof(SoundClipBase), true), CanEditMultipleObjects]
    public class SoundClipBaseEditor : Editor
    {
        private Button playButton;
        private bool isLooping;
        [SerializeField] private SoundSource soundSourcePrefab;
        private List<SoundSource> soundSources = new List<SoundSource>();
        private SoundSource lastSource;


        private void OnDisable()
        {
            for (int i = soundSources.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(soundSources[i].gameObject);
            }
            soundSources.Clear();
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            playButton = new Button();
            playButton.RegisterCallback<ClickEvent>(PlayButtonClicked);
            UpdateButtonText();
            IStyle style = playButton.style;
            style.height = 30;
            root.Add(playButton);
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            return root;
        }
        private void ApplySoundClip(SoundSource source)
        {
            SoundClipBase soundClipBase = target as SoundClipBase;
            SoundClip clip = soundClipBase.GetSoundClip();
            if (clip == null)
            {
                Debug.Log("No SoundClip available!");
                return;
            }
            if (clip.AudioClip == null)
            {
                Debug.Log("No AudioClip assigned!");
                return;
            }
            isLooping = clip.Loop;
            lastSource = source;
            SoundArgs args = new SoundArgs(clip);
            source.Play(args);
            Debug.Log($"Playing {args.audioClip.name}");
        }

        private SoundSource GetSoundSource()
        {
            if (soundSources.Any(x => !x.gameObject.activeSelf))
            {
                SoundSource source = soundSources.Find(x => !x.gameObject.activeSelf);
                source.gameObject.SetActive(true);
                return source;
            }
            else return SpawnSoundSource();
        }
        private SoundSource SpawnSoundSource()
        {
            SoundSource clone = Instantiate(soundSourcePrefab);
            clone.gameObject.hideFlags = HideFlags.HideAndDontSave;
            clone.gameObject.SetActive(true);
            soundSources.Add(clone);
            return clone;
        }
        private void PlayButtonClicked(ClickEvent evt)
        {
            if (isLooping)
            {
                lastSource.Stop();
                isLooping = false;
                UpdateButtonText();
                return;
            }
            SoundSource audioSource = GetSoundSource();
            ApplySoundClip(audioSource);
            UpdateButtonText();
        }
        private void UpdateButtonText()
        {
            playButton.text = isLooping ? "Stop" : "Play";
        }
    }
}