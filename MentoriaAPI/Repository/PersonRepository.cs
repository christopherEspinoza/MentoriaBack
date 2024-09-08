using MentoriaAPI.Models;
using MentoriaAPI.Repository.Interfaces;
using MentoriaAPI.Utils;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MentoriaAPI.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public string CreateOrUpdatePerson(Person person)
        {
            string result = "";
            Conexion conn = new Conexion();
            try
            {
                result = conn.ExecuteSimpleStoreProcedure("sp_create_or_update_person",
                    new MySqlParameter() { ParameterName = "e_id", Value=person.Id},
                    new MySqlParameter() { ParameterName = "e_nombre", Value = person.Nombre },
                    new MySqlParameter() { ParameterName = "e_identificacion", Value = person.Identificacion },
                    new MySqlParameter() { ParameterName = "e_direccion", Value = person.Direccion },
                    new MySqlParameter() { ParameterName = "e_estado", Value = person.estado }
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public string DeletePerson(int id)
        {
            string result = "";
            Conexion conn = new Conexion();
            try
            {
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "e_id";
                parameter.Value = id;

                result = conn.ExecuteSimpleStoreProcedure("sp_delete_person", parameter);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public Person GetPersonById(int id)
        {
            Person person = null;
            Conexion conn = new Conexion();
            try
            {
                MySqlParameter parameter = new MySqlParameter();
                parameter.ParameterName = "e_id";
                parameter.Value = id;

                var reader = conn.ExecuteStoreProcedure("sp_get_person", parameter);
                while (reader.Read())
                {
                    person = new Person
                    {
                        Id = reader.GetInt32("id_persona"),
                        Nombre = reader.GetString("nombre"),
                        Identificacion = reader.GetString("identificacion"),
                        Direccion = reader.GetString("direccion"),
                        estado = reader.GetChar("estado")
                    };
                }
                conn.CloseConnection();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return person;
        }

        public List<Person> ListPerson()
        {
            List<Person> persons = new List<Person>();
            Conexion conn = new Conexion();
            try
            {
                var reader = conn.ExecuteStoreProcedure("sp_list_person");
                while (reader.Read())
                {
                    persons.Add(new Person
                    {
                        Id = reader.GetInt32("id_persona"),
                        Nombre = reader.GetString("nombre"),
                        Identificacion = reader.GetString("identificacion"),
                        Direccion = reader.GetString("direccion"),
                        estado = reader.GetChar("estado")
                    });
                }
                conn.CloseConnection();

            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return persons;
        }
    }
}
