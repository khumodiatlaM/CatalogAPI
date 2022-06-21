using CatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace CatalogAPI.Data
{
    public class SqlItemsData : IItemsData
    {
        private readonly SqlConnection conn = new SqlConnection("server = LABD66D457D5B; database = TestDb; Integrated Security = true;");

        public void CreateItem(Item item)
        {
            SqlCommand command = new SqlCommand($"Insert into ItemsTable(Id,Name,Price,CreatedDate) values('{item.Id}' ,'{item.Name}', {item.Price} ,'{item.CreatedDate}')", conn);
            conn.Open();
            
            command.ExecuteNonQuery();
            conn.Close();  
        }

        public void DeleteItem(Guid id)
        {
            SqlCommand command = new SqlCommand($"Delete from ItemsTable  where Id = '{id}'", conn);
            conn.Open();

            command.ExecuteNonQuery();
            conn.Close();
        }

        public Item GetItem(Guid id)
        {
            SqlDataAdapter adapter = new($"Select Id, Name, Price, CreatedDate from ItemsTable where Id = '{id}'", conn);
            DataTable table = new DataTable();

            adapter.Fill(table);
            List<Item> items = new();

            if (table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    items.Add(new Item
                    {
                        Id = Guid.Parse((string)row["Id"]),
                        Name = row["Name"].ToString(),
                        Price = (decimal)row["Price"],
                        CreatedDate = (DateTimeOffset)row["CreatedDate"]

                    });
                }
            }

            return items[0];

        }

        public IEnumerable<Item> GetItems()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("Select * from ItemsTable", conn);
            DataTable table = new DataTable();

            adapter.Fill(table);
            List<Item> items = new();

            if(table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    items.Add(new Item
                    {
                        Id = Guid.Parse((string)row["Id"]),
                        Name = row["Name"].ToString(),
                        Price = (decimal)row["Price"],
                        CreatedDate = (DateTimeOffset)row["CreatedDate"]

                    });
                }
            }

            return items;
        }

        public void UpdateItem(Item item)
        {
            SqlCommand command = new SqlCommand($"Update ItemsTable set Name = '{item.Name}', Price =  '{item.Price}' where Id = '{item.Id}'", conn);
            conn.Open();

            command.ExecuteNonQuery();
            conn.Close();
        }
    }
}
