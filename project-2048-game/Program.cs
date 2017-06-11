using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using pb069_project_2048game._2048Game;

namespace pb069_project_2048game
{
    class Program
    {
        static void Main(string[] args)
        {

            // Used just for testing

            Console.WriteLine("Classic mode");
            var classic = new ClassicMode();
            PrintClassic(classic);
            
            Console.WriteLine("Classic Left");
            classic.MoveLeft();
            PrintClassic(classic);
            
            Console.WriteLine("Classic Up");
            classic.MoveUp();
            PrintClassic(classic);

            Console.WriteLine("Classic Down");
            classic.MoveDown();
            PrintClassic(classic);

            Console.WriteLine("Classic Right");
            classic.MoveRight();
            PrintClassic(classic);

            Console.WriteLine("Classic New Game");
            classic.CreateNewGame();
            PrintClassic(classic);
            
            // Quantum Mode
            Console.WriteLine("Quantum mode");
            var quantum = new QuantumMode();
            PrintQuantum(quantum);

            Console.WriteLine("Quantum Left");
            quantum.MoveLeft();
            PrintQuantum(quantum);

            Console.WriteLine("Quantum Up");
            quantum.MoveUp();
            PrintQuantum(quantum);

            Console.WriteLine("Quantum Down");
            quantum.MoveDown();
            PrintQuantum(quantum);

            Console.WriteLine("Quantum Right");
            quantum.MoveRight();
            PrintQuantum(quantum);

            Console.WriteLine("Quantum New Game");
            quantum.CreateNewGame();
            PrintQuantum(quantum);
            Console.ReadKey();
        }

        public static void PrintClassic(ClassicMode classicMode)
        {
            for (var i = 0; i <= classicMode.Board.GetUpperBound(0); i++)
            {
                for (var j = 0; j < classicMode.Board.Length; j++)
                {
                    Console.Write(" | ");
                    Console.Write(classicMode.Board[i][j]);
                    Console.Write(" | ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Classic mode Score= " + classicMode.Score);
            Console.WriteLine();

            Console.ReadKey();
        }

        public static void PrintQuantum(QuantumMode quantumMode)
        {
            for (var i = 0; i <= quantumMode.Board.GetUpperBound(0); i++)
            {
                for (var j = 0; j < quantumMode.Board.Length; j++)
                {
                    Console.Write("{ ");
                    foreach (var value in quantumMode.Board[i][j].TileSet)
                    {
                        Console.Write("|");
                        Console.Write(value);

                    }
                    Console.Write(" }  ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Quantum mode Score= " + quantumMode.Score);
            Console.WriteLine();


            Console.ReadKey();
        }
    }
}
