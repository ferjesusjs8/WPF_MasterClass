using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        public static string DbFile { get; private set; }

        public static bool Insert<T>(T item)
        {
            bool result = false;

            DbFile = Path.Combine(Environment.CurrentDirectory, $"{typeof(T).Name}.db3");

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                int rowsInserted = conn.Insert(item);

                result = rowsInserted > 0;
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;
            DbFile = Path.Combine(Environment.CurrentDirectory, $"{typeof(T).Name}.db3");

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                int rowsInserted = conn.Update(item);

                result = rowsInserted > 0;
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;
            DbFile = Path.Combine(Environment.CurrentDirectory, $"{typeof(T).Name}.db3");

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                var rowsDeleted = conn.Delete(item);

                result = rowsDeleted > 0;
            }

            return result;
        }

        public static List<T> Read<T>() where T : new()
        {
            List<T> items;
            DbFile = Path.Combine(Environment.CurrentDirectory, $"{typeof(T).Name}.db3");

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                items =
                    conn
                        .Table<T>()
                        .ToList();
            }

            return items;
        }
    }
}