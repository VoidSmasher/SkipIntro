using PhoenixPoint.Modding;

namespace SkipIntro
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// </summary>
    public class SkipIntroConfig : ModConfig
    {
        /// Only public fields are serialized.
        [ConfigField(text: "Skip Intro Logos",
        description: "Skips logos when loading up the game")]
        public bool SkipIntroLogos = true;

        [ConfigField(text: "Skip Intro Movie",
        description: "Skips intro movie")]
        public bool SkipIntroMovie = true;

        [ConfigField(text: "Skip Landing Sequences",
        description: "Skips landing sequences before tactical missions")]
        public bool SkipLandingSequences = false;
    }
}
