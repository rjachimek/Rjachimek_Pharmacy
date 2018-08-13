using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Rjachimek_Pharmacy.Models
{
    public class Order : ActiveRecord
    {
        public int ID { get; private set; }
        public int MedicinID { get; set; }
        public int PrescriptionID { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
		
		public Order(int medicinId, int prescriptionId, DateTime date, int amount)
		{
			MedicinID = medicinId;
			PrescriptionID = prescriptionId;
			Date = date;
			Amount = amount;
			
		}

		public Order()
		{
		}

		public override void Reload()
		{
			var connection = ActiveRecord.Open();

			do
			{

				try
				{

					Console.WriteLine("Podaj numer Id zamówienia do modyfikacji: ");
					string ID = Console.ReadLine();


					if (Order.Check(Int32.Parse(ID)) == false)
					{
						Console.ReadKey();
						return;
					}

                    Console.WriteLine("Id leku: ");
                    int medicinId = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Id recepty: ");
                    int prescriptionId = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Data: ");
					DateTime date = DateTime.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
					Console.WriteLine("Ilość: ");
					int amount = Int32.Parse(Console.ReadLine());

					Order order = new Order(medicinId, prescriptionId, date, amount);

					using (connection)
					{


						connection.Open();
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;
						sqlCommand.CommandText = @"UPDATE  Orders SET MedicinID = @medicinId, PrescriptionID = @prescriptionId, Date = @date, Amount = @amount
                                                   WHERE ID = @ID";


						var sqlMedicinIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = order.MedicinID,
							ParameterName = "@medicinId"
						};

						var sqlPrescriptionIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = order.PrescriptionID,
							ParameterName = "@prescriptionId"
						};
						var sqlDateParam = new SqlParameter
						{
							DbType = System.Data.DbType.DateTime,
							Value = order.Date,
							ParameterName = "@date"
						};

						var sqlAmountParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = order.Amount,
							ParameterName = "@amount"
						};

						var sqlIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = ID,
							ParameterName = "@ID"
						};


						sqlCommand.Parameters.Add(sqlMedicinIDParam);
						sqlCommand.Parameters.Add(sqlPrescriptionIDParam);
						sqlCommand.Parameters.Add(sqlDateParam);
						sqlCommand.Parameters.Add(sqlAmountParam);
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
					sqlCommand.CommandText = "SELECT COUNT(*) FROM Orders WHERE ID = @id";

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




					if (Order.Check(Int32.Parse(ID)) == false)
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
						sqlCommand.CommandText = @"DELETE FROM Orders WHERE ID = @ID";



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


					Console.WriteLine("Id leku: ");
					int medicinId = Int32.Parse(Console.ReadLine());
					Console.WriteLine("Id recepty: ");
					int prescriptionId = Int32.Parse(Console.ReadLine());
					Console.WriteLine("Data: ");
					DateTime date = DateTime.Parse(Console.ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
					Console.WriteLine("Ilość: ");
					int amount = Int32.Parse(Console.ReadLine());

					Order order = new Order(medicinId, prescriptionId, date, amount);


					using (connection)
					{
						connection.Open();
						var sqlCommand = new SqlCommand();
						sqlCommand.Connection = connection;
						sqlCommand.CommandText = @"INSERT INTO Orders (Orders.MedicinID, Orders.PrescriptionID, Date, Amount)
													VALUES ( (select ID from Medicines where ID = @medicinId), (select ID from Prescriptions where ID = @prescriptionId), @date, @amount)";



						var sqlMedicinIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = order.MedicinID,
							ParameterName = "@medicinId"
						};

						var sqlPrescriptionIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = order.PrescriptionID,
							ParameterName = "@prescriptionId"
						};
						var sqlDateParam = new SqlParameter
						{
							DbType = System.Data.DbType.DateTime,
							Value = order.Date,
							ParameterName = "@date"
						};

						var sqlAmountParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = order.Amount,
							ParameterName = "@amount"
						};

						var sqlIDParam = new SqlParameter
						{
							DbType = System.Data.DbType.Int32,
							Value = ID,
							ParameterName = "@ID"
						};


						sqlCommand.Parameters.Add(sqlMedicinIDParam);
						sqlCommand.Parameters.Add(sqlPrescriptionIDParam);
						sqlCommand.Parameters.Add(sqlDateParam);
						sqlCommand.Parameters.Add(sqlAmountParam);
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
					sqlCommand.CommandText = "SELECT * FROM Orders";

					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

					while (sqlDataReader.Read())
					{
						Console.WriteLine();
						Console.WriteLine("ID: " + sqlDataReader["ID"]);
						Console.WriteLine("ID leku: " + sqlDataReader["MedicinID"]);
						Console.WriteLine("ID recepty: " + sqlDataReader["PrescriptionID"]);
						Console.WriteLine("Data: " + sqlDataReader["Date"]);
						Console.WriteLine("Ilość: " + sqlDataReader["Amount"]);
						


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
			Order order = new Order();
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
						order.Save();
						break;
					case "2":
						order.Reload();
						break;
					case "3":
						order.Show();
						break;
					case "4":
						order.Remove();
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
