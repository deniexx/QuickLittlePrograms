#nullable enable
using System;
using System.Threading;
using System.IO;

namespace Area51
{
    class Agents
    {
        public string secLevel = "";
        public int floor = 0;
    }

    class Program
    {
        static void Main()
        {
            int i = 0;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt")))
            {
                outputFile.WriteLine("Output: ");
                outputFile.WriteLine("-----------------------------------");
            }
            Thread agent = new Thread(delegate()
            {
                AgentThread(i);
            });
            agent.Start();
        }

        static void AgentThread(int i)
        {
            string[] secLevels = { "c", "s", "ts" };
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            int[] floors = {1, 2, 3, 4}; //1-g, 2-s- 3-t1, 4-t2
            Random rand = new Random();

            Agents agent = new Agents();
            agent.secLevel = secLevels[rand.Next(0, 2)];

            if (agent.secLevel == "c")
            {
                agent.floor = 1;
            }
            else if (agent.secLevel == "s")
            {
                agent.floor = floors[rand.Next(1,2)];
            }
            else if (agent.secLevel == "ts")
            {
                agent.floor = floors[rand.Next(1,4)];
            }

            int agentNum = i + 1;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
            {
                outputFile.WriteLine("Agent " + agentNum + " spawned at floor number " + agent.floor + ", with security level: " + agent.secLevel);
            }
            Console.WriteLine("Agent spawned at floor number " + agent.floor + ", with security level: " + agent.secLevel);
            
            Thread elevator = new Thread(delegate()
            {
                ElevatorThread(agent.floor, agent.secLevel, docPath, i);
            });
            elevator.Start();
        }

        static void ElevatorThread(int agFloor, string agSec, string docPath, int i)
        {
            string[] inputs = {"y", "n"};
            Random rand = new Random();
            int elevatorPos = rand.Next(1, 4);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
            {
                outputFile.WriteLine("Starting position of the elevator is " + elevatorPos);
            }
            string input = inputs[rand.Next(0, 1)];

            if (input == "y")
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                {
                    outputFile.WriteLine("Agent calls the elevator.");
                }
                Console.WriteLine("Elevator starts moving.");
                if (agFloor > elevatorPos)
                {
                    while (elevatorPos != agFloor)
                    {
                        elevatorPos += 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    while (elevatorPos != agFloor)
                    {
                        elevatorPos -= 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
            }
            else if (input == "n")
            {
                Console.WriteLine("Thank you!");
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                {
                    outputFile.WriteLine("Agent doesn't want to call the elevator!");
                }
                Environment.Exit(0);
            }
            
            ElevatorButtons(agFloor, agSec, elevatorPos, docPath, i);
        }

        static void ElevatorButtons(int agFloor, string agSec, int elevatorPos, string docPath, int i)
        {
            string[] buttons = {"g", "s", "t1", "t2"};
            Random rand = new Random();
            string command = buttons[rand.Next(0, 3)];
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
            {
                outputFile.WriteLine("Agent chose " + command.ToUpper() + " button");
            }
            
            if (command == "g")
            {
                if (1 < elevatorPos)
                {
                    while (1 < elevatorPos)
                    {
                        elevatorPos -= 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("You are already on G floor!");
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Agent is already on G floor!");
                    }
                }
            }
            else if (command == "s")
            {
                if (2 < elevatorPos)
                {
                    while (2 < elevatorPos)
                    {
                        elevatorPos -= 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else if (2 > elevatorPos)
                {
                    while (2 > elevatorPos)
                    {
                        elevatorPos += 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("You are already on S floor!");
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Agent is already on S floor!");
                    }
                }
            }
            else if (command == "t1")
            {
                if (3 < elevatorPos)
                {
                    while (3 < elevatorPos)
                    {
                        elevatorPos -= 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else if (3 > elevatorPos)
                {
                    while (3 > elevatorPos)
                    {
                        elevatorPos += 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("You are already on t1 floor!");
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Agent is already on T1 floor!");
                    }
                }
            }
            else if (command == "t2")
            {
                if (4 < elevatorPos)
                {
                    while (4 < elevatorPos)
                    {
                        elevatorPos -= 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else if (4 > elevatorPos)
                {
                    while (4 > elevatorPos)
                    {
                        elevatorPos += 1;
                        ElevatorPosition(elevatorPos, docPath);
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("You are already on t2 floor!");
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Agent is already on T2 floor!");
                    }
                }
            }
            DoorCheck(agFloor, agSec, elevatorPos, docPath, i);
        }

        static void DoorCheck(int agFloor, string agSec, int elevatorPos, string docPath, int i)
        {
            string[] inputs = {"y", "n"};
            Random rand = new Random();
            string command = inputs[rand.Next(0, 1)];
            
            if(command == "n")
                ElevatorButtons(agFloor, agSec, elevatorPos, docPath, i);
            else
            {
                if (agSec == "ts")
                {
                    Console.WriteLine("The door opens.");
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Agent has chosen to open the door.");
                        outputFile.WriteLine("-----------------------------------");
                    }
                    
                    i += 1;
                    if (i < 3)
                    {
                        Thread agent = new Thread(delegate()
                        {
                            AgentThread(i);
                        });
                        agent.Start();
                    }
                    
                }
                else if (agSec == "s")
                {
                    if (elevatorPos <= 2)
                    {
                        Console.WriteLine("The door opens.");
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                        {
                            outputFile.WriteLine("Agent has chosen to open the door.");
                            outputFile.WriteLine("-----------------------------------");
                        }
                        
                        i += 1;
                        if (i < 3)
                        {
                            Thread agent = new Thread(delegate()
                            {
                                AgentThread(i);
                            });
                            agent.Start();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have permission, please choose another floor!");
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                        {
                            outputFile.WriteLine("Agent hasn't got permission to open the door!");
                        }
                        ElevatorButtons(agFloor, agSec, elevatorPos, docPath, i);
                    }
                }
                else
                {
                    if (elevatorPos == 1)
                    {
                        Console.WriteLine("The door opens.");
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                        {
                            outputFile.WriteLine("Agent has chosen to open the door.");
                            outputFile.WriteLine("-----------------------------------");
                        }

                        i += 1;
                        if (i < 3)
                        {
                            Thread agent = new Thread(delegate()
                            {
                                AgentThread(i);
                            });
                            agent.Start();
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have permission, please choose another floor!");
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                        {
                            outputFile.WriteLine("Agent hasn't got permission to open the door!");
                        }
                        ElevatorButtons(agFloor, agSec, elevatorPos, docPath, i);
                    }
                }
            }
        }
        
        static void ElevatorPosition(int elevPos, string docPath)
        {
            if (elevPos == 1)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                {
                    outputFile.WriteLine("Elevator is at G floor");
                }
                Console.WriteLine("Elevator is at G floor");
            }
            else if (elevPos == 2)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                {
                    outputFile.WriteLine("Elevator is at S floor");
                }
                Console.WriteLine("Elevator is at S floor");
            }
            else if (elevPos == 3)
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                {
                    outputFile.WriteLine("Elevator is at T1 floor");
                }
                Console.WriteLine("Elevator is at T1 floor");
            }
            else
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                {
                    outputFile.WriteLine("Elevator is at T2 floor");
                }
                Console.WriteLine("Elevator is at T2 floor");
            }
        }
    }
}