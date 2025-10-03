using UnityEngine;

namespace HexTecGames.SoundSystem
{
    public abstract class SoundClipBase : ScriptableObject
    {
        /// <summary>
        /// The same as Play() but without returning the SoundSource. Needed for Unity Events.
        /// </summary>
        public void PlaySound()
        {
            Play();
        }
        /// <summary>
        /// Sends a play request to the SoundController.
        /// </summary>
        /// /// <returns>The SoundSource playing the sound, null if something failed.</returns>
        public virtual SoundSource Play()
        {
            return Play(GetSoundArgs());
        }
        /// <summary>
        /// Sends a play request to the SoundController with custom arguments.
        /// </summary>
        /// <param name="args">Custom arguments.</param>
        /// /// <returns>The SoundSource playing the sound, null if something failed.</returns>
        public SoundSource Play(SoundArgs args)
        {
            SoundController.RequestTempSound(args);
            return args.source;
        }
        /// <summary>
        /// Sends a play request to the SoundController.
        /// </summary>
        /// <param name="volumeMulti">Multiplies the final volume by this factor.</param>
        /// <param name="pitchMulti">Multiplies the final pitch by this factor.</param>
        /// <returns>The SoundSource playing the sound, null if something failed.</returns>
        public SoundSource Play(float volumeMulti = 1f, float pitchMulti = 1f)
        {
            return Play(GetSoundArgs(volumeMulti, pitchMulti));
        }

        public abstract SoundArgs GetSoundArgs();
        public SoundArgs GetSoundArgs(float volumeMulti, float pitchMulti)
        {
            SoundArgs soundArgs = GetSoundArgs();
            soundArgs.volumeMulti = volumeMulti;
            soundArgs.pitchMulti = pitchMulti;
            return soundArgs;
        }
    }
}