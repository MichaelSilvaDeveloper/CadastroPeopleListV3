using DataBaseConnection;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Controller
{
    public class CadastraPessoa : ICadastraPessoa
    {
        //private static readonly List<Person> _peopleList = new List<Person>();
        private static readonly Connection _connection = new Connection();
        private static readonly SqlCommand _sqlCommand = new SqlCommand();
        private static readonly ValidateCpf _validateCpf = new ValidateCpf();
        SqlDataReader dataReader;

        public void AddPerson()
        {
            Console.WriteLine(" - Adicionar Pessoa - ");
            Console.WriteLine("Informe o nome: ");
            string namePerson = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Informe a idade: ");
            int agePerson = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Informe o telefone: ");
            int telephonePerson = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Informe o cpf: ");
            string cpfPerson = Convert.ToString(Console.ReadLine());

            while (!_validateCpf.CpfValid(cpfPerson))
            {
                Console.WriteLine("Formato de Cpf inválido");
                Console.WriteLine("Informe o cpf: ");
                cpfPerson = Convert.ToString(Console.ReadLine());
            }

            //_peopleList.Add(new Person(namePerson, agePerson, telephonePerson, cpfPerson));

            try
            {
                _sqlCommand.Connection = _connection.Connect();
                string strSql = "select Id_Pessoa from Pessoa where Cpf = " + cpfPerson;
                _sqlCommand.CommandText = strSql;
                dataReader = _sqlCommand.ExecuteReader();
                if (dataReader.HasRows)
                {
                    Console.WriteLine("Cpf já cadastrado");
                }
                else
                {
                    if (!dataReader.IsClosed) { dataReader.Close(); }
                    dataReader.Close();
                    strSql = String.Format("insert into Pessoa(Nome, Idade, Telefone, Cpf) values ( '{0}', '{1}', '{2}', '{3}' )", namePerson, agePerson, telephonePerson, cpfPerson);
                    _sqlCommand.CommandText = strSql;
                    _sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("Cadastrado com sucesso");
                }
                _connection.Disconnect();
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException.Message);
            }
        }

        public void ShowPeople()
        {
            Console.WriteLine(" - Pessoa Cadastradas - ");
            try
            {
                _sqlCommand.Connection = _connection.Connect();
                string strSql = "Select * from Pessoa";
                _sqlCommand.CommandText = strSql;
                dataReader = _sqlCommand.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine("Id_Pessoa: {0}, Nome: {1}, Idade: {2}, Telefone: {3}, Cpf: {4}", dataReader["Id_Pessoa"], dataReader["Nome"], dataReader["Idade"], dataReader["Telefone"], dataReader["Cpf"]);
                }
                _connection.Disconnect();
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException);
            }
        }

        public void SearchPerson()
        {
            Console.WriteLine(" - Buscar Pessoa Por Cpf - ");
            Console.WriteLine("Informe o CPF da Pessoa: ");
            string cpfPerson = Convert.ToString(Console.ReadLine()); 
            try
            {
                _sqlCommand.Connection = _connection.Connect();
                string strSql = "select * from Pessoa where Cpf = " + cpfPerson;
                _sqlCommand.CommandText = strSql;
                dataReader = _sqlCommand.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    Console.WriteLine("Pessoa não encontrada com esse CPF");
                }
                else
                {
                    while (dataReader.Read())
                    {
                        Console.WriteLine("Id_Pessoa: {0}, Nome: {1}, Idade: {2}, Telefone: {3}, Cpf: {4}", dataReader["Id_Pessoa"], dataReader["Nome"], dataReader["Idade"], dataReader["Telefone"], dataReader["Cpf"]);
                    }
                }
                _connection.Disconnect();
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException);
            }
        }

        public void RemovePerson()
        {
            Console.WriteLine(" - Remover Pessoa - ");
            Console.WriteLine("Informe o id da pessoa que deseja remover: ");
            int idPerson = Convert.ToInt32(Console.ReadLine());
            try
            {
                _sqlCommand.Connection = _connection.Connect();
                string strSql = "select * from Pessoa where Id_Pessoa = " + idPerson;
                _sqlCommand.CommandText = strSql;
                dataReader = _sqlCommand.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    Console.WriteLine("Pessoa não encontrada");
                }
                else
                {
                    if (!dataReader.IsClosed) { dataReader.Close(); }
                    dataReader.Close();
                    strSql = "delete from Pessoa where Id_Pessoa = " + idPerson;
                    _sqlCommand.CommandText = strSql;
                    _sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("Pessoa excluída");
                }
                _connection.Disconnect();
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException);
            }
        }

        public void EditPerson()
        {
            Console.WriteLine(" - Editar Pessoa - ");
            Console.WriteLine("Informe o id da pessoa que deseja editar");
            int editPersonById = Convert.ToInt32(Console.ReadLine());
            try
            {
                _sqlCommand.Connection = _connection.Connect();
                string strSql = "select * from Pessoa where Id_Pessoa = " + editPersonById;
                _sqlCommand.CommandText = strSql;
                dataReader = _sqlCommand.ExecuteReader();
                if (!dataReader.HasRows)
                {
                    Console.WriteLine("Pessoa não encontrada");
                }
                else
                {
                    if (!dataReader.IsClosed) { dataReader.Close(); }
                    dataReader.Close();
                    Console.WriteLine("Informe o novo nome da pessoa: ");
                    string newNamePerson = Convert.ToString(Console.ReadLine());
                    strSql = "update Pessoa set Nome = @nome where Id_Pessoa = " + editPersonById;
                    _sqlCommand.Parameters.AddWithValue("@Nome", newNamePerson);
                    _sqlCommand.CommandText = strSql;
                    _sqlCommand.ExecuteNonQuery();
                }
                _connection.Disconnect();
            }
            catch (SqlException sqlException)
            {
                Console.WriteLine(sqlException);
            }
        }
    }
}