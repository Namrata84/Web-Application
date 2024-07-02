// Database.cs
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace CaseManagementApp
{
    public class Database
    {
        public static void Initialize()
        {
            using (var connection = new SqliteConnection("Data Source=cases.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    CREATE TABLE IF NOT EXISTS Cases (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Length REAL NOT NULL,
                        Width REAL NOT NULL,
                        Height REAL NOT NULL,
                        Weight REAL NOT NULL
                    );
                ";
                command.ExecuteNonQuery();
            }
        }

        public static void CreateCase(string name, double length, double width, double height, double weight)
        {
            using (var connection = new SqliteConnection("Data Source=cases.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO Cases (Name, Length, Width, Height, Weight)
                    VALUES ($name, $length, $width, $height, $weight);
                ";
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$length", length);
                command.Parameters.AddWithValue("$width", width);
                command.Parameters.AddWithValue("$height", height);
                command.Parameters.AddWithValue("$weight", weight);
                command.ExecuteNonQuery();
            }
        }

        public static List<Case> GetAllCases()
        {
            var cases = new List<Case>();

            using (var connection = new SqliteConnection("Data Source=cases.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Cases";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new Case
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Length = reader.GetDouble(2),
                            Width = reader.GetDouble(3),
                            Height = reader.GetDouble(4),
                            Weight = reader.GetDouble(5)
                        };
                        cases.Add(c);
                    }
                }
            }

            return cases;
        }

        public static void UpdateCase(int id, string name, double length, double width, double height, double weight)
        {
            using (var connection = new SqliteConnection("Data Source=cases.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    UPDATE Cases
                    SET Name = $name, Length = $length, Width = $width, Height = $height, Weight = $weight
                    WHERE Id = $id;
                ";
                command.Parameters.AddWithValue("$id", id);
                command.Parameters.AddWithValue("$name", name);
                command.Parameters.AddWithValue("$length", length);
                command.Parameters.AddWithValue("$width", width);
                command.Parameters.AddWithValue("$height", height);
                command.Parameters.AddWithValue("$weight", weight);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteCase(int id)
        {
            using (var connection = new SqliteConnection("Data Source=cases.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Cases WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);
                command.ExecuteNonQuery();
            }
        }
    }

    public class Case
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}
