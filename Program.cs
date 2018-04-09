using System;
using System.Collections.Generic;
using System.IO;

namespace BudgetPro
{
    class Program
    {
        public static List<Budget> Budgets = new List<Budget>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Budget Pro Omega Alpha Premier Razzle Tazzle Yes!");
            HomeScreen();

        }

        static void HomeScreen()
        {
            //Moved to top ^^^
            //Console.WriteLine("Welcome To Budget Pro Omega Alpha Premier Razzle Tazzle Yes!");
            Console.WriteLine("\nPlease select an option: \n1) View Budgets \n2) Add Budget \n" +
                "3) Save Budgets \n4) Load Budgets \n5) Quit");
            string input = Console.ReadLine();

            if (input == "1")
            {
                ViewBuds();
            }
            else if (input == "2")
            {
                Budget.Build();
                HomeScreen();
            }
            /*else if (input == "3")
            {
                save();
            }
            else if (input == "4")
            {
                load();
            }*/
            else if (input == "5")
            {
                Console.WriteLine("Are you sure?  Y = Yes, N = No");
                string innput = Console.ReadLine();
                if (innput == "N")
                {
                    HomeScreen();
                }
                else
                {
                    Console.WriteLine("\n\nAdios!!!\n\n**Press Enter to Exit**");
                    Console.ReadLine();
                }
            }

        }

        private static void ViewBuds()
        {
            int i = 0;
            Console.WriteLine("\nCurrent Budgets: \n");
            foreach (Budget aBudget in Budgets)
            {
                i++;
                Console.WriteLine("{0}: {1} \n\n  Current Amount: ${2}\n", i, aBudget.Name, aBudget.CurrentAmount);
                
            }
            ChooseBud();
        }

        //>>>>>>>>>>>>>>>>>>>>public static int budTrackr = 0;
        //budTrackr keeps track of budgets for editing..  DUH!
        private static void ChooseBud()
        {
            Console.WriteLine("\n\n Enter Budget number to select Budget.  Otherwise, just press enter to return home.\n\n");
            string select = Console.ReadLine();

            if (select == "")
            {
                HomeScreen();
            }
            else
            {
                //int budTrackr = 0;
                //<<<<<<THIS IS WHERE YOU LEFT OFF 4/3/18!  YOU'RE ADDING THE "VIEW SINGLE BUDGET" FEATURE
                if (int.TryParse(select, out int Amount))
                {
                    Amount -= 1;
                    
                    Console.WriteLine("\n\n{0} \n\n  ${1}", Budgets[Amount].Name, Budgets[Amount].CurrentAmount);
                    //Console.WriteLine(Budgets[Amount].CurrentAmount);
                    //Console.WriteLine("Would you like to edit this budget?  \n\nPlease select an option below.");
                    EditMenu(Amount);
                }
                else
                {
                    Console.WriteLine("\n**Invalid Input.  Please try again.**\n");
                    ViewBuds();
                    
                }
                //Console.WriteLine(Budgets[select].Name);
                //Console.WriteLine(Budgets[select].CurrentAmount);
                //4/5 Leaving these here temporarily for C&P if needed.
            }
        }

        private static void EditMenu(int budTracker)
        {
            //int budTracker = budTrackr;

            Console.WriteLine("\n\nPlease select an option below. \n\n 1) Add Funds \n\n 2) Subtract Funds" +
                "\n\n 3) Rename Budget\n\n 4) Delete Budget\n\n 5) Return to Budget List\n\n 6) Return Home");

            string select = Console.ReadLine();

            if (select == "1")
            {
                BudgetMan.Add(budTracker);
            }
            else if (select == "2")
            {
                BudgetMan.Subtract(budTracker);
            }
            else if (select == "3")
            {
                BudgetMan.Rename(budTracker);
            }
            else if (select == "4")
            {
                BudgetMan.Delete(budTracker);
            }
            else if (select == "5")
            {
                ViewBuds();
            }
            else
            {
                HomeScreen();
                
            }
            //4/5/18 LEFT OFF HERE.  ADD SUBTRACT AND SUCH NEXT.
        }

        public static void BuildFail()
        {
            HomeScreen();
        }

        public static void FinishEdit(int trackr)
        {
            Console.WriteLine("\n\n{0} \n\nNew Amount: ${1}\n", Budgets[trackr].Name, Budgets[trackr].CurrentAmount);
            EditMenu(trackr);
        }

        public static void FinishEdit()
        {
            HomeScreen();
        }

    }

     class Budget
    {
        public string Name { get; set; }

        public decimal CurrentAmount { get; set; }

        public static void Build()
        {
            Budget NewBud = new Budget()
            {
                Name = BudgetName(),
                CurrentAmount = BudgetAmount(),
            };
            Program.Budgets.Add(NewBud);
            
        }



        private static string BudgetName()
        {

            Console.WriteLine("\nPlease enter name for your budget. \n");
            string Name = Console.ReadLine();
            return Name;

        }

        private static decimal BudgetAmount()
        {

            Console.WriteLine("\nWhat is the Current Balance?\nFormat: Use #'s and \".\" only\n");
            String CurAmount = Console.ReadLine();
            while (true)
            {

                if (decimal.TryParse(CurAmount, out decimal Amount))
                {
                    
                    return Amount;
                }
                else
                {
                    Console.WriteLine("\n**Invalid Input.  Please try again.**\n");
                    Program.BuildFail();
                    
                }
                return Amount;
            }
            

        }

    }

    class BudgetMan
    {
        public static void Add(int trackr)
        {
            
            Console.WriteLine("\n\n Please enter amount to add to {0}: ", Program.Budgets[trackr].Name);
            string deposit = Console.ReadLine();
            if (decimal.TryParse(deposit, out decimal Amount))
            {
                Program.Budgets[trackr].CurrentAmount += Amount;
               //Console.WriteLine("\n\n{0} \n\nNew Amount: ${1}\n", Program.Budgets[trackr].Name, Program.Budgets[trackr].CurrentAmount);
                Program.FinishEdit(trackr);
            }
            else
            {
                Console.WriteLine("\n**Invalid Input.  Please try again.**\n");
                Add(trackr);

            }
        }

        public static void Subtract(int trackr)
        {

            Console.WriteLine("\n\n Please enter amount to subtract from {0}: ", Program.Budgets[trackr].Name);
            string deposit = Console.ReadLine();
            if (decimal.TryParse(deposit, out decimal Amount))
            {
                Program.Budgets[trackr].CurrentAmount -= Amount;
                //Console.WriteLine("\n\n{0} \n\nNew Amount: ${1}\n", Program.Budgets[trackr].Name, Program.Budgets[trackr].CurrentAmount);
                Program.FinishEdit(trackr);
            }
            else
            {
                Console.WriteLine("\n**Invalid Input.  Please try again.**\n");
                Subtract(trackr);

            }
        }

        public static void Rename(int trackr)
        {
            Console.WriteLine("\n\n Please enter a new name for budget {0}\n\n", Program.Budgets[trackr].Name);
            Program.Budgets[trackr].Name = Console.ReadLine();
            //Console.WriteLine("\n\n{0} \n\nNew Amount: ${1}\n", Program.Budgets[trackr].Name, Program.Budgets[trackr].CurrentAmount);
            Program.FinishEdit(trackr);            
        }

        public static void Delete(int trackr)
        {
            Console.WriteLine("\n\nAre you sure you would like to delete this budget? \n\n{0}\n  ${1}\n\nPlease enter Y" +
                " for yes or anything else for no.", Program.Budgets[trackr].Name, Program.Budgets[trackr].CurrentAmount);
            string input = Console.ReadLine();
            if (input == "Y" || input == "y")
            {
                Program.Budgets.RemoveAt(trackr);
                Program.FinishEdit();
            }
            else
            {
                Program.FinishEdit(trackr);
            }
        }

    }



    
 }











