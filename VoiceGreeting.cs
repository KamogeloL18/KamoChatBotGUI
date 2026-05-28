using System.Media;

namespace KamoChatBotGUI
{
    public class VoiceGreeting
    {
        public static void AudioHelper(string filepath)
        {
            SoundPlayer play = new SoundPlayer();
            play.SoundLocation = filepath;
            play.Play();
        }
    }
}
