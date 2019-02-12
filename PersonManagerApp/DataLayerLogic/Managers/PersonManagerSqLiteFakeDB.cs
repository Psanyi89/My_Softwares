using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PersonEntities;

namespace DataLayerLogic.Managers
{
	 internal class PersonManagerSqLiteFakeDB : IPersonManager
	{
		#region Get Specific ConnectionString from AppConfig
		/// <summary>
		/// Chooses the specified ConnectionString from AppConfig file
		/// </summary>
		/// <param name="conn">Name of connection in AppConfig</param>
		/// <returns>Returns the connectionstring</returns>
		public static string GetConnectionString(string conn = "FakeDBSqLite")
		{
			return ConfigurationManager.ConnectionStrings[conn].ConnectionString;
		}
		#endregion

		public Person AddPerson(Person person)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@Name", person.Name);
			parameters.Add("@DateOfBirth", person.DateOfBirth.ToShortDateString());
			parameters.Add("@Email", person.Email);

			using (IDbConnection connection = new SQLiteConnection(GetConnectionString()))
			{
				var result = connection.Execute(@"insert into People(
																  Name,
																  DateOFBirth,
																  Email)
																  values(@Name,
																  @DateOfBirth,
																  @Email);
																  select last_insert_rowid()", parameters);
				person.Id = result;
				return person;
			}
		}

		public bool DeletePerson(Person person)
		{
			using (IDbConnection connection=new SQLiteConnection(GetConnectionString()))
			{
				return connection.Execute("delete from People where People.Id = @Id", person) > 0;
			}
		}

		public List<Person> GetPersons()
		{
			using (IDbConnection connection = new SQLiteConnection(GetConnectionString()))
			{

				return connection.Query<Person>("select * from People").ToList();

			}
		}

		public List<Person> SearchResult(string name = null, DateTime? dateOfBirth = null, string email = null)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@Name", string.IsNullOrWhiteSpace(name) ? null : name);
			parameters.Add("@DateOfBirth", dateOfBirth > DateTime.MinValue && dateOfBirth < DateTime.MaxValue ? dateOfBirth.Value.ToShortDateString() : null);
			parameters.Add("@Email", string.IsNullOrWhiteSpace(email) ? null : email);
			using (IDbConnection connection = new SQLiteConnection(GetConnectionString()))
			{

				return connection.Query<Person>($@"select * from People 
			where(instr(upper(Name), upper(@Name)) > 0 or @Name is null) and
			(instr(DateOfBirth,substr(@DateOfBirth,1,4)) > 0 or @DateOfBirth is null) and
			(instr(upper(Email), upper(@Email)) > 0 or @Email is null)", parameters).ToList();

			}
		}

		public Person UpdatePerson(Person person)
		{
			if(person==null || person.Id == 0)
			{
				throw new NoNullAllowedException("Empty person, or no id");
			}
			var parameters = new DynamicParameters();
			parameters.Add("@Name", string.IsNullOrWhiteSpace(person.Name) ? null : person.Name);
			parameters.Add("@Email", string.IsNullOrWhiteSpace(person.Email) ? null : person.Email);
			parameters.Add("@DateOfBirth",person.DateOfBirth> DateTime.MinValue && person.DateOfBirth<DateTime.MaxValue ? person.DateOfBirth.ToShortDateString() : null);
			parameters.Add("@Id", person.Id);
			using (IDbConnection connection = new SQLiteConnection(GetConnectionString()))
			{
				connection.Execute(@"update People
												   set Name= Ifnull(@Name,Name),
												  DateOfBirth = ifnull(@DateOfBirth,DateOfBirth),
												  Email=ifnull(@Email,Email)
												  where Id=@Id", parameters);
				return person;
			}
		}
	}
}
