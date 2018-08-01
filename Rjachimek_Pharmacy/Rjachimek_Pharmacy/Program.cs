using Rjachimek_Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Rjachimek_Pharmacy
{
    public static class Program
    {

        static void Main(string[] args)
        {


			do
			{
				Console.Clear();
				string command;
				Console.WriteLine("Menu");
				Console.WriteLine("1.Operacje na Tabeli Medicines \n2.Operacje na Tabeli Prescriptions \n3.Operacje na Tabeli Orders \n4.Exit");
				Console.Write("");
				command = Console.ReadLine().ToLower().Trim();

				if (command == "exit")
					break;

				switch (command)
				{
					case "1":
						Medicin.Menu();
						break;
					case "2":
						Prescription.Menu();
						break;
					case "3":
                        Order.Menu();
						break;

					case "4":
						command = "exit";
						return;

					default:
						Console.WriteLine("Nie rozpoznano polecenia");
						break;

				}

			} while (true);

		}

    }
}
