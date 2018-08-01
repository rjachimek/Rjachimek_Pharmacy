using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;


namespace Rjachimek_Pharmacy.Models
{
    public class Medicin : ActiveRecord
    {
        public int ID { get; private set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public bool WithPrescription { get; set; }

        public Medicin(string name, string manufacturer, decimal price, int amount, bool withPrescription)
        {
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            Amount = amount;
            WithPrescription = withPrescription;
        }

        public Medicin()
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


                    if (Medicin.Check(Int32.Parse(ID)) == false)
                    {
                        Console.ReadKey();
                        return;
                    }

                    Console.WriteLine("Nazwa: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Producent: ");
                    string manufacturer = Console.ReadLine();
                    Console.WriteLine("Cena: ");
                    decimal price = Decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Ilość: ");
                    int amount = Int32.Parse(Console.ReadLine());
                    askTheUser:
                    Console.WriteLine("Na recepte (T/N): ");
                    string tempPerscription = Console.ReadLine().ToLower().Trim();
                    bool withPrescription = false;
                    if (tempPerscription == "t")
                        withPrescription = true;
                    else if (tempPerscription == "n")
                        withPrescription = false;
                    else
                    {
                        Console.WriteLine("Błędny format.");
                        goto askTheUser;
                    }


                    Medicin medicin = new Medicin(name, manufacturer, price, amount, withPrescription);



                    using (connection)
                    {


                        connection.Open();
                        var sqlCommand = new SqlCommand();
                        sqlCommand.Connection = connection;
                        sqlCommand.CommandText = @"UPDATE  Medicines SET Name = @name, Manufacturer = @Manufacturer, Price = @Price, Amount = @Amount, WithPrescription = @WithPrescription 
                                                   WHERE ID = @ID";

                        var sqlNameParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = medicin.Name,
                            ParameterName = "@name"
                        };

                        var sqlManufacturerParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = medicin.Manufacturer,
                            ParameterName = "@Manufacturer"
                        };

                        var sqlPriceParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Decimal,
                            Value = medicin.Price,
                            ParameterName = "@Price"
                        };
                        var sqlAmountParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = medicin.Amount,
                            ParameterName = "@Amount"
                        };
                        var sqlWithPrescriptionParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Boolean,
                            Value = medicin.WithPrescription,
                            ParameterName = "@WithPrescription"
                        };

                        var sqlIDParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = ID,
                            ParameterName = "@ID"
                        };


                        sqlCommand.Parameters.Add(sqlNameParam);
                        sqlCommand.Parameters.Add(sqlManufacturerParam);
                        sqlCommand.Parameters.Add(sqlPriceParam);
                        sqlCommand.Parameters.Add(sqlAmountParam);
                        sqlCommand.Parameters.Add(sqlWithPrescriptionParam);
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
                    sqlCommand.CommandText = "SELECT COUNT(*) FROM Medicines WHERE ID = @id";

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




                    if (Medicin.Check(Int32.Parse(ID)) == false)
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
                        sqlCommand.CommandText = @"DELETE FROM Medicines WHERE ID = @ID";



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


                    Console.WriteLine("Nazwa: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Producent: ");
                    string manufacturer = Console.ReadLine();
                    Console.WriteLine("Cena: ");
                    decimal price = Decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Ilość: ");
                    int amount = Int32.Parse(Console.ReadLine());
                    askTheUser:
                    Console.WriteLine("Na recepte (T/N): ");
                    string tempPerscription = Console.ReadLine().Trim().ToLower();
                    bool withPrescription = false;
                    if (tempPerscription == "t")
                        withPrescription = true;
                    else if (tempPerscription == "n")
                        withPrescription = false;
                    else
                    {
                        Console.WriteLine("Błędny format.");
                        goto askTheUser;
                    }

                    Medicin medicin = new Medicin(name, manufacturer, price, amount, withPrescription);


                    using (connection)
                    {
                        connection.Open();
                        var sqlCommand = new SqlCommand();
                        sqlCommand.Connection = connection;
                        sqlCommand.CommandText = @"INSERT INTO Medicines (Name, Manufacturer, Price, Amount, WithPrescription)
			                             VALUES (@Name, @Manufacturer, @Price, @Amount, @WithPrescription)";

                        var sqlNameParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = medicin.Name,
                            ParameterName = "@Name"
                        };

                        var sqlManufacturerParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.AnsiString,
                            Value = medicin.Manufacturer,
                            ParameterName = "@Manufacturer"
                        };

                        var sqlPriceParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Decimal,
                            Value = medicin.Price,
                            ParameterName = "@Price"
                        };
                        var sqlAmountParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Int32,
                            Value = medicin.Amount,
                            ParameterName = "@Amount"
                        };
                        var sqlWithPrescriptionParam = new SqlParameter
                        {
                            DbType = System.Data.DbType.Boolean,
                            Value = medicin.WithPrescription,
                            ParameterName = "@WithPrescription"
                        };

                        sqlCommand.Parameters.Add(sqlNameParam);
                        sqlCommand.Parameters.Add(sqlManufacturerParam);
                        sqlCommand.Parameters.Add(sqlPriceParam);
                        sqlCommand.Parameters.Add(sqlAmountParam);
                        sqlCommand.Parameters.Add(sqlWithPrescriptionParam);

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
                    sqlCommand.CommandText = "SELECT * FROM Medicines";

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Console.WriteLine();
                        Console.WriteLine("ID: " + sqlDataReader["ID"]);
                        Console.WriteLine("Nazwa: " + sqlDataReader["Name"]);
                        Console.WriteLine("Producent: " + sqlDataReader["Manufacturer"]);
                        Console.WriteLine("Cena: " + sqlDataReader["Price"]);
                        Console.WriteLine("Ilość: " + sqlDataReader["Amount"]);
                        Console.Write("Na recepte: ");
                        if (Convert.ToBoolean(sqlDataReader["WithPrescription"]) == true)
                            Console.Write("Tak");
                        else
                            Console.Write("Nie");

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
            Medicin medicin = new Medicin();
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
                        medicin.Save();
                        break;
                    case "2":
                        medicin.Reload();
                        break;
                    case "3":
                        medicin.Show();
                        break;
                    case "4":
                        medicin.Remove();
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
