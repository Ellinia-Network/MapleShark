using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Windows.Forms;
using SharpPcap;
using System.Web.UI.WebControls;

namespace MapleShark
{
    static class MapleKeys
    {
        private static Dictionary<byte, Dictionary<KeyValuePair<ushort, byte>, byte[]>> MapleStoryKeys;

        private static void AddKey(byte locale, ushort version, byte subversion, byte[] key)
        {
            if (!MapleStoryKeys.ContainsKey(locale))
                MapleStoryKeys.Add(locale, new Dictionary<KeyValuePair<ushort, byte>, byte[]>());
            MapleStoryKeys[locale].Add(new KeyValuePair<ushort, byte>(version, subversion), key);
        }

        public static void Initialize()
        {
            Console.WriteLine("Initializing keys...");
            MapleStoryKeys = new Dictionary<byte, Dictionary<KeyValuePair<ushort, byte>, byte[]>>();
            
            AddKey(8, 232, 2, new byte[] { 0x5E, 0x9C, 0x9F, 0x0C, 0x65, 0x89, 0x7A, 0x63 });

            // Quickly count amount of keys
            int keyCount = 0;
            foreach (var kvp in MapleStoryKeys)
                keyCount += kvp.Value.Count;

            Console.WriteLine("Done. {1} keys loaded for {0} locales", MapleStoryKeys.Count, keyCount);
        }

        public static byte[] GetKeyForVersion(byte locale, ushort version, byte subversion)
        {
            if (MapleStoryKeys == null) Initialize();
            if (!MapleStoryKeys.ContainsKey(locale))
            {
                Console.WriteLine("Locale {0} not found!", locale);
                return null;
            }

            // Get first version known
            for (ushort v = version; v > 0; v--)
            {
                for (byte sv = subversion; sv >= 0; sv--)
                {
                    var tuple = new KeyValuePair<ushort, byte>(v, sv);
                    if (MapleStoryKeys[locale].ContainsKey(tuple))
                    {
                        byte[] key = MapleStoryKeys[locale][tuple];
                        byte[] ret = new byte[32];
                        for (int i = 0; i < 8; i++)
                            ret[i * 4] = key[i];

                        Console.WriteLine("Using key for version {0}.{1}", v, sv);
                        return ret;
                    }
                    if (sv == 0) break;
                }
            }
            Console.WriteLine("Version {0}.{1} for locale {2} not found!", version, subversion, locale);
            return null;
        }
    }
}
