﻿using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoopark
{
    internal class Paginator<T>
    {

        private string query;
        public string Query { get => query; set => query = value; }

        private int countRows;
        private readonly Func<MySqlDataReader, T> funcGetObject;
        private readonly string table;

        public int PageIndex
        {
            get => pageIndex;
            set
            {
                if (value > countPage)
                    value = countPage;
                if (value < 0)
                    value = 0;
                pageIndex = value;
            }
        }

        public int CountRows
        {
            get => countRows;
            set
            {
                countRows = value;
                countPage = GetCountPage(table);
            }
        }

        public int CountPages { get => countPage; }

        int countPage = 0;
        private int pageIndex;

        public Paginator(string query, int countRows, Func<MySqlDataReader, T> funcGetObject, string table)
        {
            this.Query = query;
            this.countRows = countRows;
            this.funcGetObject = funcGetObject;
            this.table = table;
        }

        private int GetCountPage(string table)
        {
            int result = 0;
            string query = "SELECT COUNT(0) from " + table;
            var db = DB.GetInstance();
            if (db.OpenConnection())
            {
                result = (int)(long)MySqlHelper.ExecuteScalar(db.Connection, query);
                db.CloseConnection();
            }

            return result / CountRows;
        }

        public List<T> GetPageValues()
        {
            List<T> results = new();
            var db = DB.GetInstance();
            if (db.OpenConnection())
            {
                using (var mc = new MySqlCommand(
                    Query +
                    $" LIMIT {PageIndex * CountRows}, {CountRows}",
                    db.Connection))
                using (var dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        T obj = funcGetObject(dr);
                        results.Add(obj);
                    }
                }
                db.CloseConnection();
            }
            return results;
        }
    }
}
