using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Rjachimek_Pharmacy.Models
{
    public class Prescription : ActiveRecord
    {
        public int ID { get; private set; }
        public string CustomerName { get; set; }
        public string PESEL { get; set; }
        public string PrescriptionNumber { get; set; }

		public Prescription(string customerName, string pesel, string prescriptionNumber)
		{
			CustomerName = customerName;
			PESEL = pesel;
			PrescriptionNumber = prescriptionNumber;
		}

		public Prescription()
		{
		}

		public override void Reload()
		{
			var connection = ActiveRecord.Open();

			do
			{

				try
				{

					Console.WriteLine("Podaj Id do modyfikacji: ");
					string ID = Console.ReadLine();


					if (Prescription.Check(Int32.Parse(ID)) == false)
					{
						Console.ReadKey();
						return;
					}

					Console.WriteLine("Nazwa klienta: ");
					string customerName = Console.ReadLine();
					Console.WriteLine("PESEL: ");
					string pesel = Console.ReadLine();
					Console.WriteLine("Numer recepty: ");
					string prescriptionNumber = Console.ReadLine();
					


					Prescription prescription = new Prescription(customerName, pesel, prescriptionNumber);



					using (connection)
					{


						connection.Open();
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;
						sqlCommand.CommandText = @"UPDATE  Prescriptions SET CustomerName = @customerName, PESEL = @pesel, PrescriptionNumber = @prescriptionNumber 
                                                   WHERE ID = @ID";

						var sqlCustomerNameParam = new SqlParameter
						{
							DbType = System.Data.DbType.AnsiString,
							Value = prescription.CustomerName,
							ParameterName = "@customerName"
						};

						var sqlPESELParam = new SqlParameter
						{
							DbType = System.Data.DbType.AnsiString,
							Value = prescription.PESEL,
							ParameterName = "@pesel"
						};

						var sqlPrescriptionNumberParam = new SqlParameter
						{
							DbType = System.Data.DbType.AnsiString,
							Value = prescription.PrescriptionNumber,
							ParameterName = "@prescriptionNumber"
						};

						var sqlIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = ID,
							ParameterName = "@ID"
						};


						sqlCommand.Parameters.Add(sqlCustomerNameParam);
						sqlCommand.Parameters.Add(sqlPESELParam);
						sqlCommand.Parameters.Add(sqlPrescriptionNumberParam);
						sqlCommand.Parameters.Add(sqlIDParam);

						sqlCommand.ExecuteNonQuery();

					}

					askTheUser2:
					Console.WriteLine("Chcesz zmodyfikować kolejną pozycję? (T/N): ");
					string addAnother = Console.ReadLine().Trim().ToLower();
					if (addAnother == "t")
						continue;
					else if (addAnother == "n")
						break;
					else
					{
						Console.WriteLine("Nieznana odpowiedź");
						goto askTheUser2;
					}
				}


				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			} while (true);
		}
		public static bool Check(int ID)
		{

			try
			{
				var connection = ActiveRecord.Open();

				int id = 0;
				id = ID;

				using (connection)
				{

					connection.Open();
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = "SELECT COUNT(*) FROM Prescriptions WHERE ID = @id";

					var sqlIDParam = new SqlParameter
					{
						DbType = System.Data.DbType.Int32,
						Value = ID,
						ParameterName = "@id"
					};

					sqlCommand.Parameters.Add(sqlIDParam);


					int userCount = (int)sqlCommand.ExecuteScalar();

					if (userCount >= 0)
					{

						return true;

					}
					else
					{

						return false;

					}


				}

			}
			catch (Exception e)
			{
				Console.WriteLine("Nie ma takiego id");
				Console.WriteLine(e.Message);
				return false;

			}

		}

		public override void Remove()
		{

			do
			{
				try
				{

					Console.WriteLine("Podaj Id do usuniecia: ");
					string ID = Console.ReadLine();




					if (Prescription.Check(Int32.Parse(ID)) == false)
					{
						Console.ReadKey();
						return;
					}

					var connection = ActiveRecord.Open();

					using (connection)
					{
						connection.Open();
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;
						sqlCommand.CommandText = @"DELETE FROM Prescriptions WHERE ID = @ID";



						var sqlIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = ID,
							ParameterName = "@ID"
						};

						sqlCommand.Parameters.Add(sqlIDParam);

						sqlCommand.ExecuteNonQuery();

					}

					askTheUser2:
					Console.WriteLine("Chcesz usunąć kolejną pozycję? (T/N): ");
					string addAnother = Console.ReadLine().Trim().ToLower();
					if (addAnother == "t")
						continue;
					else if (addAnother == "n")
						break;
					else
					{
						Console.WriteLine("Nieznana odpowiedź");
						goto askTheUser2;
					}

				}


				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			} while (true);
		}

		public override void Save()
		{

			do
			{
				try
				{
					var connection = ActiveRecord.Open();

					Console.WriteLine("Nazwa klienta: ");
					string customerName = Console.ReadLine();
					Console.WriteLine("PESEL: ");
					string pesel = Console.ReadLine();
					Console.WriteLine("Numer recepty: ");
					string prescriptionNumber = Console.ReadLine();



					Prescription prescription = new Prescription(customerName, pesel, prescriptionNumber);


					using (connection)
					{
						connection.Open();
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;
						sqlCommand.CommandText = @"INSERT INTO Prescriptions (CustomerName, PESEL, PrescriptionNumber)
			                             VALUES (@CustomerName, @PESEL, @PrescriptionNumber)";

						var sqlCustomerNameParam = new SqlParameter
						{
							DbType = System.Data.DbType.AnsiString,
							Value = prescription.CustomerName,
							ParameterName = "@customerName"
						};

						var sqlPESELParam = new SqlParameter
						{
							DbType = System.Data.DbType.AnsiString,
							Value = prescription.PESEL,
							ParameterName = "@pesel"
						};

						var sqlPrescriptionNumberParam = new SqlParameter
						{
							DbType = System.Data.DbType.AnsiString,
							Value = prescription.PrescriptionNumber,
							ParameterName = "@prescriptionNumber"
						};

						var sqlIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = ID,
							ParameterName = "@ID"
						};


						sqlCommand.Parameters.Add(sqlCustomerNameParam);
						sqlCommand.Parameters.Add(sqlPESELParam);
						sqlCommand.Parameters.Add(sqlPrescriptionNumberParam);
						sqlCommand.Parameters.Add(sqlIDParam);

						sqlCommand.ExecuteNonQuery();

					}

					askTheUser2:
					Console.WriteLine("Chcesz dodać kolejną pozycję? (T/N): ");
					string addAnother = Console.ReadLine().Trim().ToLower();
					if (addAnother == "t")
						continue;
					else if (addAnother == "n")
						break;
					else
					{
						Console.WriteLine("Nieznana odpowiedź");
						goto askTheUser2;
					}


				}


				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			} while (true);
		}

		public void Show()
		{


			try
			{
				var connection = ActiveRecord.Open();

				using (connection)
				{

					connection.Open();
					var sqlCommand = new SqlCommand();
					sqlCommand.Connection = connection;
					sqlCommand.CommandText = "SELECT * FROM Prescriptions";

					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

					while (sqlDataReader.Read())
					{
						Console.WriteLine();
						Console.WriteLine("ID: " + sqlDataReader["ID"]);
						Console.WriteLine("Nazwa Klienta: " + sqlDataReader["CustomerName"]);
						Console.WriteLine("Pesel: " + sqlDataReader["PESEL"]);
						Console.WriteLine("Numer recepty: " + sqlDataReader["PrescriptionNumber"]);



						Console.WriteLine();

					}
					Console.ReadLine();
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

		}

		public static void Menu()
		{
			Prescription prescription = new Prescription();
			do
			{
				Console.Clear();
				string command;
				Console.WriteLine("Menu");
				Console.WriteLine("1.Dodaj  \n2.Zaktualizuj \n3.Wyświetl \n4.Usuń \n5.Powrót");
				Console.Write("");
				command = Console.ReadLine().ToLower().Trim();

				if (command == "exit")
					break;

				switch (command)
				{
					case "1":
						prescription.Save();
						break;
					case "2":
						prescription.Reload();
						break;
					case "3":
						prescription.Show();
						break;
					case "4":
						prescription.Remove();
						break;

					case "5":
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
