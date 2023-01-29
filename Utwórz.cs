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
        List<string> zagnieżdżoneObiekty = new List<string>();
        MongoClient dbClient;
        IMongoDatabase baza;
        IMongoCollection<BsonDocument> kolekcja;
        string nazwabazy;
        string tabela;
        public Utwórz()
        {
            InitializeComponent();
        }
        public Utwórz(MongoClient dbClient, IMongoDatabase baza, string tabela, string nazwabazy)
        {
            InitializeComponent();
            this.dbClient = dbClient;
            this.baza = baza;
            this.tabela = tabela;
            kolekcja = this.baza.GetCollection<BsonDocument>(tabela);
            Laduj_Dane();
            this.Text = nazwabazy + '/' + baza.DatabaseNamespace.ToString() + '/' + kolekcja.CollectionNamespace.CollectionName.ToString();
            this.nazwabazy = nazwabazy;
        }



        private void Laduj_Dane()
        {
            dataGridView1.ReadOnly = false;
            try
            {
                var document = kolekcja.Find(new BsonDocument()).ToList();
                foreach (BsonElement kolumna in document[0])
                {
                    if (kolumna.Name == "_id") { continue; }
                    if (kolumna.Value.BsonType == BsonType.Document)
                    {
                        BsonDocument zagnieżdżonaKolekcja = kolumna.Value.ToBsonDocument();
                        zagnieżdżoneObiekty.Add(kolumna.Name.ToString());

                        foreach (BsonElement zagnieżdżonaKolumna in zagnieżdżonaKolekcja)
                        {
                            dataGridView1.Columns.Add(zagnieżdżonaKolumna.Name + ".Document", zagnieżdżonaKolumna.Name);
                        }
                        continue;
                    }
                    dataGridView1.Columns.Add(kolumna.Name, kolumna.Name);
                }
            }
            catch (Exception)
            {
                dataGridView1.Columns.Add("<Nazwa 1>", "<Nazwa 1>");
            }


        }



        private async void button3_Click(object sender, EventArgs e)
        {
            bool czyZagnieżdżone = false;
            int j = 0;
            foreach (DataGridViewRow wiersz in dataGridView1.Rows)
            {
                if (wiersz.Cells.Count > 0)
                {
                    bool czyWierszPusty = true;

                    foreach (DataGridViewCell komórka in wiersz.Cells)
                    {
                        if (komórka.Value != null)
                        {
                            czyWierszPusty = false;
                            break;
                        }
                    }

                    if (czyWierszPusty)
                    {
                        break;
                    }
                }
                if (wiersz.Index == dataGridView1.RowCount - 1) { break; }
                var wiersze = new BsonDocument();
                var zagnieżdżoneWiersze = new BsonDocument();
                foreach (DataGridViewCell komórka in wiersz.Cells)
                {


                    if (komórka.Value == null) { komórka.Value = ""; }
                    string nazwaKolumny = dataGridView1.Columns[komórka.ColumnIndex].Name.ToString();
                    int indeksKomórki = komórka.ColumnIndex, ilośćKolumn = dataGridView1.ColumnCount - 1;
                    if (nazwaKolumny.Contains(".Document"))
                    {
                        czyZagnieżdżone = true;

                        zagnieżdżoneWiersze.Add(nazwaKolumny, komórka.Value.ToString());

                    }
                    if ((!nazwaKolumny.Contains(".Document") || indeksKomórki == ilośćKolumn) && czyZagnieżdżone)
                    {
                        wiersze.Add(zagnieżdżoneObiekty[j++].ToString().Replace(".Document", ""), zagnieżdżoneWiersze);
                        czyZagnieżdżone = false;
                        zagnieżdżoneWiersze = new BsonDocument();
                        j++;

                    }
                    else if ((!nazwaKolumny.Contains(".Document")))
                    {
                        wiersze.Add(nazwaKolumny, komórka.Value.ToString());

                    }

                }
                await kolekcja.InsertOneAsync(wiersze);

            }
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            Laduj_Dane();

        }
        private void createRecords_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.ColumnCount;
            dataGridView1.Columns.Remove(dataGridView1.Columns[i - 1]);
        }
        private void addColumn_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.ColumnCount;

            if (textBox1.Text == "") { dataGridView1.Columns.Add("<Nazwa " + i + ">", "<Nazwa " + i + ">"); }
            else { dataGridView1.Columns.Add(textBox1.Text, textBox1.Text); }
        }
        private void deleteColumn_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.ColumnCount;
            dataGridView1.Columns.Remove(dataGridView1.Columns[i - 1]);
        }
        private void addRow_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.RowCount;
            dataGridView1.Rows.Add();
        }
        private void deleteRow_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.RowCount;
            dataGridView1.Rows.Remove(dataGridView1.Rows[i - 2]);
        }
    }

}
