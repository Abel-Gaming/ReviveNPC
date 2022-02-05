using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System;
using System.Collections.Generic;

namespace server
{
    public class Main : BaseScript
    {
        public Main()
        {
            Load.Config();
            EventHandlers["ReviveNPC:TriggerServerRevive"] += new Action<Player, int>(RevivePedServer);
        }

        public static void SendCommand(string command)
        {
            RegisterCommand(command, new Action<int, List<object>, string>((source, args, raw) =>
            {
                PlayerList pl = new PlayerList();
                Player player = pl[source];
                player.TriggerEvent("ReviveNPC:DoCommand");
            }), false);
        }

        private void RevivePedServer([FromSource] Player player, int Ped)
        {
            TriggerClientEvent("ReviveNPC:TriggerClientRevive", Ped);
        }
    }
}
