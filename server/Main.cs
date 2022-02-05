using CitizenFX.Core;
using System;

namespace server
{
    public class Main : BaseScript
    {
        public Main()
        {
            EventHandlers["ReviveNPC:TriggerServerRevive"] += new Action<Player, int>(RevivePedServer);
        }

        private void RevivePedServer([FromSource] Player player, int Ped)
        {
            TriggerClientEvent("ReviveNPC:TriggerClientRevive", Ped);
        }
    }
}
