using Dapper;
using DataAccess.Configuration;
using DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class SQL_Methods<T> : ISQL_Methods<T> where T : class
    {
        private DB_Connection Connection = new DB_Connection();

        // 1. Queryables 

        public List<T> SQL_Query(string query, T parameters)
        {
            string sentence = query;

            List<T> aux_List;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                var ListaT = connection.Query<T>(sentence, parameters);
                aux_List = ListaT.ToList();
            }
            return aux_List;
        }

        public T SQL_QueryFirstOrDefault(string query, T parameters)
        {
            string sentencia = query;
            T aux;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                aux = connection.QueryFirstOrDefault<T>(sentencia, parameters);
            };
            return aux;
        }

        public List<T> SQL_Query(string query)
        {
            string sentence = query;

            List<T> aux_List = new List<T>();

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                var ListaT = connection.Query<T>(sentence);
                aux_List = ListaT.ToList();
            };
            return aux_List;
        }

        
        //2. Executables:        

        public async Task<int> SQL_Executable(string query, T parameters)
        {
            string sentence = query;
            int affectedRows = 0;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {

                affectedRows = await connection.ExecuteAsync(sentence, toModify);

            }

            return affectedRows;
        }



        //Sin uso.

        protected List<T> SQL_QueryWithPagination(string query, Pagination pagination)
        {
            string sentence = query;

            List<T> aux_List;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                var ListaT = connection.Query<T>(sentence, pagination);
                aux_List = ListaT.ToList();
            }
            return aux_List;
        }
    }
}
