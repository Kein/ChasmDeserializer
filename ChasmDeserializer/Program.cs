using System;
using System.IO;
using System.Text;
using ChasmDeserializer.JSONConverters;
using ChasmDeserializer.Model;
using ChasmDeserializer.Model.Animation2D;
using ChasmDeserializer.Model.Coversations;
using ChasmDeserializer.Model.FormattedText;
using ChasmDeserializer.Model.Music;
using ChasmDeserializer.Model.Overworld;
using ChasmDeserializer.Model.Particles;
using ChasmDeserializer.Model.RoomManager;
using Newtonsoft.Json;

namespace ChasmDeserializer
{
    class Program
    {
        // No I'm not using NDesk or Mono.Options, bite me
        private const string version = "\nCurrent version: v0.4 (07 Aug 2018) by KZ";
        private const string helpMsg = "\nUSAGE:\n" +
            "ChasmDes.exe [action] [type] [inputFile] <outputFile>\n" +
            "ACTIONS:\n" +
            " -d\tdeserialize/decode game-file into JSON\n" +
            " -s\tserialize/encode JSON into compatible game-file\n" +
            "TYPES:\n" +
            " -csv\tfor game files with extension '*.ser' (except formatted_text)\n" +
            " -fmt\tspecial case for 'formatted_text.ser' files\n" +
            " -anm\tfor '.bin' files related to animation data (animations.json.bin)\n" +
            " -atl\tfor '.bin' files related to texture data (texture_atlas.json.bin)\n" +
            " -con\tfor '.bin' files related to conversations (conversations.json.bin)\n" +
            " -rmm\tfor '.bin' files related to room managment (roommanager.json.bin)\n" +
            " -mus\tfor '.bin' files related to music managment (musicmanager.json.bin)\n" +
            " -ovw\tfor '.bin' files related to overworld state (overworld.json.bin)\n" +
            " -par\tfor '.bin' files related to particle data (particles.json.bin)\n" +
            " -usr\tprocesses global savegame (UserInfo.cfg)\n" +
            " -sav\tprocesses specific savegame slot (*.sav)\n";

        private const string defaultDesExt = ".json";
        private const string defaultSerExt = ".out";

        static readonly JsonConverter[] Anim2DConverters = { new XNAVector2Converter(), new XNARectangleConverter() };
        static readonly JsonConverter[] TextureConverters = { new XNARectangleConverter() };
        static readonly JsonConverter[] SaveDataConverters = { new ItemConverter(), new XNAVector2Converter() };
        static readonly JsonConverter[] FormtTextCoverters = { new XNAVector2Converter(), new XNAColorConverter() };
        static readonly JsonConverter[] ParticleConverters = { new XNARectangleConverter(), new XNAVector2Converter(), new XNAColorConverter() };
        static readonly JsonConverter[] OverworldConverters = { new XNAPointConverter() };

        static string reply = String.Empty;

        static void Main(string[] args)
        {
            if (args == null || args.Length < 3 || !File.Exists(args[2]))
            {
                Console.Write($"No proper arguments were specified or input file not found.{helpMsg}{version}");
                return;
            }
            var action = args[0].ToLower();
            var type = args[1].ToLower();
            var inFile = args[2];
            var outFile = args.Length > 3 ? args[3] : action == "-d" ? $"{args[2]}{defaultDesExt}" : $"{args[2]}{defaultSerExt}" ;
            reply = "Done";
            try
            {
                switch (type)
                {
                    case "-csv":
                        if (action == "-d")
                        {
                            var binary = LoadBinary<CSVData>(inFile);
                            var ext = outFile.LastIndexOf('.') > -1 && (outFile.Length - outFile.LastIndexOf('.')) == 4 ? outFile.Substring(outFile.LastIndexOf('.') + 1) : string.Empty;
                            var decoded = String.Equals(ext, "csv", StringComparison.OrdinalIgnoreCase) ? processCVS(binary).ToString() : JsonConvert.SerializeObject(binary);
                            File.WriteAllText(outFile, decoded);
                        }
                        else if (action == "-s")
                        {
                            var cvsdata = JsonConvert.DeserializeObject<CSVData>(File.ReadAllText(inFile));
                            SaveBinary<CSVData>(outFile, cvsdata);
                        }
                        else { reply = $"No proper arguments were specified.{helpMsg}{version}"; }
                        break;
                    case "-anm":
                            ProcessObject<Animations>(action, inFile, outFile, Formatting.Indented, Anim2DConverters);
                        break;
                    case "-atl":
                            ProcessObject<TextureInfoList>(action, inFile, outFile, Formatting.Indented, TextureConverters);
                        break;
                    case "-usr":
                            ProcessObject<UserInfo>(action, inFile, outFile, Formatting.Indented);
                        break;
                    case "-sav":
                            ProcessObject<WorldSaveState>(action, inFile, outFile, Formatting.Indented, SaveDataConverters);
                        break;
                    case "-fmt":
                            ProcessObject<FormattedText>(action, inFile, outFile, Formatting.Indented, FormtTextCoverters);
                        break;
                    case "-con":
                            ProcessObject<ConversationSystem>(action, inFile, outFile, Formatting.Indented);
                        break;
                    case "-rmm":
                            ProcessObject<RoomManager>(action, inFile, outFile, Formatting.Indented);
                        break;
                    case "-mus":
                            ProcessObject<MusicManager>(action, inFile, outFile, Formatting.Indented);
                        break;
                    case "-ovw":
                            ProcessObject<OverWorldManager>(action, inFile, outFile, Formatting.Indented, OverworldConverters);
                        break;
                    case "-par":
                            ProcessObject<ParticleEngine>(action, inFile, outFile, Formatting.Indented, ParticleConverters);
                        break;
                    default:
                        reply = $"No proper arguments were specified.{helpMsg}{version}";
                        break;
                }
            }
            catch (Exception ex) { reply = $"ERROR: {ex.Message}"; }
            Console.Write(reply);
        }

        // Generics

        private static void ProcessObject<T>(string action, string inFile, string outFile, Formatting format = Formatting.Indented, JsonConverter[] converters = null) where T : IBinarySaveLoad
        {
            if (action == "-d")
            {
                T target = (T)((object)Activator.CreateInstance(typeof(T)));
                using (BinaryReader binaryReader = new BinaryReader(File.Open(inFile, FileMode.Open, FileAccess.Read)))
                {
                    target.Load(binaryReader);
                }
                var x = JsonConvert.SerializeObject(target, format, converters);
                File.WriteAllText(outFile, x);
            }
            else if (action == "-s")
            {
                var json = File.ReadAllText(inFile);
                var target = JsonConvert.DeserializeObject<T>(json, converters);
                using (FileStream fileStream = File.Open(outFile, FileMode.Create, FileAccess.Write))
                {
                    BinaryWriter writer = new BinaryWriter(fileStream);
                    target.Save(writer);
                }
            }
            else
            {
                reply = $"No proper arguments were specified.{helpMsg}";
                return;
            }
        }
        private static StringBuilder processCVS(CSVData data)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(string.Join(",", data.headers));
            foreach (var list in data.values)
            {
                for (int i = 0; i < list.Count; i++)
                    if (list[i].IndexOf(',') > -1 || list[i].IndexOf('"') > -1 || list[i].IndexOf('\n') > -1)
                        list[i] = String.Concat("\"", list[i], "\"");
                builder.AppendLine(string.Join(",", list));
            }
            return builder;
        }
        private static T LoadBinary<T>(string path) where T : IBinarySaveLoad
        {
            using (FileStream fileStream = File.OpenRead(path))
            {
                BinaryReader read = new BinaryReader(fileStream);
                T result = (T)((object)Activator.CreateInstance(typeof(T)));
                result.Load(read);
                return result;
            }
            return default(T);
        }
        private static void SaveBinary<T>(string path, T obj) where T : IBinarySaveLoad
        {
            using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
            {
                BinaryWriter writer = new BinaryWriter(fileStream);
                obj.Save(writer);
            }
        }

        // Unused
        private static void DeserializeObject<T>(string inFile, string outFile, Formatting format = Formatting.Indented, JsonConverter[] converters = null) where T : IBinarySaveLoad
        {
            T target = (T)((object)Activator.CreateInstance(typeof(T)));
            using (BinaryReader binaryReader = new BinaryReader(File.Open(inFile, FileMode.Open, FileAccess.Read)))
            {
                target.Load(binaryReader);
            }
            var x = JsonConvert.SerializeObject(target, format, converters);
            File.WriteAllText(outFile, x);
        }
        private static void SerializeObject<T>(string inFile, string outFile, JsonConverter[] converters = null) where T : IBinarySaveLoad
        {
            var json = File.ReadAllText(inFile);
            var target = JsonConvert.DeserializeObject<T>(json, converters);
            using (FileStream fileStream = File.Open(outFile, FileMode.Create, FileAccess.Write))
            {
                BinaryWriter writer = new BinaryWriter(fileStream);
                target.Save(writer);
            }
        }
        private static void ShowHelp(string msg = null)
        {
            msg = msg ?? string.Empty;
            Console.WriteLine(msg);
            Console.Write(helpMsg);
            Console.Write(version);
        }
        
    
    }
}
