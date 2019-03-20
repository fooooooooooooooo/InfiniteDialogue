using Microsoft.Xna.Framework.Content;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDialogue.Framework
{
    public class Shit
    {
        public void Yeet(bool yoot = false) {

            var villagers = new List<NPC>();
            foreach (var location in Game1.locations) {
                foreach (var npc in location.characters) {
                    if (npc == null)
                        continue;
                    if (!villagers.Contains(npc) && npc.isVillager()) {
                        villagers.Add(npc);
                    }
                }
            }

            var dialogue = new Dictionary<string, string>();
            foreach (var npc in villagers) {

                if (yoot) {
                    try {
                        string key = "";
                        dialogue = Game1.content.Load<Dictionary<string, string>>("Characters\\Dialogue\\" + npc.Name).Select(pair => {
                            key = pair.Key;
                            string str1 = pair.Value;
                            if (str1.Contains("¦"))
                                str1 = !Game1.player.IsMale ? str1.Substring(str1.IndexOf("¦") + 1) : str1.Substring(0, str1.IndexOf("¦"));
                            string str2 = str1;
                            return new KeyValuePair<string, string>(key, str2);
                        }).ToDictionary(p => p.Key, p => p.Value);
                        foreach (var thing in dialogue) {
                            npc.CurrentDialogue.Push(new Dialogue(thing.Value, npc));
                        }
                    } catch (ContentLoadException ex) {
                        dialogue = new Dictionary<string, string>();
                    }
                } else {
                    npc.CurrentDialogue = null;
                }
            }
        }
    }
}
