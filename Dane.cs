using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static MongoDB.Driver.WriteConcern;

namespace Projekt
{
    public partial class Dane : Form
    {
        MongoClient dbClient;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;
        string tabela="Pracownicy";
        string tryb="Widok";
        public Dane()
        {
            InitializeComponent();
        }
        public Dane(MongoClient dbClient, IMongoDatabase baza,string tabela,string tryb)
        {
            InitializeComponent();
            this.dbClient = dbClient;
            this.tabela = tabela;
            this.tryb = tryb;
            database = baza;

        }
        private void Laduj_Dane()
        {
            var document = collection.Find(new BsonDocument()).ToList();
            try {
                foreach (BsonElement kolumna in document[0])
                {
                    if (kolumna.Value.BsonType == BsonType.Document)
                    {
                        BsonDocument nest = kolumna.Value.ToBsonDocument();
                        foreach (BsonElement kolumna2 in nest)
                        {
                            pracownik_data.Columns.Add(kolumna2.Name, kolumna2.Name);
                        }
                        continue;
                    }
                    pracownik_data.Columns.Add(kolumna.Name, kolumna.Name);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("W kolekcji "+collection.CollectionNamespace+" bazy "+database.DatabaseNamespace+"nie ma żadnych rekordów", "Błąd",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(Close));
                return;
                
            }
            

            foreach (BsonDocument pracownik in document)
            {
                int row = pracownik_data.Rows.Add();
                int j = 0;

                foreach (BsonElement wartosc in pracownik)
                {

                    if (wartosc.Value.BsonType == BsonType.Document)
                    {
                        BsonDocument wartosc_nested = wartosc.Value.ToBsonDocument();
                        foreach (BsonElement wartosc2 in wartosc_nested)
                        {
                            pracownik_data.Rows[row].Cells[j++].Value = wartosc2.Value; ;
                        }
                        continue;
                    }
                    try {
                        pracownik_data.Rows[row].Cells[j++].Value = wartosc.Value;
                    }catch(Exception)
                    {
                        pracownik_data.Columns.Add(wartosc.Name,wartosc.Name);
                        pracownik_data.Rows[row].Cells[--j].Value = wartosc.Value;
                    }
                    
                }

            }
        }
        private void Dane_Load(object sender, EventArgs e)
        {
            if (tryb == "edycja") { pracownik_data.ReadOnly = false; }
            collection = database.GetCollection<BsonDocument>(tabela);
            Laduj_Dane();

           
            


        }

        private void pracownik_data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) {
                int row = e.RowIndex;
                int column = e.ColumnIndex;

                string cell = pracownik_data.Columns[column].Name;
                string data_id = pracownik_data.Rows[row].Cells[0].Value.ToString();
                var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(data_id));
                var update = Builders<BsonDocument>.Update.Set(cell, pracownik_data.Rows[row].Cells[e.ColumnIndex].Value.ToString());
                var test = collection.UpdateOne(filter, update);
            }
            


        }
        

        private void pracownik_data_CellEndEdit(object sender, DataGridViewCellValueEventArgs e)
        {
            
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            pracownik_data.Rows.Clear();
            pracownik_data.Refresh();
            Laduj_Dane();
        }
    }
}
