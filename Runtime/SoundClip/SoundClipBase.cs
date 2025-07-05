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
        public abstract SoundSource Play();
        /// <summary>
        /// Sends a play request to the SoundController with custom arguments.
        /// </summary>
        /// <param name="args">Custom arguments.</param>
        /// /// <returns>The SoundSource playing the sound, null if something failed.</returns>
        public abstract SoundSource Play(SoundArgs args);
        /// <summary>
        /// Sends a play request to the SoundController.
        /// </summary>
        /// <param name="volumeMulti">Multiplies the final volume by this factor.</param>
        /// <param name="pitchMulti">Multiplies the final pitch by this factor.</param>
        /// <returns>The SoundSource playing the sound, null if something failed.</returns>
        public abstract SoundSource Play(float volumeMulti = 1f, float pitchMulti = 1f);

        public abstract SoundClip GetSoundClip();
    }
}