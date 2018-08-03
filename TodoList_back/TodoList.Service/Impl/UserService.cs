using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Repository;
using TodoList.Repository.Entity;

namespace TodoList.Service.Impl
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly TodoListDbContext todoListDbContext;
        public UserService(TodoListDbContext todoListDbContext)
        {
            this.todoListDbContext = todoListDbContext;
        }
        //用户注册
        public int Register(string username, string password)
        {
            // 数据库连接字符串
            var connStr = "server=192.168.50.208;port=3306;Initial Catalog=training;user id=training;password=123456;ConnectionReset=false;SslMode = none;";
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                #region 账号唯一校验
                cmd.CommandText = "select id from lzq_user where username = @name and password = @password";
                cmd.Parameters.Add(new MySqlParameter("@name", username));
                cmd.Parameters.Add(new MySqlParameter("@password", password));
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return -1;
                }
                else
                {
                    var tran = conn.BeginTransaction();
                    try
                    {
                        #region 用户注册
                        cmd.CommandText = "INSERT INTO `lzq_user` (`username`, `password`) VALUES (@name, @password)";
                        cmd.Parameters.Add(new MySqlParameter("@name", username));
                        cmd.Parameters.Add(new MySqlParameter("@password", password));
                        cmd.Transaction = tran;
                        cmd.ExecuteNonQuery();

                        #endregion

                        #region 获取用户id

                        cmd.CommandText = "select last_insert_id()";
                        var id = long.Parse(cmd.ExecuteScalar().ToString());

                        #endregion

                        #region 插入一条示例todo

                        // 创建todo示例
                        cmd.CommandText = $"insert into lzq_todolist(content, userId) values ('示例',{id})";
                        cmd.ExecuteNonQuery();

                        #endregion
                        tran.Commit();

                        return 1;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        Console.WriteLine(e);
                        return 0;
                    }

                }
                    #endregion
                    
            }
        }

        public long Login(string name, string password)
        {
            var connStr = "server=192.168.50.208;port=3306;Initial Catalog=training;user id=training;password=123456;ConnectionReset=false;SslMode = none;";

             using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "select id from lzq_user where username = @name and password = @password";
                cmd.Parameters.Add(new MySqlParameter("@name", name));
                cmd.Parameters.Add(new MySqlParameter("@password", password));
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return dr.GetInt64(0);
                }
                return 0;
            }
        }
    }
}
