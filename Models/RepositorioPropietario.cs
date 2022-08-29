using System.Data;
using MySql.Data.MySqlClient;

namespace InmobiliariaULP.Models;

public class RepositorioPropietario
{
    public RepositorioPropietario() 
		{

		}

        public int Alta(Propietario p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection( "Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"INSERT INTO Propietarios (Nombre, Apellido, Dni, Telefono, Email) " +
					$"VALUES (@nombre, @apellido, @dni, @telefono, @email);" +
					"SELECT LAST_INSERT_ID();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@telefono", p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email);
					//command.Parameters.AddWithValue("@clave", p.Clave);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
                    p.Id= res;
                    connection.Close();
				}
			}
			return res;
		}

        public int Baja(int id)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection("Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"DELETE FROM Propietarios WHERE Id= @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@id", id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}
		public int Modificacion(Propietario p)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection("Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"UPDATE Propietarios SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email " +
					$"WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", p.Nombre);
					command.Parameters.AddWithValue("@apellido", p.Apellido);
					command.Parameters.AddWithValue("@dni", p.Dni);
					command.Parameters.AddWithValue("@telefono", p.Telefono);
					command.Parameters.AddWithValue("@email", p.Email);
					//command.Parameters.AddWithValue("@clave", p.Clave);
					command.Parameters.AddWithValue("@id", p.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

        public IList<Propietario> ListarTodos()
		{
			IList<Propietario> res = new List<Propietario>();
			using (MySqlConnection connection = new MySqlConnection( "Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email" +
                    $" FROM Propietarios";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Propietario p = new Propietario
						{
							Id = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader["Telefono"].ToString(),
							Email = reader.GetString(5),
							//Clave = reader.GetString(6),
						};
						res.Add(p);
					}
					connection.Close();
				}
			}
			return res;
		}

        public Propietario ListarPorId(int id)
		{
			Propietario p = null;
			using (MySqlConnection connection = new MySqlConnection("Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email FROM Propietarios" +
					$" WHERE Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						p = new Propietario
						{
							Id= reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader.GetString(4),
							Email = reader.GetString(5),
							//Clave = (String)reader["Clave"],
						};
					}
					connection.Close();
				}
			}
			return p;
        }
}

