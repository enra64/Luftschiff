using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.States.Fights
{
    internal class WhaleFight : FightState
    {
        public WhaleFight()
        {
            CurrentMonsterList.Add(new Skywhale());
        }
    }
}