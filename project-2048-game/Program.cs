using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using pb069_project_2048game.Model;

namespace pb069_project_2048game
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassicMode classic = new ClassicMode();
            PrintClassic(classic);
            
            Console.WriteLine("Left");
            classic.MoveLeft();
            PrintClassic(classic);
            
            Console.WriteLine("Up");
            classic.MoveUp();
            PrintClassic(classic);

            Console.WriteLine("Down");
            classic.MoveDown();
            PrintClassic(classic);

            Console.WriteLine("Right");
            classic.MoveRight();
            PrintClassic(classic);

            Console.WriteLine("New Game");
            classic.CreateNewGame();
            PrintClassic(classic);

            // ------------------------------------------------------
            // Quantum Mode
            Console.WriteLine("Quantum mode");
            QuantumMode quantum = new QuantumMode();
            PrintQuantum(quantum);

            Console.WriteLine("Left");
            quantum.MoveLeft();
            PrintQuantum(quantum);

            Console.WriteLine("Up");
            quantum.MoveUp();
            PrintQuantum(quantum);

            Console.WriteLine("Down");
            quantum.MoveDown();
            PrintQuantum(quantum);

            Console.WriteLine("Right");
            quantum.MoveRight();
            PrintQuantum(quantum);

            Console.WriteLine("New Game");
            quantum.CreateNewGame();
            PrintQuantum(quantum);
            Console.ReadKey();

        }

        public static void PrintClassic(ClassicMode classicMode)
        {
            for (int i = 0; i <= classicMode.Board.GetUpperBound(0); i++)
            {
                for (int j = 0; j < classicMode.Board.Length; j++)
                {
                    Console.Write(" | ");
                    Console.Write(classicMode.Board[i][j]);
                    Console.Write(" | ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(classicMode.Score);

            Console.ReadKey();
        }

        public static void PrintQuantum(QuantumMode quantumMode)
        {

            for (int i = 0; i <= quantumMode.Board.GetUpperBound(0); i++)
            {
                for (int j = 0; j < quantumMode.Board.Length; j++)
                {
                    Console.Write(" { ");
                    foreach (var value in quantumMode.Board[i][j].TileSet)
                    {
                        Console.Write("|");
                        Console.Write(value);

                    }
                    Console.Write(" }      ");
                }

                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(quantumMode.Score);

            Console.ReadKey();
        }
    }
}
