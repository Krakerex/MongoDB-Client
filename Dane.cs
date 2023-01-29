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
using System.Runtime.InteropServices.ComTypes;

namespace Projekt
{
    public partial class Dane : Form
    {

        MongoClient dbClient;
        IMongoDatabase baza;
        IMongoCollection<BsonDocument> kolekcja;
        string tabela="Pracownicy";
        string tryb="Widok";
        public Dane()
        {
            InitializeComponent();

        }

        public Dane(MongoClient dbClient, IMongoDatabase baza, string tabela, string tryb, string nazwabazy)
        {
            InitializeComponent();
            this.dbClient = dbClient;
            this.tabela = tabela;
            this.tryb = tryb;
            this.baza = baza;
            kolekcja = baza.GetCollection<BsonDocument>(tabela);
            this.Text = nazwabazy+'/'+baza.DatabaseNamespace.ToString() + '/'+kolekcja.CollectionNamespace.CollectionName.ToString();
            
        }
        private void Dane_Load(object sender, EventArgs e)
        {
            if (tryb == "edycja")
            {
                pracownik_data.ReadOnly = false;
                deleteRecord.Visible = true;

            }
            kolekcja = baza.GetCollection<BsonDocument>(tabela);
            Laduj_Dane();
        }
        private void Laduj_Dane()
        {
            var wiersze = kolekcja.Find(new BsonDocument()).ToList();
            try {
                foreach (BsonElement kolumna in wiersze[0])
                {
                    if (kolumna.Value.BsonType == BsonType.Document)
                    {
                        BsonDocument zagnieżdżonaKolekcja = kolumna.Value.ToBsonDocument();
                        foreach (BsonElement zagnieżdżonaKolumna in zagnieżdżonaKolekcja)
                        {
                            pracownik_data.Columns.Add(zagnieżdżonaKolumna.Name + "Document.", zagnieżdżonaKolumna.Name);
                        }
                        continue;
                    }
                    pracownik_data.Columns.Add(kolumna.Name, kolumna.Name);
                }
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("W kolekcji "+kolekcja.CollectionNamespace+" bazy "+baza.DatabaseNamespace+"nie ma żadnych rekordów", "Błąd",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BeginInvoke(new MethodInvoker(Close));
                return;
                
            }
            

            foreach (BsonDocument pracownik in wiersze)
            {
                int wiersz = pracownik_data.Rows.Add();
                int indeksKomórki = 0;

                foreach (BsonElement wartosc in pracownik)
                {

                    if (wartosc.Value.BsonType == BsonType.Document)
                    {
                        BsonDocument zagnieżdżoneWartości = wartosc.Value.ToBsonDocument();
                        foreach (BsonElement zagnieżdżonaWartość in zagnieżdżoneWartości)
                        {
                            pracownik_data.Rows[wiersz].Cells[indeksKomórki++].Value = zagnieżdżonaWartość.Value; ;
                        }
                        continue;
                    }
                    try {
                        pracownik_data.Rows[wiersz].Cells[indeksKomórki++].Value = wartosc.Value;
                    }catch(Exception)
                    {
                        pracownik_data.Columns.Add(wartosc.Name,wartosc.Name);
                        pracownik_data.Rows[wiersz].Cells[--indeksKomórki].Value = wartosc.Value;
                    }
                    
                }

            }
        }
        

        private void pracownik_data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0) {
                int wiersz = e.RowIndex;
                int kolumna = e.ColumnIndex;

                string komórka = pracownik_data.Columns[kolumna].Name;
                string idRekordu = pracownik_data.Rows[wiersz].Cells[0].Value.ToString();
                var filtrAktualizacji = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(idRekordu));
                var zapytanie = Builders<BsonDocument>.Update.Set(komórka, pracownik_data.Rows[wiersz].Cells[e.ColumnIndex].Value.ToString());
                kolekcja.UpdateOne(filtrAktualizacji, zapytanie);
            }
            


        }
        private void refresh_button_Click(object sender, EventArgs e)
        {
            pracownik_data.Rows.Clear();
            pracownik_data.Columns.Clear();
            pracownik_data.Refresh();
            Laduj_Dane();
        }

        private void pracownik_data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            deleteRecord.Enabled = true;
        }
        private void deleteRecord_Click(object sender, EventArgs e)
        {
            var zgoda = MessageBox.Show("Czy chcesz usunąć ten rekord?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            string idUsuwanegoRekordu;
            if (zgoda == DialogResult.Yes)
            {
                foreach (DataGridViewCell komórka in pracownik_data.SelectedCells)
                {
                    idUsuwanegoRekordu = pracownik_data.Rows[komórka.RowIndex].Cells[0].Value.ToString();

                    var filtrUsuwania = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(idUsuwanegoRekordu));
                    kolekcja.DeleteOne(filtrUsuwania);
                    MessageBox.Show("Usunięto rekord o id: " + idUsuwanegoRekordu, "Usunięto");
                }

            }
        }
    }
}
