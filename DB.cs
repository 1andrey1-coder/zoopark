using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using zoopark.DTO;
using zoopark.View;

namespace zoopark
{
    internal class DB // открытие соединения
    {

        //ПОИИИИИСКККККК

        
        public void CloseConnection()
        {
            try
            {
                connection.Close();
            }
            catch
            { }
        }


        private DB() { }
        static DB instance;
        public static DB GetInstance()
        {
            if (instance == null)
                instance = new();
            return instance;
        }
        public MySqlConnection Connection { get => connection; }

        MySqlConnection connection;
        void ConfigureConnection() // подключение соединения
        {
            MySqlConnectionStringBuilder sb =
                new MySqlConnectionStringBuilder();
            sb.Server = "192.168.200.13";
            sb.UserID = "student";
            sb.Password = "student";
            sb.Database = "socicka";
            sb.CharacterSet = "utf8mb4";
            connection = new MySqlConnection(
                sb.ToString());
        }




        // должностььььььььььььььььььььь

        internal List<position> GetDataList()
        {
            List<position> positions = new List<position>();
            if (OpenConnection())
            {
                string sql = "select * from tbl_position";
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        position position = new position()
                        {
                            ID = reader.GetInt32("id"),
                            Title = reader.GetString("Title")
                        };
                        positions.Add(position);
                    }
                }
                connection.Close();
            }
            return positions;
        }





        //уДАЛЕНИЕЕЕЕЕЕЕ

        internal void DeleteTool(Tools selectedTool)
        {
            if (OpenConnection())
            {
                string sql = "delete from tbl_Tools where id = " + selectedTool.ID;
                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }
        }

        internal void DeleteSotrudnikk(Sotrudnikk SelectedSotrudnikk)
        {
            if (OpenConnection())
            {
                string sql = "delete from tbl_Sotrudnikk where id = " + SelectedSotrudnikk.ID;
                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }
            else
            {
                MessageBox.Show("Уберите сначала инструменты");
                return;
            }
        }


        internal void DeleteFamily(Family SelectedFamily)
        {
            if (OpenConnection())
            {
                string sql = "delete from tbl_Family where id = " + SelectedFamily.ID;
                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }

        }

        internal void DeleteAnimal(Animal selectedAnimal)
        {
            if (OpenConnection())
            {
                string sql = "delete from tbl_Animal where id = " + selectedAnimal.ID;
                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }
        }


        internal void DeleteFood(Food selectedFood)
        {
            if (OpenConnection())
            {
                string sql = "delete from tbl_Food where id = " + selectedFood.ID;
                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }
        }

        internal void DeletePostition(position selectedPost)
        {

            if (OpenConnection())
            {
                string sql = "delete from tbl_position where id = " + selectedPost.ID;
                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }

        }
        // Дядя Пушкин, у нас не работает метод удаления, а так же мы не знаем почему в должностях можно редактировать с сохранением,
        // а в состальних таблица - нет. Следущий вопрос заключается в том, что нам не известна причина того, почему при создании
        // животного в комбобоксе не высвечиваются сотрудники, а остальные параметры показываются как ни в чём не бывало
        // Помогите нам пожалуймта, уверены ошибка какая-то глупая, но Вы - наш Мессия


        internal IEnumerable<Tools> LoadToolsBySotrudnik(int id)
        {
            List<Tools> tool = new();
            if (OpenConnection())
            {
                string sql = "SELECT tt.id, tt.classification FROM tbl_belonging JOIN tbl_Tools tt ON tbl_belonging.`id tool` = tt.id WHERE `id sotrudnik` = " + id;
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tools tools = new Tools()
                        {
                            ID = reader.GetInt32("id"),
                            Classification = reader.GetString("classification"),
                        };
                        tool.Add(tools);
                    }
                }
                connection.Close();
            }
            return tool;
        }

        internal int GetNextID(string table)
        {
            int result = 0;
            string sql = $"SHOW TABLE STATUS FROM socicka WHERE Name = '{table}'";
            if (OpenConnection())
            {
                using (var mc = new MySqlCommand(sql, connection))
                using (var dr = mc.ExecuteReader())
                {
                    if (dr.Read())
                        result = dr.GetInt32("Auto_increment");
                }
                connection.Close();
            }
            return result;
        }

        internal void UpdateToolsForSotrudnik(int id, IEnumerable<int> tools)
        {
            if (OpenConnection())
            {
                string sql = "delete from tbl_belonging where `id sotrudnik` = " + id + ";";
                sql += String.Join(" ", tools.Select(x => $"insert into tbl_belonging (`id sotrudnik`, `id tool`) values ({id}, {x});"));

                MySqlHelper.ExecuteNonQuery(connection, sql);
                connection.Close();
            }
        }

        internal void EditPosition(position edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_position " +
                "SET Title = @title " +
                "WHERE id = " + edit.ID;
                MySqlParameter[] parametrs = new MySqlParameter[1]
                {
                    new MySqlParameter("title", edit.Title),
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);

                connection.Close();
            }
        }



        internal void AddPositon(position edit)
        {
            if (OpenConnection())
            {
                string sql = "INSERT INTO  tbl_position (Title) " +
                "VALUES (@title); ";
                MySqlParameter[] parametrs = new MySqlParameter[1]
                {
                    new MySqlParameter("title", edit.Title),
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }


        //сотрудниккккккккккккккккккккккккккккккккккк

        internal void EditSotrudnikk(Sotrudnikk edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Sotrudnikk " +
                "SET Name = @Name, " +
                "SET Otchestvo = @Otchestvo, " +
                "SET Scheadule = @Scheadule, " +
                "SET Salary = @Salary, " +
                 "SET Idposition = @Idposition, " +
                "WERE id = " + edit.ID;

                connection.Close();
            }
        }

        internal void AddSotrudnikk(Sotrudnikk edit)
        {
            if (OpenConnection())
            {
                string sql = "INSERT INTO  tbl_Sotrudnikk (Name, Otchestvo, Scheadule, Salary,`id position`) values " +
                " (@name, @otchestvo, @scheadule, @salary, @idposition); ";
                MySqlParameter[] parametrs = new MySqlParameter[5]
                {
                    new MySqlParameter("name", edit.Name),
                    new MySqlParameter("otchestvo", edit.Otchestvo),
                    new MySqlParameter("scheadule", edit.Scheadule),
                    new MySqlParameter("salary", edit.Salary),
                    new MySqlParameter("idposition", edit.Idposition),
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        internal List<Sotrudnikk> GetDataListSotrudnik()
        {
            List<Sotrudnikk> sotridnikk = new List<Sotrudnikk>();
            if (OpenConnection())
            {
                string sql = "SELECT * FROM tbl_Sotrudnikk sotr JOIN tbl_position tp ON sotr.`id position` = tp.id";
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Sotrudnikk sotrudnikk = new Sotrudnikk()
                        {
                            ID = reader.GetInt32("id"),
                            Name = reader.GetString("Name"),
                            Otchestvo = reader.GetString("Otchestvo"),
                            Scheadule = reader.GetString("Scheadule"),
                            Salary = reader.GetInt32("Salary"),
                            Idposition = reader.GetInt32("id position"),
                            Position = new position
                            {
                                Title = reader.GetString("Title"),
                            }
                        };
                        sotridnikk.Add(sotrudnikk);
                    }
                }
                connection.Close();
            }
            return sotridnikk;
        }


        internal void EditSotrudnik(Sotrudnikk edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Sotrudnikk " +
                "SET Name = @name " +
                "SET Otchestvo = @otchestvo " +
                "SET Scheadule = @scheadule " +
                "SET Salary = @salary " +
                "SET Id position = @idposition " +
                "WHERE id = " + edit.ID;
                MySqlParameter[] parametrs = new MySqlParameter[5]
                 {
                    new MySqlParameter("name", edit.Name),
                    new MySqlParameter("otchestvo", edit.Otchestvo),
                    new MySqlParameter("scheadule", edit.Scheadule),
                    new MySqlParameter("salary", edit.Salary),
                    new MySqlParameter("idposition", edit.Idposition),
                 };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }


        //итнструментыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыыы


        internal void EditTool(Tools edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Tools " +
                "SET classification = @Classification, " +
                "SET perfomance = @Perfomance, " +
                "SET reliability = @Reliability, " +
                "SET wear resistance = @Wear_resistance, " +
                "WERE id = " + edit.ID;

                connection.Close();
            }
        }

        internal void AddTool(Tools edit)
        {
            if (OpenConnection())
            {
                string sql = "INSERT INTO  tbl_Tools (classification, performance, reliability,`wear resistance`) values " +
                " (@classification, @performance, @reliability, @wear_resistance); ";
                MySqlParameter[] parametrs = new MySqlParameter[4]
                {
                    new MySqlParameter("classification", edit.Classification),
                    new MySqlParameter("performance", edit.Performance),
                    new MySqlParameter("reliability", edit.Reliability),
                    new MySqlParameter("wear_resistance", edit.Wear_resistance)
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        internal List<Tools> GetDataListTool()
        {
            List<Tools> tool = new List<Tools>();
            if (OpenConnection())
            {
                string sql = "select * from tbl_Tools";
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Tools tools = new Tools()
                        {
                            ID = reader.GetInt32("id"),
                            Classification = reader.GetString("classification"),
                            Performance = reader.GetString("performance"),
                            Reliability = reader.GetString("reliability"),
                            Wear_resistance = reader.GetString("wear resistance"),
                        };
                        tool.Add(tools);
                    }
                }
                connection.Close();
            }
            return tool;
        }


        internal void EditTools(Tools edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Tools " +
                "SET classification = @classification " +
                "SET performance = @performance " +
                "SET reliability = @reliability " +
                "SET wear_resistance = @wear_resistance " +
                "WHERE id = " + edit.ID;
                MySqlParameter[] parametrs = new MySqlParameter[4]
                {
                    new MySqlParameter("classification", edit.Classification),
                    new MySqlParameter("performance", edit.Performance),
                    new MySqlParameter("reliability", edit.Reliability),
                    new MySqlParameter("wear_resistance", edit.Wear_resistance)
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        //ЧЕРТИИИИИИИИИИИИИИИИ

        internal void EditAnimal(Sotrudnikk edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE   tbl_Animal " +
                "SET Feed = @IdFeed, " +
                "SET Sotrudnik = @IdSotrudnik, " +
                "SET Family = @IdFamily, " +
                "SET Anname = @Anname, " +
                "WERE id = " + edit.ID;

                connection.Close();
            }
        }

        internal void AddAnimal(Animal edit)
        {
            if (OpenConnection())
            {
                string sql = "INSERT INTO  tbl_Animal (`id Feed`, `id Sotrudnik`, `id Family`, Anname) values " +
                " (@idFeed, @idSotrudnik, @idFamily, @Anname); ";
                MySqlParameter[] parametrs = new MySqlParameter[4]
                {
                    new MySqlParameter("idFeed", edit.IdFeed),
                    new MySqlParameter("idSotrudnik", edit.IdSotrudnik),
                    new MySqlParameter("idFamily", edit.IdFamily),
                    new MySqlParameter("Anname", edit.Anname),
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        internal List<Animal> GetDataListAnimal()
        {
            List<Animal> animal = new List<Animal>();
            if (OpenConnection())
            {
                string sql = "SELECT * FROM tbl_Animal ta JOIN tbl_Food tf ON ta.`id Feed` = tf.id JOIN tbl_Sotrudnikk ts ON ta.`id Sotrudnik` = ts.id JOIN tbl_Family tf1 ON ta.`id Family` = tf1.id";
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Animal animals = new Animal()
                        {
                            ID = reader.GetInt32("id"),
                            IdFeed = reader.GetInt32("id Feed"),
                            Food = new Food
                            {
                                Tip = reader.GetString("Tip")
                            },
                            IdSotrudnik = reader.GetInt32("id Sotrudnik"),
                            Sotrudnikk = new Sotrudnikk
                            {

                                Name = reader.GetString("Name")

                            },

                            IdFamily = reader.GetInt32("id Family"),
                            Family = new Family
                            {

                                family = reader.GetString("family")

                            },
                            Anname = reader.GetString("Anname")
                        };
                        animal.Add(animals);
                    }
                }
                connection.Close();
            }
            return animal;
        }

        internal void EditAnimal(Animal edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Animal " +
                "SET id Feed = @IdFeed " +
                "SET id Sotrudnik = @IdSotrudnik " +
                "SET id Family = @IdFamily " +
                "SET Anname = @Anname " +
                "WHERE id = " + edit.ID;
                MySqlParameter[] parametrs = new MySqlParameter[4]
                     {
                    new MySqlParameter("idFeed", edit.IdFeed),
                    new MySqlParameter("idSotrudnik", edit.IdSotrudnik),
                    new MySqlParameter("idFamily", edit.IdFamily),
                    new MySqlParameter("Anname", edit.Anname),
                     };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }



        //ЕДАААААААААААААААААААААААААААААААААА

        internal void EditFood(Food edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Food " +
                "SET Tip = @Tip, " +
                "SET Count = @Count, " +
                "WERE id = " + edit.ID;

                connection.Close();
            }
        }

        internal void AddFood(Food edit)
        {
            if (OpenConnection())
            {
                string sql = "INSERT INTO  tbl_Food (Tip, Count) values " +
                " (@Tip, @Count); ";
                MySqlParameter[] parametrs = new MySqlParameter[2]
                {
                        new MySqlParameter("Tip", edit.Tip),
                        new MySqlParameter("Count", edit.Count),
                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        internal List<Food> GetDataListFood()
        {
            List<Food> food = new List<Food>();
            if (OpenConnection())
            {
                string sql = "select * from tbl_Food";
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Food foods = new Food()
                        {
                            ID = reader.GetInt32("id"),
                            Tip = reader.GetString("Tip"),
                            Count = reader.GetInt32("Count"),

                        };
                        food.Add(foods);
                    }
                }
                connection.Close();
            }
            return food;
        }

        internal void EditFoods(Food edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Food " +
                "SET Tip = @Tip " +
                "SET Count = @Count " +
                "WHERE id = " + edit.ID;
                MySqlParameter[] parametrs = new MySqlParameter[2]
                    {
                        new MySqlParameter("Tip", edit.Tip),
                        new MySqlParameter("Count", edit.Count),
                    };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        //СЕМЕЙСТВОООоооооооооооооооооооОООООООООООООООООООООООоооооооЪ

        internal void EditFamily(Family edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Family" +
                "SET family = @Family, " +
                "WERE id = " + edit.ID;

                connection.Close();
            }
        }

        internal void AddFamily(Family edit)
        {
            if (OpenConnection())
            {
                string sql = "INSERT INTO  tbl_Family (family) values " +
                " (@Family); ";
                MySqlParameter[] parametrs = new MySqlParameter[1]
                {
                    new MySqlParameter("Family", edit.family)

                };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }

        internal List<Family> GetDataListFamily()
        {
            List<Family> Family = new List<Family>();
            if (OpenConnection())
            {
                string sql = "select * from tbl_Family";
                using (var mc = new MySqlCommand(sql, connection))
                using (var reader = mc.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Family family = new Family()
                        {
                            ID = reader.GetInt32("id"),
                            family = reader.GetString("family")

                        };
                        Family.Add(family);
                    }
                }
                connection.Close();
            }
            return Family;
        }

        internal void EditFamilys(Family edit)
        {
            if (OpenConnection())
            {
                string sql = "UPDATE tbl_Family " +
                "SET Family = @Family " +
                "WHERE id = " + edit.ID;
                MySqlParameter[] parametrs = new MySqlParameter[1]
                    {
                        new MySqlParameter("Family", edit.family)
                    };
                MySqlHelper.ExecuteNonQuery(connection, sql, parametrs);
                connection.Close();
            }
        }


        public bool OpenConnection() //проверка соединения
        {
            if (connection == null)
                ConfigureConnection();
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }
            return true;


        }
        public void TestConnection()
        {
            if (OpenConnection())
                connection.Close();
            System.Windows.MessageBox.Show("Всё нормль");
        }
    }

            
 }
