using Luftschiff.Code.Game.Monsters;

namespace Luftschiff.Code.States.Fights
{
    internal class PetunieFight : FightState
    {
        public PetunieFight()
        {
            CurrentMonsterList.Add(new Petunie());
        }
    }
}