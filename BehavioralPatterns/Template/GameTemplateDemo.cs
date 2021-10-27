using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.BehavioralPatterns.Template
{
    public class GameTemplateDemo
    {
        //public static void Main(string[] args)
        //{
        //    var numberOfPlayers = 2;
        //    var currenPlayer = 0;
        //    int turn = 1, maxTurns = 10;

        //    void Start()
        //    {
        //        Console.WriteLine($"Start a game with {numberOfPlayers} players");
        //    }

        //    bool HaveWinner()
        //    {
        //        return turn == maxTurns;
        //    }

        //    void TakeTurn()
        //    {
        //        Console.WriteLine($"Turn {turn++} taken by player {currenPlayer}");
        //        currenPlayer = (currenPlayer += 1) % numberOfPlayers;

        //    }

        //    int WinningPlayer()
        //    {
        //        return currenPlayer;
        //    }

        //    GameTemplate.Run(Start, TakeTurn, HaveWinner, WinningPlayer);
        //}
    }

    public static class GameTemplate
    {
        public static void Run(
            Action start,
            Action takeTurn,
            Func<bool> haveWinner,
            Func<int> winningPlayer)
        {
            start();
            while(!haveWinner())
                takeTurn();
            Console.WriteLine($"Player {winningPlayer()} wins.");
        }
    }
}
