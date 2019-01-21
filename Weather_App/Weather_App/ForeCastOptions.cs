using System.Collections.Generic;

namespace Weather_App
{
    public class ForeCastOptions
    {
        public Dictionary<string,string> options = new Dictionary<string, string>()
        {
            {"Daily","weather" },
            {"5 days forecast","forecast" }
        };
        public Dictionary<string, string> lang = new Dictionary<string, string>()
        {
            {"English","en" },
            { "Arabic","ar"},
            { "Bulgarian","bg"},
            { "Catalan","ca"},
            { "Czech","cz"},
            { "German","de"},
            { "Greek","el"},
            { "Persian (Farsi)","fa"},
            { "Finnish","fi"},
            { "French","fr"},
            { "Galician","gl"},
            { "Croatian","hr"},
            { "Hungarian","hu"},
            { "Italian","it"},
            { "Japanese","ja"},
            { "Korean","kr"},
            { "Latvian","la"},
            { "Lithuanian","lt"},
            { "Macedonian","mk"},
            { "Dutch","nl"},
            { "Polish","pl"},
            { "Portuguese","pt"},
            { "Romanian","ro"},
            { "Russian","ru"},
            { "Swedish","se"},
            { "Slovak","sk"},
            { "Slovenian","sl"},
            { "Spanish","es"},
            { "Turkish","tr"},
            { "Ukrainian","ua"},
            { "Vietnamese","vi"},
            { "Chinese Simplified","zh_cn"},
            { "Chinese Traditional","zh_tw"},
        };
    }
}
