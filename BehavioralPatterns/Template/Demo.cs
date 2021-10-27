using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Template
{

    //A high-level blueprint for an alghoritm to be completed by inheritors
    //Similar to the strategy, based on inheritance rather than interfaces
    public class Demo
    {
        //public static void Main(string[] args)
        //{
        //    var chess = new Chess();
        //    chess.Run();
        //}
    }

    public abstract class Game
    {
        public Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }
        public void Run()
        {
            Start();
            while (!HaveWinner)
                TakeTurn();
            Console.WriteLine($"Player {WinningPlayer} has won the game!");
        }

        protected int currentPlayer;
        protected readonly int numberOfPlayers;
        protected abstract void TakeTurn();
        protected abstract bool HaveWinner { get; }
        protected abstract int WinningPlayer { get; }

        protected abstract void Start();

    }

    public class Chess : Game
    {
        private int turn = 1;
        private int maxTurns = 10;


        public Chess():base(2)
        {

        }
        protected override bool HaveWinner => turn == maxTurns;
        protected override int WinningPlayer => currentPlayer;

        protected override void Start()
        {
            Console.WriteLine($"Starting the game of chess with number of player {numberOfPlayers}.");
        }

        protected override void TakeTurn()
        {
            Console.WriteLine($"Turn {turn++} taken by {currentPlayer}.");
            currentPlayer = (currentPlayer += 1) % numberOfPlayers;
        }
    }
}
