using System.Data;
using MySql.Data.MySqlClient;

namespace InmobiliariaULP.Models;

public class RepositorioInquilino
{
    public RepositorioInquilino()
		{

		}

    
        public int Alta(Inquilino i)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection( "Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"INSERT INTO Inquilinos (Nombre, Apellido, Dni, Telefono, Email) " +
					$"VALUES (@nombre, @apellido, @dni, @telefono, @email);" +
					"SELECT LAST_INSERT_ID();";//devuelve el id insertado (LAST_INSERT_ID para mysql)
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", i.Nombre);
					command.Parameters.AddWithValue("@apellido", i.Apellido);
					command.Parameters.AddWithValue("@dni", i.Dni);
					command.Parameters.AddWithValue("@telefono", i.Telefono);
					command.Parameters.AddWithValue("@email", i.Email);
					//command.Parameters.AddWithValue("@clave", p.Clave);
					connection.Open();
					res = Convert.ToInt32(command.ExecuteScalar());
                    i.Id= res;
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
				string sql = $"DELETE FROM Inquilinos WHERE Id= @id";
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
		public int Modificacion(Inquilino i)
		{
			int res = -1;
			using (MySqlConnection connection = new MySqlConnection("Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"UPDATE Inquilinos SET Nombre=@nombre, Apellido=@apellido, Dni=@dni, Telefono=@telefono, Email=@email " +
					$"WHERE Id = @id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					command.Parameters.AddWithValue("@nombre", i.Nombre);
					command.Parameters.AddWithValue("@apellido", i.Apellido);
					command.Parameters.AddWithValue("@dni", i.Dni);
					command.Parameters.AddWithValue("@telefono", i.Telefono);
					command.Parameters.AddWithValue("@email", i.Email);
					//command.Parameters.AddWithValue("@clave", p.Clave);
					command.Parameters.AddWithValue("@id", i.Id);
					connection.Open();
					res = command.ExecuteNonQuery();
					connection.Close();
				}
			}
			return res;
		}

        public IList<Inquilino> ListarTodos()
		{
			IList<Inquilino> res = new List<Inquilino>();
			using (MySqlConnection connection = new MySqlConnection( "Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email" +
                    $" FROM Inquilinos";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
					command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					while (reader.Read())
					{
						Inquilino i = new Inquilino
						{
							Id = reader.GetInt32(0),
							Nombre = reader.GetString(1),
							Apellido = reader.GetString(2),
							Dni = reader.GetString(3),
							Telefono = reader["Telefono"].ToString(),
							Email = reader.GetString(5),
							//Clave = reader.GetString(6),
						};
						res.Add(i);
					}
					connection.Close();
				}
			}
			return res;
		}

        public Inquilino ListarPorId(int id)
		{
			Inquilino i = null;
			using (MySqlConnection connection = new MySqlConnection("Server=localhost;User=root;Password=;Database=inmobiliariaulp_miranda;SslMode=none"))
			{
				string sql = $"SELECT Id, Nombre, Apellido, Dni, Telefono, Email FROM Inquilinos" +
					$" WHERE Id=@id";
				using (MySqlCommand command = new MySqlCommand(sql, connection))
				{
                    command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                    command.CommandType = CommandType.Text;
					connection.Open();
					var reader = command.ExecuteReader();
					if (reader.Read())
					{
						i = new Inquilino
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
			return i;
        }
}

