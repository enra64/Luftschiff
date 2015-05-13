using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.States.Fights
{
    internal class DragonFight : FightState
    {
        public DragonFight()
        {
            CurrentMonsterList.Add(new Dragon());
        }
    }
}