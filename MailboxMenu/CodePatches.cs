using StardewModdingAPI;
using StardewValley;
using System.Linq;
using StardewValley.Tools;

namespace MailboxMenu
{
    public partial class ModEntry
    {
        public class GameLocation_mailbox_Patch
        {
            private static bool UsingMailServices() {
                if (!isMailServicesActive) return false;

                return Game1.player.ActiveObject != null ||
                       Game1.player.CurrentTool is Axe ||
                       Game1.player.CurrentTool is Pickaxe ||
                       Game1.player.CurrentTool is Hoe ||
                       Game1.player.CurrentTool is WateringCan;
            }            
            
            public static bool Prefix(GameLocation __instance)
            {
                if (!Config.ModEnabled || 
                    !Config.MenuOnMailbox ||
                    (Config.ModKey != SButton.None && !SHelper.Input.IsDown(Config.ModKey)) || 
                    UsingMailServices()) 
                    return true;
                
                var list = Game1.mailbox.Where(str => GetMailString(str) == "").ToList();

                if (list.Count == 0) {
                    Game1.activeClickableMenu = new MailMenu();
                    return false;
                }

                foreach (var str in Game1.mailbox) {
                    if (!list.Contains(str)) list.Add(str);
                }

                Game1.mailbox.Clear();
                foreach (var str in list) Game1.mailbox.Add(str);
                
                return true;
            }
        }
    }
}