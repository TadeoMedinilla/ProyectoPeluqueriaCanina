using Dapper;
using DataAccess.Configuration;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class SQL_Methods<T> where T : class
    {
        private DB_Connection Connection = new DB_Connection();

        protected List<T> SQL_Query(string query)
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

        protected List<T> SQL_Query(string query, T Buscar)
        {
            string sentence = query;

            List<T> aux_List;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                var ListaT = connection.Query<T>(sentence, Buscar);
                aux_List = ListaT.ToList();
            }
            return aux_List;
        }

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

        protected T SQL_QueryFirstOrDefault(string query, T Buscar)
        {
            string sentencia = query;
            T aux;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {
                aux = connection.QueryFirstOrDefault<T>(sentencia, Buscar);
            };
            return aux;
        }

        protected async Task<int> SQL_Executable(string query, T toModify)
        {
            string sentence = query;
            int affectedRows = 0;

            using (var connection = new SqlConnection(Connection.ConnectionString))
            {

                affectedRows = await connection.ExecuteAsync(sentence, toModify);

            }

            return affectedRows;
        }

    }
}
