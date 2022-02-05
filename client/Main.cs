using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;

namespace client
{
    public class Main : BaseScript
    {
        public Main()
        {
            //Events
            EventHandlers["ReviveNPC:DoCommand"] += new Action(RegisterReviveCommand);
            EventHandlers["ReviveNPC:TriggerClientRevive"] += new Action<int>(RevivePedClient);
        }

        private static void RegisterReviveCommand()
        {
            RevivePed();
        }

        private static async void RevivePed()
        {
            Ped ped1;
            Ped[] allPeds = World.GetAllPeds();
            float num = float.MaxValue;
            ped1 = (Ped)null;
            foreach (Ped ped2 in allPeds)
            {
                if (!Entity.Equals((Entity)Game.PlayerPed, (Entity)ped2))
                {
                    float distance = World.GetDistance(((Entity)ped2).Position, ((Entity)Game.PlayerPed).Position);
                    if ((double)distance < (double)num)
                    {
                        ped1 = ped2;
                        num = distance;
                    }
                }
            }

            float checkdistance = World.GetDistance(Game.Player.Character.Position, ped1.Position);

            if (checkdistance <= 2.0f)
            {
                //Block Events
                ped1.BlockPermanentEvents = true;

                //CPR Intro
                Game.Player.Character.Task.PlayAnimation("mini@cpr@char_a@cpr_def", "cpr_intro");
                ped1.Task.PlayAnimation("mini@cpr@char_b@cpr_def", "cpr_intro");

                //Wait
                await BaseScript.Delay(3000);

                //Chest Pump
                Game.Player.Character.Task.PlayAnimation("mini@cpr@char_a@cpr_str", "cpr_pumpchest");
                ped1.Task.PlayAnimation("mini@cpr@char_b@cpr_str", "cpr_pumpchest");

                //Wait
                await BaseScript.Delay(1000);

                //Chest Pump
                Game.Player.Character.Task.PlayAnimation("mini@cpr@char_a@cpr_str", "cpr_pumpchest");
                ped1.Task.PlayAnimation("mini@cpr@char_b@cpr_str", "cpr_pumpchest");

                //Wait
                await BaseScript.Delay(1500);

                //Trigger Server Revive Event
                TriggerServerEvent("ReviveNPC:TriggerServerRevive", ped1.Handle);
            }
        }

        private static void RevivePedClient(int PedID)
        {
            API.ClearPedTasksImmediately(PedID);
            API.ResurrectPed(PedID);
            int MaxHealth = API.GetEntityMaxHealth(PedID);
            API.SetEntityHealth(PedID, MaxHealth);
            API.ClearPedTasksImmediately(PedID);
            API.ClearPedBloodDamage(PedID);
        }
    }
}
