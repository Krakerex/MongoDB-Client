using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Projekt
{
    public partial class Utwórz : Form
    {
        List<string> nestedName= new List<string>();
        MongoClient dbClient;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        string tabela = "Pracownicy";
        string tryb = "Widok";
        public Utwórz()
        {
            InitializeComponent();
        }
        public Utwórz(MongoClient dbClient, IMongoDatabase baza, string tabela)
        {
            InitializeComponent();
            this.dbClient = dbClient;
            this.database = baza;
            this.tabela = tabela;
            collection = database.GetCollection<BsonDocument>(tabela);
            Laduj_Dane();

        }



        private void Laduj_Dane()
        {
            dataGridView1.ReadOnly = false;
            try
            {
                var document = collection.Find(new BsonDocument()).ToList();
                foreach (BsonElement kolumna in document[0])
                {
                    if (kolumna.Name=="_id") { continue; }
                    if (kolumna.Value.BsonType == BsonType.Document)
                    {
                        BsonDocument nest = kolumna.Value.ToBsonDocument();
                        nestedName.Add(kolumna.Name.ToString());

                        foreach (BsonElement kolumna2 in nest)
                        {
                            dataGridView1.Columns.Add(kolumna2.Name + ".Document", kolumna2.Name);
                        }
                        continue;
                    }
                    dataGridView1.Columns.Add(kolumna.Name, kolumna.Name);
                }
            }
            catch(Exception)
            {
                dataGridView1.Columns.Add("<Nazwa>", "<Nazwa>");
            }


        }

        

        private async void button3_Click(object sender, EventArgs e)
        {
            bool nestFlag = false;
            int j = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells.Count > 0)
                {
                    bool rowIsEmpty = true;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            rowIsEmpty = false;
                            break;
                        }
                    }

                    if (rowIsEmpty)
                    {
                        break;
                    }
                }
                if (row.Index==dataGridView1.RowCount-1) { break; }
                var document = new BsonDocument();
                var nestedDocument = new BsonDocument();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    
                    
                    if (cell.Value == null) { cell.Value = ""; }
                    string columnName = dataGridView1.Columns[cell.ColumnIndex].Name.ToString();
                    int CellI= cell.ColumnIndex, ColI= dataGridView1.ColumnCount - 1;
                    if (columnName.Contains(".Document"))
                    {
                        nestFlag = true;

                        nestedDocument.Add(columnName, cell.Value.ToString());

                    }
                    if ((!columnName.Contains(".Document") || CellI == ColI) && nestFlag)
                    {
                        document.Add(nestedName[j++].ToString().Replace(".Document",""), nestedDocument);
                        nestFlag = false;
                        nestedDocument = new BsonDocument();
                        j++;

                    }else if((!columnName.Contains(".Document")))
                    {
                        document.Add(columnName, cell.Value.ToString());
                        
                    }

                }
               await collection.InsertOneAsync(document);

            }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            Laduj_Dane();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.ColumnCount;
            dataGridView1.Columns.Remove(dataGridView1.Columns[i - 1]);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.ColumnCount;
            
            if (textBox1.Text == "") { dataGridView1.Columns.Add("<Nazwa " + i + ">", "<Nazwa " + i + ">"); }
            else { dataGridView1.Columns.Add(textBox1.Text, textBox1.Text); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.RowCount;
            dataGridView1.Rows.Add("<Wiersz " + i + ">", "<Wiersz " + i + ">");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.RowCount;
            dataGridView1.Rows.Remove(dataGridView1.Rows[i-2]);
        }
    }
}
