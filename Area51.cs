#nullable enable
using System;
using System.Threading;
using System.IO;

namespace Area51
{
    class Agents
    {
        public string SecLevel = "";
        public int Floor;
    }

    static class Program
    {
        private static void Main()
        {
            Random rand = new Random();
            var i = 0;
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            int elevatorPos = rand.Next(1, 4);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt")))
            {
                outputFile.WriteLine("Output: ");
                outputFile.WriteLine("-----------------------------------");
            }
            Thread agent = new Thread(delegate()
            {
                AgentThread(i, elevatorPos);
            });
            agent.Start();
        }

        private static void AgentThread(int i, int elevPos)
        {
            string[] secLevels = { "c", "s", "ts" };
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            int[] floors = {1, 2, 3, 4}; //1-g, 2-s- 3-t1, 4-t2
            Random rand = new Random();

            Agents agent = new Agents {SecLevel = secLevels[rand.Next(0, 2)]};

            agent.Floor = agent.SecLevel switch
            {
                "c" => 1,
                "s" => floors[rand.Next(1, 2)],
                "ts" => floors[rand.Next(1, 4)],
                _ => agent.Floor
            };

            int agentNum = i + 1;
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
            {
                outputFile.WriteLine("Agent " + agentNum + " spawned at floor number " + agent.Floor + ", with security level: " + agent.SecLevel);
            }
            Console.WriteLine("Agent spawned at floor number " + agent.Floor + ", with security level: " + agent.SecLevel);
            
            Thread elevator = new Thread(delegate()
            {
                ElevatorThread(agent.Floor, agent.SecLevel, docPath, i, elevPos);
            });
            elevator.Start();
        }

        static void ElevatorThread(int agFloor, string agSec, string docPath, int i, int elevatorPos)
        {
            Random rand = new Random();
            string[] inputs = {"y", "n"};
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
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true);
                    outputFile.WriteLine("Agent is already on G floor!");
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
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true);
                    outputFile.WriteLine("Agent is already on S floor!");
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
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true);
                    outputFile.WriteLine("Agent is already on T1 floor!");
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
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true);
                    outputFile.WriteLine("Agent is already on T2 floor!");
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
                switch (agSec)
                {
                    case "ts":
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
                                AgentThread(i, elevatorPos);
                            });
                            agent.Start();
                        }

                        break;
                    }
                    case "s" when elevatorPos <= 2:
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
                                AgentThread(i, elevatorPos);
                            });
                            agent.Start();
                        }

                        break;
                    }
                    case "s":
                    {
                        Console.WriteLine("You don't have permission, please choose another floor!");
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                        {
                            outputFile.WriteLine("Agent hasn't got permission to open the door!");
                        }
                        ElevatorButtons(agFloor, agSec, elevatorPos, docPath, i);
                        break;
                    }
                    default:
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
                                    AgentThread(i, elevatorPos);
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

                        break;
                    }
                }
            }
        }
        
        static void ElevatorPosition(int elevPos, string docPath)
        {
            switch (elevPos)
            {
                case 1:
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Elevator is at G floor");
                    }
                    Console.WriteLine("Elevator is at G floor");
                    break;
                }
                case 2:
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Elevator is at S floor");
                    }
                    Console.WriteLine("Elevator is at S floor");
                    break;
                }
                case 3:
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Elevator is at T1 floor");
                    }
                    Console.WriteLine("Elevator is at T1 floor");
                    break;
                }
                default:
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Output.txt"), true))
                    {
                        outputFile.WriteLine("Elevator is at T2 floor");
                    }
                    Console.WriteLine("Elevator is at T2 floor");
                    break;
                }
            }
        }
    }
}
