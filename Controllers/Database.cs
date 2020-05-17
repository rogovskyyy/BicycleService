using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using MyBicycle.Model;

namespace MyBicycle.Controllers
{
    public class Database
    {
        public int COUNTER { get; set; }
        private readonly string DBPATH = $@"{System.Environment.CurrentDirectory}\Lite.db";
        private readonly WINDOW_ADD window_add;


        public Database() { }
        public Database(WINDOW_ADD Window)
        {
            window_add = Window;
        }

        public void INSERT_NEW_CUSTOMER()
        {
            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");

                var customer = new Customers
                {
                    Name = window_add.textBox1.Text,
                    Surname = window_add.textBox2.Text,
                    Phone = window_add.maskedTextBox1.Text,
                    Model = window_add.textBox3.Text,
                    Date = window_add.dateTimePicker1.Value,
                    Description = window_add.textBox4.Text,
                    Description_Long = window_add.textBox5.Text,
                    IsActive = true,
                    LastUpdate = DateTime.Now
                };

                col.Insert(customer);
            }
        }

        public IEnumerable<Customers> SELECT_ALL_CUSTOMERS()
        {
            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");
                var result = col.FindAll();
                return result;
            }
        }

        public List<Customers> GetAll()
        {
            var list = new List<Customers>();

            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");

                foreach (Customers _id in col.FindAll())
                {
                    list.Add(_id);
                }
            }

            return list;
        }

        public void UPDATE_STATUS(int Id, bool status)
        {
            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");
                var result = col.FindById(Id);

                result.IsActive = status;
                result.LastUpdate = DateTime.Now;

                col.Update(result);
            }
        }

        public Customers SELECT_TO_PDF(int Id)
        {
            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");
                var result = col.FindById(Id);
                return result;
            }
        }

        public Customers SELECT_ONE(int Id)
        {
            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");
                var result = col.FindById(Id);
                return result;
            }
        }

        public void UPDATE_CUSTOMER(Customers model)
        {
            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");
                var result = col.FindById(model.Id);
                col.Update(model);
            }
        }

        public List<Customers> SELECT_ALL_WHERE_PHONE(string phone)
        {
            var list = new List<Customers>();

            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");

                foreach (Customers client in col.Find(n => n.Phone.Contains(phone)))
                {
                    list.Add(client);
                }
            }

            return list;
        }

        public List<Customers> SELECT_ALL_BETWEEN_DATE(DateTime from, DateTime to)
        {
            var list = new List<Customers>();

            using (var db = new LiteDatabase(DBPATH))
            {
                var col = db.GetCollection<Customers>("customers");
                var query = col.Find(x => x.Date.Date >= from.Date && x.Date.Date <= to.Date);

                foreach (Customers client in query)
                {
                    list.Add(client);
                }

                COUNTER = query.Count();
            }

            return list;
        }
    }
}
