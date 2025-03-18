using System.Text.Json;

namespace FFXIVConnector
{
    public static class OpCodeManager
    {
        public static async Task UpdateOpcodes()
        {
            string url = "https://raw.githubusercontent.com/karashiiro/FFXIVOpcodes/112ddfe2f14f2eeb5730bc6acf887578e642fe57/opcodes.min.json";
            var opcodes = await FetchGlobalOpCodesAsync(url);

            string libraryRoot = GetLibraryRoot("FFXIVConnector");
            string opCodesPath = Path.Combine(libraryRoot, "Network", "Models", "OpCodes.cs");

            UpdateEnumFile(opCodesPath, opcodes);
        }

        static string GetLibraryRoot(string libraryName)
        {
            string? dir = Directory.GetCurrentDirectory();

            while (!string.IsNullOrEmpty(dir) && !Directory.Exists(Path.Combine(dir, libraryName)))
            {
                dir = Directory.GetParent(dir)?.FullName;
            }

            if (string.IsNullOrEmpty(dir))
            {
                throw new Exception($"Library root for {libraryName} not found.");
            }

            return Path.Combine(dir, libraryName);
        }

        static async Task<Dictionary<string, Dictionary<string, int>>> FetchGlobalOpCodesAsync(string url)
        {
            using HttpClient client = new HttpClient();
            string json = await client.GetStringAsync(url);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var allVersions = JsonSerializer.Deserialize<List<OpcodeVersion>>(json, options)!;

            var globalVersion = allVersions.FirstOrDefault(v => v.Region == "Global");
            if (globalVersion == null)
            {
                throw new Exception("No global opcodes found.");
            }

            var opcodesByCategory = new Dictionary<string, Dictionary<string, int>>();

            foreach (var category in globalVersion.Lists)
            {
                var extractedOpcodes = category.Value
                    .ToDictionary(o => o.Name, o => o.Opcode);

                if (extractedOpcodes.Any())
                    opcodesByCategory[category.Key] = extractedOpcodes;
            }

            return opcodesByCategory;
        }

        static void UpdateEnumFile(string filePath, Dictionary<string, Dictionary<string, int>> opcodesByCategory)
        {
            var enumContent = GenerateEnumCode(opcodesByCategory);
            File.WriteAllText(filePath, enumContent);
        }

        static string GenerateEnumCode(Dictionary<string, Dictionary<string, int>> opcodesByCategory)
        {
            var code = new System.Text.StringBuilder();
            code.AppendLine("// Auto-generated file. Do not modify manually.");
            code.AppendLine();
            code.AppendLine("namespace FFXIVConnector.Network.Models");
            code.AppendLine("{");

            foreach (var category in opcodesByCategory)
            {
                code.AppendLine($"    public enum {category.Key} : ushort");
                code.AppendLine("    {");

                foreach (var entry in category.Value)
                {
                    code.AppendLine($"        {entry.Key} = {entry.Value},");
                }

                code.AppendLine("    }");
                code.AppendLine();
            }

            code.AppendLine("}");
            return code.ToString();
        }


        class OpcodeVersion
        {
            public string Version { get; set; } = "";
            public string Region { get; set; } = "";
            public Dictionary<string, List<OpcodeEntry>> Lists { get; set; } = new();
        }

        class OpcodeEntry
        {
            public string Name { get; set; } = "";
            public int Opcode { get; set; }
        }
    }
}