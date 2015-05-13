using System;
using Luftschiff.Code.Game.Monsters;
using SFML.System;

namespace Luftschiff.Code.States.Fights
{
    internal class BatFight : FightState
    {
        public BatFight()
        {
            Console.WriteLine("Get Rekt! Bats are in the House!");
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f, 200f)));
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f, 250f)));
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f + 130f, 220f)));
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f + 55f, 200f)));
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f, 170f)));
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f + 55f, 200f)));
            CurrentMonsterList.Add(new Bat(new Vector2f(Controller.Window.Size.X/1.5f - 20f, 100f)));
        }
    }
}